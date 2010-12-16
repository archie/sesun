using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PADIbookCommonLib;

namespace Client
{
    public class ClientServicesObject : MarshalByRefObject, ClientServices
    {
        public void receivePost(Post post)
        {
            ClientApp._user.addPost(post);
        }

        public void receiveFriendPosts(List<Post> listaposts)
        {
            ClientApp._form.refreshViewMural(listaposts);
        }

        public Friend sendFriendRequest(Friend friend)
        {
            ClientApp._user.addPendingFriend(friend);
            UpdateTextBoxesDelegate updateDelegate = new UpdateTextBoxesDelegate(ClientApp._form.refreshMyPendingFriends);
            ClientApp._form.BeginInvoke(updateDelegate);
            return null;
        }

        public void lookupNameResponse(String name, String uri, List<RedirectionFile> redList)
        {
            UpdateLookupNameDelegate updateDelegate = new UpdateLookupNameDelegate(ClientApp._form.refreshLookupName);
            ClientApp._form.BeginInvoke(updateDelegate,name,uri,redList);
        }

  
        public void refreshFriends()
        {
            //MessageBox.Show(ClientApp._user.Username + " : Vou mandar o form refrescar os meus amigos");
            UpdateTextBoxesDelegate updateDelegate = new UpdateTextBoxesDelegate(ClientApp._form.refreshMyFriends);
            ClientApp._form.BeginInvoke(updateDelegate);
        }

        public void changeNameOfFriend(String name, String primary)
        {
            foreach (Friend f in ClientApp._user.Friends)
            {
                if (f.Uris.ElementAt(0).CompareTo(primary) == 0)
                {
                    f.Name = name;
                    return;
                }
            }
        }

        public Friend acceptFriendRequest(Friend friend)
        {
            RemoteAsyncPostDelegate postDel;
            AsyncCallback postCallback;

            ClientApp._user.addFriend(friend);
            try
            {
                if (ClientApp._user.getPendingFriend(friend.Name) != null)
                    ClientApp._user.removePendingFriend(friend.Name);
            }
            catch (Exception) { }
            UpdateTextBoxesDelegate updateDelegate = new UpdateTextBoxesDelegate(ClientApp._form.refreshMyFriends);
            updateDelegate += ClientApp._form.refreshMyPendingFriends;
            ClientApp._form.BeginInvoke(updateDelegate);

            postCallback = new AsyncCallback(ClientApp._form.remoteAsyncSendPostCallBack);
            postDel = new RemoteAsyncPostDelegate(ClientApp._form.Server.sendPost);

            postDel.BeginInvoke(new Post(postMessage(ClientApp._user.Username,friend.Name),ClientApp._user.Username),postCallback,null);

            return null;
        }

        private string postMessage(string me, string myNewFriend)
        {
            return me + " added " + myNewFriend;
        }

        public void changeLeader(string leaderUri)
        {

            ClientApp._serverUri = leaderUri;

            ClientApp._form.Server= (ClientToServerServices)Activator.GetObject(
                    typeof(ClientToServerServices),
                    ClientApp._serverUri + "/" + ServicesNames.ClientToServerServicesName);
        }

        public void serviceUnavailable()
        {
            System.Windows.Forms.MessageBox.Show(Messages.ServiceNotAvailable);
        }

    }
}
