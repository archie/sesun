using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void shareObject(ObjectFile file, String uri, DateTime nounce, String contactingUri, String lowest)
        {
            RemoteAsyncShareObjectDelegate del;
            ServerToServerServices friend;
            Friend predecessor = null;

            MessageBox.Show(ServerApp._myUri + " received a request to share " + file.FileName);

            foreach (DateTime d in ServerApp._user.ReceivedMessages)
            {
                if (d.Equals(nounce))
                {
                    MessageBox.Show(ServerApp._myUri + " received a request to share but message was REPEATED");
                    return;
                }
            }
            ServerApp._user.ReceivedMessages.Add(nounce);

            MessageBox.Show(ServerApp._myUri + " testing " + file.FileName[0] + " vs " + ServerApp._user.Username[0]);
            /*if (ServerApp._user.Username[0] >= file.FileName[0])
                MessageBox.Show("EQUAL");
            else
                MessageBox.Show("DIFFER");
            */
            if (ServerApp._user.Username[0] == file.FileName[0])
            {
                //should store in redirection
                MessageBox.Show(ServerApp._myUri + " will put uri on redirection list. (obj=now)");
                ServerApp._user.addRedirection(new RedirectionFile(file.FileName, uri));
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
                if (predecessor.Name[0] > ServerApp._user.Username[0] && file.FileName[0] > predecessor.Name[0])
                {
                    //should store in redirection
                    MessageBox.Show(ServerApp._myUri + " will put uri on redirection list. (before>now && obj>before)");
                    ServerApp._user.addRedirection(new RedirectionFile(file.FileName, uri));
                    return;
                }
                if (file.FileName[0] > predecessor.Name[0] && file.FileName[0] < ServerApp._user.Username[0])
                {
                    //should store in redirection
                    MessageBox.Show(ServerApp._myUri + " will put uri on redirection list. (obj>before && obj<now)");
                    ServerApp._user.addRedirection(new RedirectionFile(file.FileName, uri));
                    return;
                }
                //if(lower[0] >= ServerApp._user.Username[0])

                //should continue sending
                foreach (Friend f in ServerApp._user.Friends)
                {
                    if (f.Uris.ElementAt(0) != null && f.SucessorSwarm)
                    {
                        if (f.Uris.ElementAt(0).CompareTo(contactingUri) != 0)
                        {
                            //MessageBox.Show(ServerApp._myUri + " sending a request to share to " + f.Uris.ElementAt(0));
                            friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                                f.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));

                            del = new RemoteAsyncShareObjectDelegate(friend.shareObject);
                            del.BeginInvoke(file, uri, nounce, ServerApp._myUri,
                                (ServerApp._user.Username[0] > lowest[0]) ? lowest : ServerApp._user.Username, null, null);
                        }
                    }
                }
            }
        }

        public void getName(String responseUri)
        {
            ServerToServerServices friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                responseUri + "/" + ServicesNames.ServerToServerServicesName));
            RemoteAsyncChangeNameInFriendsDelegate remoteDel = new RemoteAsyncChangeNameInFriendsDelegate(friend.changeNameOfFriend);
            remoteDel.BeginInvoke(ServerApp._user.Username, ServerApp._primaryURI, null, null);
        }

        /*public void lookupSexAge(QueryByGenderAge q)
        {
            ServerToServerServices friend;
            ServerToServerServices origin;
            RemoteAsyncLookupSexAgeDelegate remoteDel;
            RemoteAsyncLookupSexAgeDelegate remoteResDel;

            QueryByGenderAge newQ = new QueryByGenderAge(q.Gender, q.Age, q.Uris, new List<string>());
            newQ.ContactingServerUri.Clear();
            newQ.ContactingServerUri.Add(ServerApp._primaryURI);
            //newQ.ContactingServerUri.Add(ServerApp._replicaOneURI);
            //newQ.ContactingServerUri.Add(ServerApp._replicaTwoURI);

            foreach (DateTime d in ServerApp._user.ReceivedMessages)
            {
                if (d.Equals(q.Id))
                {
                    return;
                }
            }
            ServerApp._user.ReceivedMessages.Add(q.Id);

            if (ServerApp._user.Gender.CompareTo(q.Gender) == 0 && ServerApp._user.Age == q.Age)
            {
                String i = q.Uris.ElementAt(0);
                q.Name = ServerApp._user.Username;
                origin = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                        i + "/" + ServicesNames.ServerToServerServicesName));
                remoteResDel = new RemoteAsyncLookupSexAgeDelegate(origin.lookupSexAgeResponse);
                remoteResDel.BeginInvoke(q, null, null);
                //}
            }

            foreach (Friend i in ServerApp._user.Friends)
            {
                if (i.Uris.ElementAt(0) != null)
                {
                    if (i.Uris.ElementAt(0).CompareTo(q.ContactingServerUri.ElementAt(0)) != 0)
                    {
                        friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                            i.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));
                        remoteDel = new RemoteAsyncLookupSexAgeDelegate(friend.lookupSexAge);
                        remoteDel.BeginInvoke(newQ, null, null);
                    }
                }
            }
        }

        public void lookupInterest(QueryByInterest q)
        {
            ServerToServerServices friend;
            ServerToServerServices origin;
            RemoteAsyncLookupInterestDelegate remoteDel;
            RemoteAsyncLookupInterestDelegate remoteResDel;

            QueryByInterest newQ = new QueryByInterest(ServerApp._user.Username,q.Interest,q.Uris,new List<string>());
            newQ.ContactingServerUri.Clear();
            newQ.ContactingServerUri.Add(ServerApp._primaryURI);
            //newQ.ContactingServerUri.Add(ServerApp._replicaOneURI);
            //newQ.ContactingServerUri.Add(ServerApp._replicaTwoURI);

            foreach (DateTime d in ServerApp._user.ReceivedMessages)
            {
                if (d.Equals(q.Id))
                {
                    return;
                }
            }
            ServerApp._user.ReceivedMessages.Add(q.Id);
            //System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " :O interesse a testar e" + q.Interest);

            foreach (Interest i in ServerApp._user.Interests)
            {
                if (i.ToString().CompareTo(q.Interest) == 0)
                {
                    //System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " :Olha, tenho esse interesse! Que giro...");
                    q.Name = ServerApp._user.Username;
                    origin = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                            q.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));
                    remoteResDel = new RemoteAsyncLookupInterestDelegate(origin.lookupInterestResponse);
                    remoteResDel.BeginInvoke(q, null, null);
                }
            }

            foreach (Friend i in ServerApp._user.Friends)
            {
                if (i.Uris.ElementAt(0) != null)
                {
                    if (i.Uris.ElementAt(0).CompareTo(q.ContactingServerUri.ElementAt(0)) != 0)
                    {
                        //System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " : I'll send it whom I havent sent yet : " + i.Uris.ElementAt(0));
                        friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                            i.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));
                        remoteDel = new RemoteAsyncLookupInterestDelegate(friend.lookupInterest);
                        remoteDel.BeginInvoke(newQ, null, null);
                    }
                    //else System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " : I dont send to who sent it to me : " + i.Uris.ElementAt(0));
                }
            }
        }*/

        public void lookupname(QueryByName q)
        {
            ServerToServerServices friend;
            ServerToServerServices origin;
            RemoteAsyncLookupNameDelegate remoteDel;
            RemoteAsyncLookupNameResponseDelegate remoteResDel;

            QueryByName newQ = new QueryByName(q.Name, q.Uris, new List<string>());
            newQ.ContactingServerUri.Clear();
            newQ.ContactingServerUri.Add(ServerApp._primaryURI);
            //newQ.ContactingServerUri.Add(ServerApp._replicaOneURI);
            //newQ.ContactingServerUri.Add(ServerApp._replicaTwoURI);

            foreach (DateTime d in ServerApp._user.ReceivedMessages)
            {
                if (d.Equals(q.Id))
                {
                    return;
                }
            }
            ServerApp._user.ReceivedMessages.Add(q.Id);
            //System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " :O nome a testar e" + q.Name);

            if (ServerApp._user.Username.CompareTo(q.Name) == 0)
            {
                //System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + "@" + ServerApp._primaryURI + " :Olha e o meu nome");
                String i = q.Uris.ElementAt(0);
                origin = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                        i + "/" + ServicesNames.ServerToServerServicesName));
                remoteResDel = new RemoteAsyncLookupNameResponseDelegate(origin.lookupNameResponse);
                remoteResDel.BeginInvoke(ServerApp._user.Username,ServerApp._myUri,ServerApp._user.RedirectionList, null, null);
            }

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
                        remoteDel.BeginInvoke(newQ, null, null);
                    }
                    //else System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " : Nao mando a quem me enviou : " + i.Uris.ElementAt(0));
                }
            }
        }

        /*TODO : por a funcionar de maneira diferente pois faz efeito tampao
        public void lookupGeneralRing(QueryByInterest q)
        {
            ServerToServerServices friend;
            ServerToServerServices origin;
            RemoteAsyncLookupInterestDelegate remoteDel;
            RemoteAsyncLookupInterestDelegate remoteResDel;

            QueryByInterest newQ = new QueryByInterest(q.Interest, q.Uris, new List<string>());
            newQ.ContactingServerUri.Clear();
            newQ.Name = ServerApp._user.Username;
            newQ.ContactingServerUri.Add(ServerApp._primaryURI);
            newQ.ContactingServerUri.Add(ServerApp._replicaOneURI);
            newQ.ContactingServerUri.Add(ServerApp._replicaTwoURI);

            foreach (DateTime d in ServerApp._user.ReceivedMessages)
            {
                if (d.Equals(q.Id))
                {
                    System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " :Mensagem repetida");
                    return;
                }
            }
            ServerApp._user.ReceivedMessages.Add(q.Id);

            if (ServerApp._user.IsRegisteredInAnelGeral)
            {
                System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " : estou registado, vou responder");
                String i = q.Uris.ElementAt(0);
                origin = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                        i + "/" + ServicesNames.ServerToServerServicesName));
                remoteResDel = new RemoteAsyncLookupInterestDelegate(origin.lookupGeneralRingResponse);
                remoteResDel.BeginInvoke(newQ, null, null);
            }
            else
            {
                foreach (Friend i in ServerApp._user.Friends)
                {
                    if (i.Uris.ElementAt(0) != null)
                    {
                        if (i.Uris.ElementAt(0).CompareTo(q.ContactingServerUri.ElementAt(0)) != 0)
                        {
                            System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " : Vou enviar a quem ainda n enviei : " + i.Uris.ElementAt(0));
                            friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                                i.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));
                            remoteDel = new RemoteAsyncLookupInterestDelegate(friend.lookupGeneralRing);
                            remoteDel.BeginInvoke(newQ, null, null);
                        }
                        else System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " : Nao mando a quem me enviou : " + i.Uris.ElementAt(0));
                    }
                }
            }
        }*/

        public void lookupNameResponse(String name, String uri, List<RedirectionFile> redList)
        {
            //System.Windows.Forms.MessageBox.Show("lookupNameResponse");
            ClientServices cliente = (ClientServices)Activator.GetObject(
                typeof(ClientServices),
                ServerApp._clientUri + "/" + ServicesNames.ClientServicesName);
            cliente.lookupNameResponse(name,uri,redList);
        }

        /*public void lookupInterestResponse(QueryByInterest q)
        {
            //System.Windows.Forms.MessageBox.Show("lookupInterestResponse");
            ClientServices cliente = (ClientServices)Activator.GetObject(
                typeof(ClientServices),
                ServerApp._clientUri + "/" + ServicesNames.ClientServicesName);
            cliente.lookupInterestResponse(q);
        }*/

        //TODO : paper says this doesnt exist
        /*public void lookupGeneralRingResponse(QueryByInterest q)
        {

            System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " : " + q.Name + " is in the lookupGr! Uri = " + q.ContactingServerUri.ElementAt(0));
            MessageRegister message = new MessageRegister(ServerApp._user.Username, new List<String>(), new List<String>());
            Boolean found;
            ServerToServerServices friend;
            RemoteAsyncRegisterInterestInRingDelegate remoteDel;


            foreach (DateTime d in ServerApp._user.ReceivedMessages)
            {
                if (d.Equals(q.Id))
                {
                    System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " :Mensagem repetida");
                    return;
                }
            }
            ServerApp._user.ReceivedMessages.Add(q.Id);

            message.ContactingServerUri.Add(ServerApp._primaryURI);
            message.Uris.Add(ServerApp._primaryURI);
            message.Name = ServerApp._user.Username;

            found = false;

            foreach (Interest i in ServerApp._user.Interests)
            {
                found = false;
                foreach (RegisteredInterest r in ServerApp._user.RegInterests)
                {
                    //TODO acabar o registo de interesses
                    if (i.Equals(r.Interest))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    message.Interests.Add(i);
            }

            System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " : Vou mandar os interesses por reg para o anel : " + q.Name);
            friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                 q.ContactingServerUri.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));
            remoteDel = new RemoteAsyncRegisterInterestInRingDelegate(friend.registerInterestInRing);
            remoteDel.BeginInvoke(message, null, null);

            //TODO : register no interesse
        }*/

        /*public void ChangeInterestRingFriends(MessageInsertRing message)
        {
            Boolean found = false;
            RegisteredInterest reg = new RegisteredInterest(message.Interest, message.Before, message.After);
            foreach (RegisteredInterest r in ServerApp._user.RegInterests)
            {
                if (r.Interest.Equals(message.Interest))
                {
                    if (message.Actualize == 1)
                    {
                        r.RingBeforeUri = message.Before;
                        return;
                    }
                    found = true;
                    break;
                }
            }
            if(found==false)
                ServerApp._user.RegInterests.Add(reg);
        }
        public void ChangeGeneralRingFriends(MessageInsertRing message)
        {//se nao tiver la
        }

        public void registerInterestInRing(MessageRegister message)
        {
            System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + ": recebi uma messageRegister de " + message.Name);
            Boolean found;
            String nextUriAux;
            List<Interest> newInter = new List<Interest>();
            MessageInsertRing mreg;
            ServerToServerServices friend;
            RemoteAsynChangeRingDelegate remoteDel;
            RemoteAsynChangeRingDelegate remoteDel2;
            RemoteAsyncRegisterInterestInRingDelegate remoteDel3;

            foreach (DateTime d in ServerApp._user.ReceivedMessages)
            {
                if (d.Equals(message.Id)) //JA DEU A VOLTA
                {
                    System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " :Percorreu o Anel inteiro = ja deu a volta!");

                    Os interesses que sobrarem ele que se amanhe
                    foreach (Interest i in message.Interests)
                    {
                        //diz para criar o anel em q ta sozinho
                        mreg = new MessageInsertRing(2, i, message.Uris.ElementAt(0), message.Uris.ElementAt(0), ServerApp._user.Username, new List<string>(), new List<string>());
                        mreg.Uris.Add(ServerApp._primaryURI);
                        mreg.ContactingServerUri.Add(ServerApp._primaryURI);

                        friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                         message.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));
                        remoteDel = new RemoteAsynChangeRingDelegate(friend.ChangeInterestRingFriends);
                        remoteDel.BeginInvoke(mreg, null, null);

                        //diz para se inscrever no anel geral logo a seguir a si
                        mreg = new MessageInsertRing(2, i, ServerApp._primaryURI, ServerApp._user.AnelGeralUris.ElementAt(1), ServerApp._user.Username, new List<string>(), new List<string>());
                        mreg.Uris.Add(ServerApp._primaryURI);
                        mreg.ContactingServerUri.Add(ServerApp._primaryURI);

                        friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                         message.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));
                        remoteDel = new RemoteAsynChangeRingDelegate(friend.ChangeGeneralRingFriends);
                        remoteDel.BeginInvoke(mreg, null, null);
                    }
                    return;
                }
            }
            ServerApp._user.ReceivedMessages.Add(message.Id);

            foreach (Interest i in message.Interests)
            {
                found = false;
                foreach (RegisteredInterest r in ServerApp._user.RegInterests)
                {
                    //TODO acabar o registo de interesses
                    if (i.Equals(r.Interest))
                    {
                        if (r.RingNextUri.CompareTo(ServerApp._primaryURI) != 0) nao esta sozinho no anel
                        {
                            nextUriAux = r.RingNextUri; ;
                            r.RingNextUri = message.Uris.ElementAt(0);

                            mreg = new MessageInsertRing(2, i, ServerApp._primaryURI, nextUriAux, ServerApp._user.Username, new List<string>(), new List<string>());
                            mreg.Uris.Add(ServerApp._primaryURI);
                            mreg.ContactingServerUri.Add(ServerApp._primaryURI);

                            /*Server Uri(0) regista como before ServerApp.primary
                            /*              regista como after nexUriAux
                            System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " : Vou dizer ao " + message.Name +
                                "para por o seu before para " + mreg.Before + "e o seu next para : " + mreg.After);

                            friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                                 message.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));
                            remoteDel = new RemoteAsynChangeRingDelegate(friend.ChangeInterestRingFriends);
                            remoteDel.BeginInvoke(mreg, null, null);

                            /*tem de contactar o nextUriAux para actualizar o before dele com o uri do server uri(0)
                            mreg = new MessageInsertRing(1, i, message.Uris.ElementAt(0), message.Name, ServerApp._user.Username, new List<string>(), new List<string>());
                            mreg.Uris.Add(ServerApp._primaryURI);
                            mreg.ContactingServerUri.Add(ServerApp._primaryURI);

                            friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                                 nextUriAux + "/" + ServicesNames.ServerToServerServicesName));
                            remoteDel2 = new RemoteAsynChangeRingDelegate(friend.ChangeInterestRingFriends);
                            remoteDel2.BeginInvoke(mreg, null, null);
                        }
                        else /*Era o unico no anel
                        {
                            r.RingBeforeUri = message.Uris.ElementAt(0);
                            r.RingNextUri = message.Uris.ElementAt(0);

                            mreg = new MessageInsertRing(2, i, ServerApp._primaryURI, ServerApp._primaryURI, ServerApp._user.Username, new List<string>(), new List<string>());
                            mreg.Uris.Add(ServerApp._primaryURI);
                            mreg.ContactingServerUri.Add(ServerApp._primaryURI);

                            /*Server Uri(0) regista como before ServerApp.primary
                            /*              regista como after ServerApp.primary
                            System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " : Vou dizer ao " + message.Name +
                                "para por o seu before para " + mreg.Before + "e o seu next para : " + mreg.After);

                            friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                                 message.Uris.ElementAt(0) + "/" + ServicesNames.ServerToServerServicesName));
                            remoteDel = new RemoteAsynChangeRingDelegate(friend.ChangeInterestRingFriends);
                            remoteDel.BeginInvoke(mreg, null, null);
                        }
                        found = true;
                        break;
                    }
                }
                if (!found)
                    newInter.Add(i); //Assim so os que nao adicionou ao ring é que continuam pelo anel geral a procura
            }

            message.Interests = newInter;
            System.Windows.Forms.MessageBox.Show(ServerApp._user.Username + " : Vou mandar os interesses por reg para o anel : " + ServerApp._user.AnelGeralUris.ElementAt(1));
            friend = ((ServerToServerServices)Activator.GetObject(typeof(ServerToServerServices),
                 ServerApp._user.AnelGeralUris.ElementAt(1) + "/" + ServicesNames.ServerToServerServicesName));
            remoteDel3 = new RemoteAsyncRegisterInterestInRingDelegate(friend.registerInterestInRing);
            remoteDel3.BeginInvoke(message, null, null);
        }*/

        /*public void lookupSexAgeResponse(QueryByGenderAge q)
        {
            ClientServices cliente = (ClientServices)Activator.GetObject(
                typeof(ClientServices),
                ServerApp._clientUri + "/" + ServicesNames.ClientServicesName);
            cliente.lookupSexAgeResponse(q);
        }*/

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
    }
}
