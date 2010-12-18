using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Security.Cryptography;
using PADIbookCommonLib;
using System.Windows.Forms;

namespace Server
{
    class ServerToServerServicesObject : MarshalByRefObject, ServerToServerServices
    {
        public List<Post> getPosts()
        {
            return ServerApp._user.UserPosts;
        }

        public Friend sendFriendRequest(Friend friend)
        {
            MessageBox.Show(ServerApp._user.Username + " -> ServerToserver : sendFriendRequest(adds pending friend) : " + friend.Name + " %s" +friend.Uris.ElementAt(0));

            try
            {
                ServerApp._user.addPendingFriend(friend);
            }
            catch (DuplicatePendingFriendException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return null;
            }

            ClientServices client = (ClientServices)Activator.GetObject(typeof(ClientServices),
                ServerApp._clientUri + "/" + ServicesNames.ClientServicesName);

            RemoteAsyncFriendDelegate del = new RemoteAsyncFriendDelegate(client.sendFriendRequest);
            del.BeginInvoke(friend, null, null);

            return null;
        }

        public void changeNameOfFriend(String name, String primary)
        {
            foreach (Friend f in ServerApp._user.Friends)
            {
                if (f.Uris.ElementAt(0).CompareTo(primary) == 0)
                {
                    f.Name = name;

                }
            }
            ClientServices client = (ClientServices)Activator.GetObject(typeof(ClientServices),
                ServerApp._clientUri + "/" + ServicesNames.ClientServicesName);
            RemoteAsyncChangeNameInFriendsDelegate changedel = new RemoteAsyncChangeNameInFriendsDelegate(client.changeNameOfFriend);
            RemoteAsyncRefreshFriendsDelegate del = new RemoteAsyncRefreshFriendsDelegate(client.refreshFriends);
            del.BeginInvoke(null, null);
            changedel.BeginInvoke(name, primary, null, null);
        }

        private bool queryIsVerified(SignedQueryByFile signedQuery)
        {
            RSACryptoServiceProvider rsaWithPublicKeyOfRemoteUser = new RSACryptoServiceProvider();
            String contactingName = "didnt find";
            foreach (Friend f in ServerApp._user.Friends)
            {
                if (f.Uris.ElementAt(0).CompareTo(signedQuery.Query.ContactingServerUri.ElementAt(0)) == 0)
                {
                    contactingName = f.Name;
                }
            }
            //MessageBox.Show(ServerApp._user.Username + "says that signedQuery.Query.Name=" + contactingName);
            UserEntry userToVerify = ServerApp._pkiCommunicator.GetVerifiedUserPublicKey(contactingName);
            if (userToVerify == null)
            {
                MessageBox.Show(ServerApp._user.Username + " : User to verify was null!");
                return false;
            }
            rsaWithPublicKeyOfRemoteUser.FromXmlString(userToVerify.PubKey);
            //MessageBox.Show("pubkey of user: " + userToVerify.NodeId + " with key: " + userToVerify.PubKey);
            byte[] data = Encoding.Default.GetBytes(signedQuery.Query.ToString());
            return rsaWithPublicKeyOfRemoteUser.VerifyData(data, "SHA1", signedQuery.Signature);
        }

        private bool isPredecessor(Query q)
        {
            foreach (Friend f in ServerApp._user.Friends)
            {
                if (q.ContactingServerUri.ElementAt(0).CompareTo(f.Uris.ElementAt(0)) == 0)
                {
                    if(!f.SucessorSwarm)
                        return true;
                }
            }
            return false;

        }

        public void shareObject(SignedQueryByFile signedQuery)
        {
            Boolean sendMessage = false;
            QueryByFile query = signedQuery.Query;

            MessageBox.Show(ServerApp._user.Username + " received a request to share " + query.Name);

            if (ServerApp._user.SentMessages.Contains(query.Id))
            {
                MessageBox.Show(ServerApp._user.Username + " : Message was already sent so the request was discarted!");
                return;
            }

            if (ServerApp._user.ReceivedFileMessages.Contains(signedQuery.Query))
            {
                MessageBox.Show(ServerApp._user.Username + " : Message was already received so the request was discarted!");
                return;
            }

            //only accepts messages from predecessors!
            if (!isPredecessor(signedQuery.Query))
            {
                MessageBox.Show(ServerApp._user.Username + " says " + whoSentQuery(signedQuery.Query)
                                                         + " is trying to screw the comunication! (whosentMethod)");
                return;
            }
            if (!queryIsVerified(signedQuery))
            {
                MessageBox.Show("Could not verify signed query.");
                return;
            }

            ServerApp._user.ReceivedFileMessages.Add(query);


            if (signedQuery.Query.ContactingServerUri.ElementAt(0).CompareTo(signedQuery.Query.Uris.ElementAt(0)) == 0)
            {
                //In this case the sender of the message is the same that originated it. 
                //So if an attacker tries to do this he'll be the one receiving the responses in the end.
                //But the true requester will still get the right response.
                sendMessage = true;

            }
            else
            {
                sendMessage = consensus(query);
            }

            if (sendMessage)
            {
                keepOrForward(query);
                ServerApp._user.SentMessages.Add(signedQuery.Query.Id);
            }
        }

