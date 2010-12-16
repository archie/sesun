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
    public delegate void RemoteAsyncLookupNameDelegate(QueryByName q);
    public delegate void RemoteAsyncLookupNameResponseDelegate(String name,String uri,List<RedirectionFile> redList);
    
    //public delegate void RemoteAsynchRemoveOtherReplicaDelegate();
    //public delegate void RemoteAsyncLookupNameResponseDelegate(QueryByName q);
    public delegate void RemoteAsyncChangeNameInFriendsDelegate(String name,String primary);
    public delegate void RemoteAsyncRefreshFriendsDelegate();
    //public delegate void RemoteAsyncLookupSexAgeDelegate(QueryByGenderAge q);
    public delegate void RemoteAsyncGetNameDelegate(String uri);
    public delegate void RemoteAsyncShareObjectDelegate(QueryByFile query);
    public delegate void RemoteAsyncChangeFriendUriDelegate(string oldUri, string newUri);
    //public delegate void RemoteAsyncLookupInterestDelegate(QueryByInterest q);
    public delegate void RemoteAsyncServiceUnavailableDelegate();
    //public delegate void RemoteAsyncRegisterInterestInRingDelegate(MessageRegister m);
    //public delegate void RemoteAsynChangeRingDelegate(MessageInsertRing m);

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
        //void lookupSexAge(QueryByGenderAge q);
        //void lookupInterest(QueryByInterest q);
    }

    public interface ServerToServerServices
    {
        Friend sendFriendRequest(Friend friend);
        Friend acceptFriendRequest(Friend friend);
        List<Post> getPosts();
        void lookupname(QueryByName q);
        void lookupNameResponse(String name, String uri, List<RedirectionFile> redList);
        void changeNameOfFriend(String name,String primary);
        void getName(String responseUri);
        void shareObject(QueryByFile query);
        //void lookupSexAge(QueryByGenderAge q);
        //void lookupSexAgeResponse(QueryByGenderAge q);
        void changeFriendUri(string oldFriendUri, string newFriendUri);
        /*void lookupGeneralRing(QueryByInterest q);
        void lookupGeneralRingResponse(QueryByInterest q);
        void registerInterestInRing(MessageRegister message);
        void ChangeInterestRingFriends(MessageInsertRing message);
        void ChangeGeneralRingFriends(MessageInsertRing message);*/
        //void lookupInterest(QueryByInterest q);
        //void lookupInterestResponse(QueryByInterest q);
    }

    public interface ReplicationServices
    {
        //User registerReplica(string uri);
        Post sendPost(Post post);
        void modifyUserProfile(DateTime time, String name, String gen, List<Interest> inter);
        Friend sendFriendRequest(Friend friend);
        Friend acceptFriendRequest(Friend friend);
        //void sendOtherReplica(string uri);
        //void isAlive();
        //void removeOtherReplica();
        //string imTheLeader(string uri);
        void sendClientUri(string clientUri);
        void sendPrimaryUri(string primaryUri);
        void changeFriendUri(string oldFriendUri, string newFriendUri);
    }

    public interface ClientServices
    {
        //void receiveAllPosts(List<Post> posts);
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
