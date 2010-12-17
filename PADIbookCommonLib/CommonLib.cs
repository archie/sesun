using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PADIbookCommonLib;

namespace PADIbookCommonLib
{

    public static class ServicesNames
    {
        public const string ClientToServerServicesName = "ClientToServerServices";
        public const string ServerToServerServicesName = "ServerToServerServices";
        public const string ReplicationServicesName = "ReplicationServices";
        public const string ClientServicesName = "ClientServices";
    }

    public static class Times
    {
        public const int ReplicaIsAlive = 1000;
        public const int LeaderIsAlive = 3000;
    }

    public static class Messages
    {
        public const string ServiceNotAvailable = "The service is not available!";
    }

    public delegate Post RemoteAsyncPostDelegate(Post post);
    public delegate void RemoteAsyncObjectDelegate(ObjectFile file);
    
    public delegate void RemoteAsyncUriDelegate(string uri);
    public delegate void RemoteAsyncModifyUserProfileDelegate(DateTime time,
                                                              String name,
                                                              String gen,
                                                              List<Interest> inter);
    public delegate void RemoteAsyncFriendRequestDelegate(Friend friend);
    public delegate Friend RemoteAsyncFriendDelegate(Friend friend);
    public delegate void RemoteAsyncGetFriendPostsDelegate();
    public delegate List<Post> RemoteAsyncSendFriendPostsDelegate();
    public delegate void RemoteAsyncLookupNameClientDelegate(QueryByName q);
    public delegate void RemoteAsyncLookupNameDelegate(SignedQueryByName q);
    public delegate void RemoteAsyncLookupNameResponseDelegate(SignedLookupResponse response);
    
    public delegate void RemoteAsyncChangeNameInFriendsDelegate(String name,String primary);
    public delegate void RemoteAsyncRefreshFriendsDelegate();
    public delegate void RemoteAsyncGetNameDelegate(String uri);
    public delegate void RemoteAsyncShareObjectDelegate(SignedQueryByFile query);
    public delegate void RemoteAsyncChangeFriendUriDelegate(string oldUri, string newUri);
    public delegate void RemoteAsyncServiceUnavailableDelegate();

    public interface ClientToServerServices
    {

        Post sendPost(Post post);
        void modifyUserProfile(String name, String spoofaddr);
        //List<Post> getAllUserPosts();
        User returnUserInstance();
        //User returnUserInstance(string clientUri);
        //void createUser(User u);
        void modifyUserFriends(List<Friend> fr);
        void sendFriendRequest(string friendUri);
        void getFriendsPosts();
        bool registerClient(String u);
        Friend acceptFriendRequest(Friend friend);
        Friend removeFriendRequest(Friend friend);
        void lookupname(QueryByName q);
        void refreshAllFriendNames();
        void shareObject(ObjectFile file);
    }

    public interface ServerToServerServices
    {
        Friend sendFriendRequest(Friend friend);
        Friend acceptFriendRequest(Friend friend);
        List<Post> getPosts();
        void lookupname(SignedQueryByName q);
        void lookupNameResponse(SignedLookupResponse response);
        void changeNameOfFriend(String name,String primary);
        void getName(String responseUri);
        void shareObject(SignedQueryByFile query);
        void changeFriendUri(string oldFriendUri, string newFriendUri);
    }

    public interface ReplicationServices
    {
        Post sendPost(Post post);
        void modifyUserProfile(DateTime time, String name, String gen, List<Interest> inter);
        Friend sendFriendRequest(Friend friend);
        Friend acceptFriendRequest(Friend friend);
        void sendClientUri(string clientUri);
        void sendPrimaryUri(string primaryUri);
        void changeFriendUri(string oldFriendUri, string newFriendUri);
    }

    public interface ClientServices
    {
        void receivePost(Post post);
        void receiveFriendPosts(List<Post> listaposts);
        Friend sendFriendRequest(Friend friend);
        Friend acceptFriendRequest(Friend friend);
        void changeLeader(string leaderUri);
        void lookupNameResponse(String name, String uri, List<RedirectionFile> redList);
        void refreshFriends();
        void changeNameOfFriend(String name, String primary);
        void serviceUnavailable();
    }

    /* PKI SERVICE DELEGATES */
    public delegate bool RemoteAsyncUserIsRegisteredDelegate(string id);
    public delegate byte[] RemoteAsyncUserRegisterDelegate(UserEntry ue);
    public delegate bool RemoteAsyncUserRegisterChallengeResponseDelegate(CipheredChallenge cc);
    public delegate SignedEntry RemoteAsyncGetPubKeyDelegate(string id);
    public delegate void RemoteAsyncUserResponseDelegate(CipheredChallenge response);

    public interface PKIServices
    {
        byte[] Register(UserEntry entry);
        bool IsRegistered(string id);
        SignedEntry GetPublicKey(string id);
        bool ChallengeResponse(CipheredChallenge response);
    }
}
