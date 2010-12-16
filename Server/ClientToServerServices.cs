using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PADIbookCommonLib;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace Server
{
    class ClientToServerServicesObject : MarshalByRefObject, ClientToServerServices
    {

        public void shareObject(ObjectFile file)
        {
            ServerToServerServices friend;
            RemoteAsyncShareObjectDelegate del;
            ServerApp._user.addObject(file);
            List<String> uri = new List<string>();
            List<String> contacting = new List<string>();

            contacting.Add(ServerApp._primaryURI);
            uri.Add(ServerApp._primaryURI);
            QueryByFile query = new QueryByFile(file.FileName, uri, contacting, ServerApp._user.Username,DateTime.Now);

            MessageBox.Show(ServerApp._myUri + " wants to share " + file.FileName);

            foreach (Friend f in ServerApp._user.Friends)
            {
                if (f.Uris.ElementAt(0) != null && f.SucessorSwarm)
                {
                    friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                        f.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));

                    MessageBox.Show(ServerApp._myUri + " sends to " + f.Name);

                    del = new RemoteAsyncShareObjectDelegate(friend.shareObject);
                    del.BeginInvoke(query, null, null);
                }
            }

        }

        // mudar pra esperar pela resposta de pelo menos uma replca e so depois
        // alterar.
        public Post sendPost(Post post)
        {
            if (!ServerApp._serviceAvailable)
            {
                return null;
            }

            //string[] replicasURIs = { ServerApp._replicaOneURI, ServerApp._replicaTwoURI };
            //ReplicationServices replica;
            //RemoteAsyncPostDelegate remoteDel;

            ServerApp._user.addPost(post);

            /*foreach (string uri in replicasURIs)
            {
                if (uri != null)
                {
                    replica = ((ReplicationServices)Activator.GetObject(typeof(ReplicationServices), uri + "/" + ServicesNames.ReplicationServicesName));
                    remoteDel = new RemoteAsyncPostDelegate(replica.sendPost);
                    remoteDel.BeginInvoke(post, null, null);
                }
            }*/

            return post;
        }

        public void refreshAllFriendNames()
        {
            ServerToServerServices friend;
            RemoteAsyncGetNameDelegate del;
            ClientServices client;

            if(!ServerApp._serviceAvailable) {
                client = ((ClientServices)Activator.GetObject(typeof(ClientServices),
                    ServerApp._clientUri + "/" + ServicesNames.ClientServicesName));
                new RemoteAsyncServiceUnavailableDelegate(client.serviceUnavailable).BeginInvoke(null, null);
                return;
            }


            foreach (Friend f in ServerApp._user.Friends)
            {
                if (f.Uris.ElementAt(0) != null)
                {
                    friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                        f.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));

                    del = new RemoteAsyncGetNameDelegate(friend.getName);
                    del.BeginInvoke(ServerApp._primaryURI, null, null);
                }
            }
        }

        public void getFriendsPosts()
        {
            ClientServices client;
            ServerToServerServices friend;
            RemoteAsyncSendFriendPostsDelegate remoteDel;

            if (!ServerApp._serviceAvailable)
            {
                client = ((ClientServices)Activator.GetObject(typeof(ClientServices),
                    ServerApp._clientUri + "/" + ServicesNames.ClientServicesName));
                new RemoteAsyncServiceUnavailableDelegate(client.serviceUnavailable).BeginInvoke(null, null);
                return;
            }

            foreach (Friend i in ServerApp._user.Friends)
            {
                if (i.Uris.ElementAt(0) != null)
                {
                    friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                        i.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));
                    remoteDel = new RemoteAsyncSendFriendPostsDelegate(friend.getPosts);
                    remoteDel.BeginInvoke(RemoteAsyncgetFriendsPostsCallBack, null);
                }
            }
        }

        public void lookupname(QueryByName q)
        {
            //System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " lookupname " + q.Name);
            ServerToServerServices friend;
            RemoteAsyncLookupNameDelegate remoteDel;
            ClientServices client;

            if (!ServerApp._serviceAvailable)
            {
                client = ((ClientServices)Activator.GetObject(typeof(ClientServices),
                    ServerApp._clientUri + "/" + ServicesNames.ClientServicesName));
                new RemoteAsyncServiceUnavailableDelegate(client.serviceUnavailable).BeginInvoke(null, null);
                return;
            }

            //One of this is the response uri and the other is the previous uri
            q.Uris.Add(ServerApp._primaryURI);
            //q.Uris.Add(ServerApp._replicaOneURI);
            //q.Uris.Add(ServerApp._replicaTwoURI);

            q.ContactingServerUri.Add(ServerApp._primaryURI);
            //q.ContactingServerUri.Add(ServerApp._replicaOneURI);
            //q.ContactingServerUri.Add(ServerApp._replicaTwoURI);

            foreach (Friend i in ServerApp._user.Friends)
            {
                if (i.Uris.ElementAt(0) != null && i.SucessorSwarm) //only sends to the sucessor nodes
                {

                    //System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " manda lookupName para " + i.Uris.ElementAt(0));
                    friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                        i.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));
                    remoteDel = new RemoteAsyncLookupNameDelegate(friend.lookupname);
                    remoteDel.BeginInvoke(q, null, null);
                }
            }
        }

        /*public void lookupInterest(QueryByInterest q)
        {
            //System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " lookupInterest " + q.Interest);
            ServerToServerServices friend;
            RemoteAsyncLookupInterestDelegate remoteDel;
            ClientServices client;

            if (!ServerApp._serviceAvailable)
            {
                client = ((ClientServices)Activator.GetObject(typeof(ClientServices),
                    ServerApp._clientUri + "/" + ServicesNames.ClientServicesName));
                new RemoteAsyncServiceUnavailableDelegate(client.serviceUnavailable).BeginInvoke(null, null);
                return;
            }

            q.Uris.Add(ServerApp._primaryURI);
            //q.Uris.Add(ServerApp._replicaOneURI);
            //q.Uris.Add(ServerApp._replicaTwoURI);

            q.ContactingServerUri.Add(ServerApp._primaryURI);
            //q.ContactingServerUri.Add(ServerApp._replicaOneURI);
            //q.ContactingServerUri.Add(ServerApp._replicaTwoURI);

            foreach (Friend i in ServerApp._user.Friends)
            {
                if (i.Uris.ElementAt(0) != null)
                {
                    //System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " manda lookupInterest para " + i.Uris.ElementAt(0));
                    friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                        i.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));
                    remoteDel = new RemoteAsyncLookupInterestDelegate(friend.lookupInterest);
                    remoteDel.BeginInvoke(q, null, null);
                }
            }
        }

        public void lookupSexAge(QueryByGenderAge q)
        {
            //System.Windows.Forms.MessageBox.Show("lookup :Client server");
            ServerToServerServices friend;
            RemoteAsyncLookupSexAgeDelegate remoteDel;
            ClientServices client;

            if (!ServerApp._serviceAvailable)
            {
                client = ((ClientServices)Activator.GetObject(typeof(ClientServices),
                    ServerApp._clientUri + "/" + ServicesNames.ClientServicesName));
                new RemoteAsyncServiceUnavailableDelegate(client.serviceUnavailable).BeginInvoke(null,null);
                return;
            }

            q.Uris.Add(ServerApp._primaryURI);
            //q.Uris.Add(ServerApp._replicaOneURI);
            //q.Uris.Add(ServerApp._replicaTwoURI);

            q.ContactingServerUri.Add(ServerApp._primaryURI);
            //q.ContactingServerUri.Add(ServerApp._replicaOneURI);
            //q.ContactingServerUri.Add(ServerApp._replicaTwoURI);

            foreach (Friend i in ServerApp._user.Friends)
            {
                if (i.Uris.ElementAt(0) != null)
                {
                    //System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " : Server recebeu e manda lookupSexAge para " + i.Uris.ElementAt(0));
                    friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                        i.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));
                    remoteDel = new RemoteAsyncLookupSexAgeDelegate(friend.lookupSexAge);
                    //System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " : client server : sending to " + i.Uris.ElementAt(0));
                    remoteDel.BeginInvoke(q, null, null);
                }
            }
        }

        /*public void RemoteAsyncLookupNameCallBack(IAsyncResult ar) { System.Windows.Forms.MessageBox.Show("Recebi de volta"); }*/

        public void RemoteAsyncgetFriendsPostsCallBack(IAsyncResult ar)
        {
            try
            {
                RemoteAsyncSendFriendPostsDelegate del = (RemoteAsyncSendFriendPostsDelegate)((AsyncResult)ar).AsyncDelegate;

                ClientServices cliente = (ClientServices)Activator.GetObject(
                typeof(ClientServices),
                ServerApp._clientUri + "/" + ServicesNames.ClientServicesName);

                List<Post> listaposts = del.EndInvoke(ar);
                cliente.receiveFriendPosts(listaposts);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("FriendsPostsCallBack : " + e.Message
                  + "\r\n" + e.StackTrace + "\r\n" + e.InnerException);
            }
            return;
        }

        public void modifyUserProfile(String name, String spoofaddr)
        {
            ClientServices client;

            ServerApp._user.SpoofAdress = spoofaddr;

            if (!ServerApp._serviceAvailable)
            {
                client = ((ClientServices)Activator.GetObject(typeof(ClientServices),
                    ServerApp._clientUri + "/" + ServicesNames.ClientServicesName));
                new RemoteAsyncServiceUnavailableDelegate(client.serviceUnavailable).BeginInvoke(null,null);
                return;
            }

            //string[] replicasURIs = { ServerApp._replicaOneURI, ServerApp._replicaTwoURI };
            //ReplicationServices replica;
            //RemoteAsyncModifyUserProfileDelegate remoteDel;
            ServerToServerServices friend;
            //RemoteAsyncLookupInterestDelegate generalDel;
            //QueryByInterest q = new QueryByInterest(Interest.Cars, new List<String>(), new List<String>());

            //int interestsfound = 0;

            /*foreach (string uri in replicasURIs)
            {
                if (uri != null)
                {
                    //System.Windows.Forms.MessageBox.Show("vai replicar no " + uri);
                    replica = ((ReplicationServices)Activator.GetObject(typeof(ReplicationServices), uri + "/" + ServicesNames.ReplicationServicesName));
                    remoteDel = new RemoteAsyncModifyUserProfileDelegate(replica.modifyUserProfile);
                    remoteDel.BeginInvoke(time, name, gen, inter, null, null);
                }
            }*/

            if (ServerApp._user.Username.CompareTo(name) != 0)
            {
                foreach (Friend i in ServerApp._user.Friends)
                {
                    if (i.Uris.ElementAt(0) != null)
                    {
                        //System.Windows.Forms.MessageBox.Show("Mudou de nome->manda changeNameOnF para " + i.Uris.ElementAt(0));
                        friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                            i.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));
                        RemoteAsyncChangeNameInFriendsDelegate superDelegate = new RemoteAsyncChangeNameInFriendsDelegate(friend.changeNameOfFriend);
                        superDelegate.BeginInvoke(name, ServerApp._primaryURI, null, null);
                    }
                }
                ServerApp._user.Username = name;
            }

            //ServerApp._user.Gender = gen;
            //ServerApp._user.BirthDate = time;

            //if (ServerApp._user.IsRegisteredInAnelGeral)
              //  MessageBox.Show(ServerApp._user.Username + " : Ja estou no anel geral");

