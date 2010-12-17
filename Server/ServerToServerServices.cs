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
            //string[] replicasURIs = { null, I };

            //ReplicationServices replica;
            //RemoteAsyncFriendDelegate remoteDel;

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

            /*foreach (string uri in replicasURIs)
            {
                if (uri != null)
                {
                    replica = ((ReplicationServices)Activator.GetObject(typeof(ReplicationServices), uri + "/" + ServicesNames.ReplicationServicesName));
                    remoteDel = new RemoteAsyncFriendDelegate(replica.sendFriendRequest);
                    remoteDel.BeginInvoke(friend, null, null);
                }
            }*/

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
            MessageBox.Show(ServerApp._user.Username + "says that signedQuery.Query.Name=" + contactingName);
            UserEntry userToVerify = ServerApp._pkiCommunicator.GetVerifiedUserPublicKey(contactingName);
            if (userToVerify == null)
            {
                MessageBox.Show(ServerApp._user.Username + " : User to verify was null!");
                return false;
            }
            rsaWithPublicKeyOfRemoteUser.FromXmlString(userToVerify.PubKey);
            byte[] data = Encoding.Default.GetBytes(signedQuery.Query.ToString());
            return rsaWithPublicKeyOfRemoteUser.VerifyData(data, "SHA1", signedQuery.Signature);
        }

        public void shareObject(SignedQueryByFile signedQuery)
        {
            RemoteAsyncShareObjectDelegate del;
            ServerToServerServices friend;
            Friend predecessor = null;
            List<QueryByFile> messageList = new List<QueryByFile>();
            List<String> contacting = new List<string>();
            int i, j,counting;
            QueryByFile q1, q2;
            Boolean sendMessage = false;
            QueryByFile query = signedQuery.Query;

            MessageBox.Show(ServerApp._user.Username + " received a request to share " + query.Name);

            if (ServerApp._user.SentMessages.Contains(query.Id))
            {
                MessageBox.Show(ServerApp._user.Username + " : Message was already sent so the request was discarted!");
                return;
            }

            contacting.Add(ServerApp._myUri);

            if (!queryIsVerified(signedQuery))
            {
                MessageBox.Show("Could not verify signed query.");
                return;
            }
            ServerApp._user.ReceivedFileMessages.Add(query);

            //Checks if it has received #predecessors/2 answers, if so it responds and stores Datetime on sentMessages
            //otherwise it adds query to receivedMessages
            foreach (QueryByFile q in ServerApp._user.ReceivedFileMessages)
            {
                if (q.Id.Equals(query.Id)){
                    messageList.Add(q);
                }
            }

            MessageBox.Show("MessageCount = " + messageList.Count + " predecessors count = "+ ServerApp._user.Friends.Count(x => !x.SucessorSwarm));

            if (messageList.Count > ServerApp._user.Friends.Count(x => !x.SucessorSwarm) / 2)
            {
                //anwer message
                //do consensus thing------------------------------

                for (i = 0; i<messageList.Count;i++ )
                {
                    q1 = messageList.ElementAt(i);
                    counting = 0;
                    for (j = i + 1;j<messageList.Count ;j++ )
                    {
                        q2 = messageList.ElementAt(j);
                        if (q1.Name.CompareTo(q2.Name) == 0 &&
                            q1.LowestId.CompareTo(q2.LowestId) == 0 &&
                            q1.Uris.ElementAt(0).CompareTo(q2.Uris.ElementAt(0)) == 0)
                            counting++;

                    }
                    if (counting + 1 >= ServerApp._user.Friends.Count(x => !x.SucessorSwarm) / 2)
                    {
                        sendMessage = true;
                        break;
                    }
                }

                //-------------------------------------------------

                if(sendMessage){

                    if (ServerApp._user.Username[0] == query.Name[0])
                    {
                        //should store in redirection
                        MessageBox.Show(ServerApp._myUri + " will put uri on redirection list. (obj=now)");
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
                            MessageBox.Show(ServerApp._myUri + " Inconsistent routing table");
                            return;
                        }
                        if (predecessor.Name[0] > ServerApp._user.Username[0] && query.Name[0] > predecessor.Name[0])
                        {
                            //should store in redirection
                            MessageBox.Show(ServerApp._myUri + " will put uri on redirection list. (before>now && obj>before)");
                            ServerApp._user.addRedirection(new RedirectionFile(query.Name, query.Uris.ElementAt(0)));
                            return;
                        }
                        if (query.Name[0] > predecessor.Name[0] && query.Name[0] < ServerApp._user.Username[0])
                        {
                            //should store in redirection
                            MessageBox.Show(ServerApp._myUri + " will put uri on redirection list. (obj>before && obj<now)");
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
                                    //MessageBox.Show(ServerApp._myUri + " sending a request to share to " + f.Uris.ElementAt(0));
                                    friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                                        f.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));


                                    q1 = new QueryByFile(query.Name, query.Uris, contacting,
                                        (ServerApp._user.Username[0] > query.LowestId[0]) ? query.LowestId : ServerApp._user.Username,query.Id);
                                    
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
                else
                    MessageBox.Show(ServerApp._user.Username + " : There are enough messages but still no consensus.");
            }
            else
            {
                MessageBox.Show(ServerApp._user.Username + " : Still not enough messages to do consensus.");
            }

            //MessageBox.Show("MARCUS owns c#" + ServerApp._user.ReceivedFileMessages.Count(x => x.Id.Equals(query.Id)));
            //ServerApp._user.ReceivedMessages.Add(nounce);

            //MessageBox.Show(ServerApp._myUri + " testing " + file.FileName[0] + " vs " + ServerApp._user.Username[0]);

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
            ServerToServerServices origin;
            RemoteAsyncLookupNameDelegate remoteDel;
            RemoteAsyncLookupNameResponseDelegate remoteResDel;
            QueryByName q = incomingQuery.Query;
            QueryByName newQ = new QueryByName(q.Name, q.Uris, new List<string>(),q.Id);
            newQ.ContactingServerUri.Clear();
            newQ.ContactingServerUri.Add(ServerApp._primaryURI);

            foreach (Query qu in ServerApp._user.ReceivedNameMessages)
            {
                if (qu.Id.Equals(qu.Id))
                {
                    return;
                }
            }
            ServerApp._user.ReceivedNameMessages.Add(q);
            //System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " :O nome a testar e" + q.Name);

            /* verify incoming q before sending response */ 

            if (ServerApp._user.Username.CompareTo(q.Name) == 0)
            {
                //System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + "@" + ServerApp._primaryURI + " :Olha e o meu nome");
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
                        //System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " : Vou enviar a quem ainda n enviei : " + i.Uris.ElementAt(0));
                        friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                            i.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));
                        remoteDel = new RemoteAsyncLookupNameDelegate(friend.lookupname);
                        remoteDel.BeginInvoke(signedNewNameQuery, null, null);
                    }
                    
                }
            }
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
            /* verify before */
            string name = signedResponse.Username;
            string uri = signedResponse.Uri;
            List<RedirectionFile> redList = signedResponse.FileList;
            //System.Windows.Forms.MessageBox.Show("lookupNameResponse");
            ClientServices cliente = (ClientServices)Activator.GetObject(
                typeof(ClientServices),
                ServerApp._clientUri + "/" + ServicesNames.ClientServicesName);
            cliente.lookupNameResponse(name,uri,redList);
        }

        public Friend acceptFriendRequest(Friend friend)
        {
            //string[] replicasURIs = { ServerApp._replicaOneURI, ServerApp._replicaTwoURI };

            //ReplicationServices replica;
            //RemoteAsyncFriendDelegate remoteDel;

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

            /*foreach (string uri in replicasURIs)
            {
                if (uri != null)
                {
                    replica = ((ReplicationServices)Activator.GetObject(typeof(ReplicationServices), uri + "/" + ServicesNames.ReplicationServicesName));
                    remoteDel = new RemoteAsyncFriendDelegate(replica.acceptFriendRequest);
                    remoteDel.BeginInvoke(friend, null, null);
                }
            }*/

            ClientServices client = (ClientServices)Activator.GetObject(typeof(ClientServices),
               ServerApp._clientUri + "/" + ServicesNames.ClientServicesName);

            RemoteAsyncFriendDelegate del = new RemoteAsyncFriendDelegate(client.acceptFriendRequest);

            del.BeginInvoke(friend, null, null);

            string[] myUris = { ServerApp._primaryURI };//, ServerApp._replicaOneURI, ServerApp._replicaTwoURI };

            return new Friend(ServerApp._user.Username, new List<string>(myUris));
        }

        public void changeFriendUri(string oldFriendUri, string newFriendUri)
        {
            //string[] replicasURIs = { ServerApp._replicaOneURI, ServerApp._replicaTwoURI };

            //ReplicationServices replica;
            //RemoteAsyncChangeFriendUriDelegate remoteDel;

            //System.Windows.Forms.MessageBox.Show("tou aqui");
            try
            {
                Friend friend = ServerApp._user.getFriendByUri(oldFriendUri);
                //System.Windows.Forms.MessageBox.Show(friend.Name + " " + friend.Uris.ElementAt(0));
                friend.setPrimaryUri(newFriendUri);
                //System.Windows.Forms.MessageBox.Show(friend.Name + " " + friend.Uris.ElementAt(0));

                //System.Windows.Forms.MessageBox.Show("o novo uri do " + friend.Name + " e " + friend.Uris.ElementAt(0));
            }
            catch (InvalidFriendUriException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }

            /*foreach (string uri in replicasURIs)
            {
                if (uri != null)
                {
                    replica = ((ReplicationServices)Activator.GetObject(typeof(ReplicationServices),
                        uri + "/" + ServicesNames.ReplicationServicesName));
                    remoteDel = new RemoteAsyncChangeFriendUriDelegate(replica.changeFriendUri);
                    remoteDel.BeginInvoke(oldFriendUri, newFriendUri, null, null);
                }
            }*/
        }

        public void FreezeService(int time)
        {
            Thread.Sleep(time);
        }
    }
}