        private void keepOrForward(QueryByFile query)
        {
            RemoteAsyncShareObjectDelegate del;
            ServerToServerServices friend;
            Friend predecessor = null;
            QueryByFile q1;
            List<String> contacting = new List<string>();
            contacting.Add(ServerApp._myUri);
            if (ServerApp._user.Username[0] == query.Name[0])
            {
                //should store in redirection
                MessageBox.Show(ServerApp._user.Username + " will put uri on redirection list. (obj=now)");
                ServerApp._user.addRedirection(new RedirectionFile(query.Name, query.Uris.ElementAt(0)));
                return;
            }
            else
            {
                foreach (Friend node in ServerApp._user.Friends)
                    if (!node.SucessorSwarm)
                    {
                        predecessor = node;
                        break;
                    }
                if (predecessor == null)
                {
                    MessageBox.Show(ServerApp._user.Username + " says : Inconsistent routing table");
                    return;
                }
                if (predecessor.Name[0] > ServerApp._user.Username[0] && query.Name[0] > predecessor.Name[0])
                {
                    //should store in redirection
                    MessageBox.Show(ServerApp._user.Username + " will put uri on redirection list. (before>now && obj>before)");
                    ServerApp._user.addRedirection(new RedirectionFile(query.Name, query.Uris.ElementAt(0)));
                    return;
                }
                if (query.Name[0] > predecessor.Name[0] && query.Name[0] < ServerApp._user.Username[0])
                {
                    //should store in redirection
                    MessageBox.Show(ServerApp._user.Username + " will put uri on redirection list. (obj>before && obj<now)");
                    ServerApp._user.addRedirection(new RedirectionFile(query.Name, query.Uris.ElementAt(0)));
                    return;
                }
                //if(lower[0] >= ServerApp._user.Username[0])

                //should continue sending
                foreach (Friend f in ServerApp._user.Friends)
                {
                    if (f.Uris.ElementAt(0) != null && f.SucessorSwarm)
                    {
                        if (f.Uris.ElementAt(0).CompareTo(query.ContactingServerUri.ElementAt(0)) != 0)
                        {
                            MessageBox.Show(ServerApp._user.Username + " sending a request to share to " + f.Name);
                            friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                                f.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));


                            q1 = new QueryByFile(query.Name, query.Uris, contacting,
                                (ServerApp._user.Username[0] > query.LowestId[0]) ? query.LowestId : ServerApp._user.Username, query.Id);

                            byte[] data = Encoding.Default.GetBytes(q1.ToString());
                            byte[] signature = ServerApp._rsaProvider.SignData(data, "SHA1");
                            SignedQueryByFile signedForwardQuery = new SignedQueryByFile(q1, signature);

                            del = new RemoteAsyncShareObjectDelegate(friend.shareObject);
                            del.BeginInvoke(signedForwardQuery, null, null);
                        }
                    }
                }

            }
        }
        

        private bool consensus(Query query)
        {
            int i, j, counting;
            //This list contains all the messages from PREDECESSORS with the same ID
            List<Query> messageList = getList(query);
            bool sendMessage = false;
            Query q1, q2;

            //Checks if it has received #predecessors/2 answers, if so it responds and stores Datetime on sentMessages
            //otherwise it adds query to receivedMessages
            

            MessageBox.Show("MessageCount = " + messageList.Count + " predecessors count = " + ServerApp._user.Friends.Count(x => !x.SucessorSwarm));

            if (messageList.Count > ServerApp._user.Friends.Count(x => !x.SucessorSwarm) / 2)
            {
                //anwer message
                //do consensus thing------------------------------

                for (i = 0; i < messageList.Count; i++)
                {
                    q1 = messageList.ElementAt(i);
                    counting = 0;
                    for (j = i + 1; j < messageList.Count; j++)
                    {
                        q2 = messageList.ElementAt(j);
                        if (q1.CompareTo(q2))
                            counting++;

                    }
                    if (counting + 1 >= ServerApp._user.Friends.Count(x => !x.SucessorSwarm) / 2)
                    {
                        sendMessage = true;
                        break;
                    }
                }
                if(sendMessage == false)
                    MessageBox.Show(ServerApp._user.Username + " : There are enough messages but still no consensus.");
                //-------------------------------------------------
            }
            else
            {
                MessageBox.Show(ServerApp._user.Username + " : Still not enough messages to do consensus.");
            }
            return sendMessage;
        }

        public void getName(String responseUri)
        {
            ServerToServerServices friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                responseUri + "/" + ServicesNames.ServerToServerServicesName));
            RemoteAsyncChangeNameInFriendsDelegate remoteDel = new RemoteAsyncChangeNameInFriendsDelegate(friend.changeNameOfFriend);
            remoteDel.BeginInvoke(ServerApp._user.Username, ServerApp._primaryURI, null, null);
        }

        public void lookupname(SignedQueryByName incomingQuery)
        {
            ServerToServerServices friend;
            RemoteAsyncLookupNameDelegate remoteDel;
            QueryByName q = incomingQuery.Query;
            QueryByName newQ = new QueryByName(q.Name, q.Uris, new List<string>(),q.Id);
            newQ.ContactingServerUri.Clear();
            newQ.ContactingServerUri.Add(ServerApp._primaryURI);
            bool sendMessage = false;

            System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " : Received the request.");
            
            if (ServerApp._user.SentMessages.Contains(incomingQuery.Query.Id))
            {
                MessageBox.Show(ServerApp._user.Username + " : Message was already sent so the request was discarted!");
                return;
            }

            if (ServerApp._user.ReceivedNameMessages.Contains(incomingQuery.Query))
            {
                MessageBox.Show(ServerApp._user.Username + " : Message was already received so the request was discarted!");
                return;
            }

            //only accepts messages from predecessors!
            if (!isPredecessor(incomingQuery.Query))
            {
                MessageBox.Show(ServerApp._user.Username + " says " + whoSentQuery(incomingQuery.Query)
                                                         + " is trying to screw the comunication!");
                return;
            }

            if (!isLookupQueryValid(incomingQuery)) {
                MessageBox.Show(ServerApp._user.Username + " Could not verify lookup message");
                return;
            }

            ServerApp._user.ReceivedNameMessages.Add(q);

            //Is it receiving this message from the original requester? if so doesnt need consensus. (check expl @ shareObject())
            if (incomingQuery.Query.ContactingServerUri.ElementAt(0).CompareTo(incomingQuery.Query.Uris.ElementAt(0)) == 0)
            {
                //Does not need consensus
                MessageBox.Show(ServerApp._user.Username + ": Does not need consensus!");
                sendMessage = true;
            }
            else
            {
                //consensus
                sendMessage = consensus(incomingQuery.Query);

            }

            if (sendMessage)
            {
                MessageBox.Show(ServerApp._user.Username + ": Reached consensus!");
                if (ServerApp._user.Username[0] == q.Name[0])
                {
                    sendResponseToLookupQuery(q);
                    return;
                }


                /* sign the query which we send to our friends */
                byte[] queryData = Encoding.Default.GetBytes(newQ.ToString());
                byte[] signature = ServerApp._rsaProvider.SignData(queryData, "SHA1");
                SignedQueryByName signedNewNameQuery = new SignedQueryByName(newQ, signature);

                foreach (Friend i in ServerApp._user.Friends)
                {
                    if (i.Uris.ElementAt(0) != null && i.SucessorSwarm)
                    {
                        if (i.Uris.ElementAt(0).CompareTo(q.ContactingServerUri.ElementAt(0)) != 0)
                        {
                            System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " : forwarding to : " + i.Name);
                            friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                                i.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));
                            remoteDel = new RemoteAsyncLookupNameDelegate(friend.lookupname);
                            remoteDel.BeginInvoke(signedNewNameQuery, null, null);
                        }

                    }
                }

                ServerApp._user.SentMessages.Add(incomingQuery.Query.Id);
            }
            else
                MessageBox.Show(ServerApp._user + ": Didn't reach consensus yet!");
        }

        private string whoSentQuery(Query query)
        {
            foreach (Friend f in ServerApp._user.Friends)
            {
                if (f.Uris.ElementAt(0).CompareTo(query.ContactingServerUri.ElementAt(0)) == 0)
                    return f.Name;
            }
            MessageBox.Show(ServerApp._user.Username + "WhoIsUri called with invalid node uri");
            return "";
        }

        private bool isLookupQueryValid(SignedQueryByName signedQuery)
        {
            UserEntry responder = ServerApp._pkiCommunicator.GetVerifiedUserPublicKey(whoSentQuery(signedQuery.Query));
            if (responder != null)
            {
                RSACryptoServiceProvider responderProvider = new RSACryptoServiceProvider();
                responderProvider.FromXmlString(responder.PubKey);
                byte[] data = Encoding.Default.GetBytes(signedQuery.Query.ToString());
                return responderProvider.VerifyData(data, "SHA1", signedQuery.Signature);
            }
            else
                return false;
        }

        private void sendResponseToLookupQuery(QueryByName q)
        {
            ServerToServerServices origin;
            RemoteAsyncLookupNameResponseDelegate remoteResDel;

            System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + "@" + ServerApp._primaryURI + " : Its my node name!");
            String i = q.Uris.ElementAt(0);
            origin = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                    i + "/" + ServicesNames.ServerToServerServicesName));
            remoteResDel = new RemoteAsyncLookupNameResponseDelegate(origin.lookupNameResponse);

            /* sign response */
            string data = getResponseDataForHash();
            byte[] bytestreamData = Encoding.Default.GetBytes(data);
            byte[] responseSignature = ServerApp._rsaProvider.SignData(bytestreamData, "SHA1");
            SignedLookupResponse signedLookupResponse =
                new SignedLookupResponse(ServerApp._user.Username, ServerApp._myUri,
                    ServerApp._user.RedirectionList, responseSignature);
            remoteResDel.BeginInvoke(signedLookupResponse, null, null);
        }

        private string getResponseDataForHash()
        {
            string data = ServerApp._user.Username + ServerApp._myUri;
            foreach (RedirectionFile f in ServerApp._user.RedirectionList)
                data += f.ToString();
            return data;
        }

        public void lookupNameResponse(SignedLookupResponse signedResponse)
        {
            if (IsResponseLookupNameMessageValid(signedResponse))
            {
                string name = signedResponse.Username;
                string uri = signedResponse.Uri;
                List<RedirectionFile> redList = signedResponse.FileList;
                
                ClientServices cliente = (ClientServices)Activator.GetObject(
                    typeof(ClientServices),
                    ServerApp._clientUri + "/" + ServicesNames.ClientServicesName);
                cliente.lookupNameResponse(name, uri, redList);
            }
        }

        private bool IsResponseLookupNameMessageValid(SignedLookupResponse signedResponse)
        {
            bool result = false;

            UserEntry responder = ServerApp._pkiCommunicator.GetVerifiedUserPublicKey(signedResponse.Username);
            if (responder != null)
            {
                
                RSACryptoServiceProvider responderProvider = new RSACryptoServiceProvider();
                responderProvider.FromXmlString(responder.PubKey);
                string data = signedResponse.Username + signedResponse.Uri;
                foreach (RedirectionFile f in signedResponse.FileList)
                    data += f.ToString();
                byte[] bytedata = Encoding.Default.GetBytes(data);
                if (responderProvider.VerifyData(bytedata, "SHA1", signedResponse.Signature))
                    result = true;
                else
                    result = false;
            }
            
            return result;
        }

        public Friend acceptFriendRequest(Friend friend)
        {
            try
            {
                friend.SucessorSwarm = true;
                ServerApp._user.addFriend(friend);
                //ServerApp._user.PendingFriends.Remove(friend);
            }
            catch (DuplicatePendingFriendException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return null;
            }

            ClientServices client = (ClientServices)Activator.GetObject(typeof(ClientServices),
               ServerApp._clientUri + "/" + ServicesNames.ClientServicesName);

            RemoteAsyncFriendDelegate del = new RemoteAsyncFriendDelegate(client.acceptFriendRequest);

            del.BeginInvoke(friend, null, null);

            string[] myUris = { ServerApp._primaryURI };//, ServerApp._replicaOneURI, ServerApp._replicaTwoURI };

            return new Friend(ServerApp._user.Username, new List<string>(myUris));
        }

        public void changeFriendUri(string oldFriendUri, string newFriendUri)
        {
            try
            {
                Friend friend = ServerApp._user.getFriendByUri(oldFriendUri);
                friend.setPrimaryUri(newFriendUri);
            }
            catch (InvalidFriendUriException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        public void FreezeService(int time)
        {
            Thread.Sleep(time);
        }

        private List<Query> getList(Query query)
        {
            List<Query> messageList = new List<Query>();
            List<Query> messages = new List<Query>();
            if (query is QueryByFile)
            {
                foreach (QueryByFile q in ServerApp._user.ReceivedFileMessages)
                    messages.Add(q);
            }
            else
            {
                foreach (QueryByName q in ServerApp._user.ReceivedNameMessages)
                    messages.Add(q);
            }

            //Only returns messages wich have the same ID and belong to predecessor nodes.
            foreach (Query q in messages)
            {
                if (q.Id.Equals(query.Id))
                {
                    messageList.Add(q);
                }
            }
            return messageList;
        }
    }
}