/*            foreach (Interest i in inter)
            {
                foreach (RegisteredInterest r in ServerApp._user.RegInterests)
                {
                    //TODO acabar o registo de interesses
                    if (i.Equals(r.Interest))
                    {
                        interestsfound++;
                        break;
                    }
                }
            }

            if (inter.Count != interestsfound)
            {
                System.Windows.Forms.MessageBox.Show("tenho de registar interesses.");
                /*Vai fazer lookup do interesse por uma via ligeiramente diferente*/
                /*Os lookups devolvem sempre o ultimo uri por onde passou*/
                //q.Interest = i;
                /*q.refreshTimeStamp();
                q.Uris.Add(ServerApp._primaryURI);
                q.Uris.Add(ServerApp._replicaOneURI);
                q.Uris.Add(ServerApp._replicaTwoURI);

                q.ContactingServerUri.Add(ServerApp._primaryURI);
                q.ContactingServerUri.Add(ServerApp._replicaOneURI);
                q.ContactingServerUri.Add(ServerApp._replicaTwoURI);

                foreach (Friend f in ServerApp._user.Friends)
                {
                    if (f.Uris.ElementAt(0) != null)
                    {
                        System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " : Server do cliente recebeu e manda lookupGRing para " + f.Uris.ElementAt(0));
                        friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                            f.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));
                        generalDel = new RemoteAsyncLookupInterestDelegate(friend.lookupGeneralRing);
                        generalDel.BeginInvoke(q, null, null);
                    }
                }

            }

        */
            //ServerApp._user.Interests = inter;
        }

        public User returnUserInstance()
        {
            if (!ServerApp._serviceAvailable)
                return null; ;

            return ServerApp._user;
        }

        public bool registerClient(string clientUri)
        {

            if (!ServerApp._serviceAvailable)
            {
                return false;
            }

            //ReplicationServices replica;
            //RemoteAsyncUriDelegate del;

            ServerApp._clientUri = clientUri;

            /*string[] myReplicas = { ServerApp._replicaOneURI, ServerApp._replicaTwoURI };

            foreach (string uri in myReplicas)
            {
                if (uri != null)
                {
                    replica = (ReplicationServices)Activator.GetObject(
                                typeof(ReplicationServices),
                                uri + "/" + ServicesNames.ReplicationServicesName);

                    del = new RemoteAsyncUriDelegate(replica.sendClientUri);
                    del.BeginInvoke(ServerApp._clientUri, null, null);
                }

            }*/

            return true;
        }

        //TODO: depois tem que ter callback
        public void sendFriendRequest(string friendUri)
        {
            ClientServices client;

            if (!ServerApp._serviceAvailable)
            {
                client = ((ClientServices)Activator.GetObject(typeof(ClientServices),
                    ServerApp._clientUri + "/" + ServicesNames.ClientServicesName));
                new RemoteAsyncServiceUnavailableDelegate(client.serviceUnavailable).BeginInvoke(null, null);
                return;
            }

            try
            {
                List<String> replicasUri = new List<string>();
                replicasUri.Add(ServerApp._myUri);
                //string[] replicasURIs = new string[] { ServerApp._primaryURI };
                //                      //ServerApp._replicaOneURI,ServerApp._replicaTwoURI };

                MessageBox.Show(ServerApp._user.Username + " -> ClientToServer : sendFriendRequest : " + ServerApp._user.Username + " - " + replicasUri.ElementAt(0));
                Friend friend = new Friend(ServerApp._user.Username,replicasUri,true);

                ServerToServerServices server = (ServerToServerServices)Activator.GetObject(
                    typeof(ServerToServerServices),
                    friendUri + "/" + ServicesNames.ServerToServerServicesName);

                RemoteAsyncFriendDelegate del =
                    new RemoteAsyncFriendDelegate(server.sendFriendRequest);

                System.Windows.Forms.MessageBox.Show("vou adicionar pending friend " + friendUri);
                del.BeginInvoke(friend, null, null);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        //TODO: tem que ir ao servidor do amigo retirar-se
        public void modifyUserFriends(List<Friend> fr)
        {
            ClientServices client;
            
            if (!ServerApp._serviceAvailable)
            {
                client = ((ClientServices)Activator.GetObject(typeof(ClientServices),
                    ServerApp._clientUri + "/" + ServicesNames.ClientServicesName));
                new RemoteAsyncServiceUnavailableDelegate(client.serviceUnavailable).BeginInvoke(null, null);
                return;
            }

            ServerApp._user.Friends = fr;
        }

        public Friend acceptFriendRequest(Friend friend)
        {
            ClientServices client;

            if (!ServerApp._serviceAvailable)
            {
                client = ((ClientServices)Activator.GetObject(typeof(ClientServices),
                    ServerApp._clientUri + "/" + ServicesNames.ClientServicesName));
                new RemoteAsyncServiceUnavailableDelegate(client.serviceUnavailable).BeginInvoke(null, null);
                return null;
            }

            string friendUri = friend.Uris.ElementAt(0);

            string[] myUris = { ServerApp._primaryURI};//, ServerApp._replicaOneURI, ServerApp._replicaTwoURI };

            Friend user = new Friend(ServerApp._user.Username, new List<string>(myUris));

            ServerToServerServices server = (ServerToServerServices)Activator.GetObject(
                                typeof(ServerToServerServices),
                                friendUri + "/" + ServicesNames.ServerToServerServicesName);

            AsyncCallback remoteCallback = new AsyncCallback(remoteAsyncAcceptFriendRequestCallback);
            RemoteAsyncFriendDelegate del = new RemoteAsyncFriendDelegate(server.acceptFriendRequest);
            del.BeginInvoke(user, remoteCallback, null);


            return null;
        }

        //TODO: falar com cliente
        private void remoteAsyncAcceptFriendRequestCallback(IAsyncResult ar)
        {
            //ReplicationServices replica;
            //RemoteAsyncFriendDelegate remoteDel;

            //string[] replicasURIs = { ServerApp._replicaOneURI, ServerApp._replicaTwoURI };

            try
            {
                RemoteAsyncFriendDelegate del = (RemoteAsyncFriendDelegate)((AsyncResult)ar).AsyncDelegate;

                Friend newFriend = del.EndInvoke(ar);

                if (newFriend == null)
                    return;

                //KLFK
                newFriend.SucessorSwarm = false;
                ServerApp._user.Friends.Add(newFriend);
                ServerApp._user.removePendingFriend(newFriend.Name);

                ClientServices client = (ClientServices)Activator.GetObject(typeof(ClientServices),
                ServerApp._clientUri + "/" + ServicesNames.ClientServicesName);

                del = new RemoteAsyncFriendDelegate(client.acceptFriendRequest);

                del.BeginInvoke(newFriend, null, null);

                /*foreach (string uri in replicasURIs)
                {
                    if (uri != null)
                    {
                        replica = ((ReplicationServices)Activator.GetObject(typeof(ReplicationServices),
                            uri + "/" + ServicesNames.ReplicationServicesName));
                        remoteDel = new RemoteAsyncFriendDelegate(replica.acceptFriendRequest);
                        remoteDel.BeginInvoke(newFriend, null, null);
                    }
                }*/
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message + " aqui1");
                return;
            }
        }

        public Friend removeFriendRequest(Friend friend)
        {
            if (!ServerApp._serviceAvailable)
                return null;


            try
            {
                ServerApp._user.removePendingFriend(friend.Name);
            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.Message + "aqui"); }

            return friend;
        }

      
    }
}
