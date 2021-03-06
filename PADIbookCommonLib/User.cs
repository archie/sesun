﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PADIbookCommonLib
{
    [Serializable]
    public class User
    {
        private String _username;
        private String _spoofAdress;
        private List<Post> _userPosts;
        private List<Friend> _friends;
        private List<Friend> _pendingFriends;

        private List<ObjectFile> _objectList;
        private List<RedirectionFile> _redirectionList;

        public static List<QueryByName> _receivedNameMessages;
        public static List<QueryByFile> _receivedFileMessages;
        public static List<DateTime> _sentMessages;

        public List<DateTime> SentMessages
        {
            get { return _sentMessages; }
            set { _sentMessages = value; }
        }

        public List<QueryByFile> ReceivedFileMessages
        {
            get;
            set;
        }

        public List<QueryByName> ReceivedNameMessages
        {
            get;
            set;
        }

        public List<Post> UserPosts
        {
            get { return _userPosts; }
            set { _userPosts = value; }
        }

        public List<ObjectFile> ObjectList
        {
            get { return _objectList; }
            set { _objectList = value; }
        }

        public List<RedirectionFile> RedirectionList
        {
            get { return _redirectionList; }
            set { _redirectionList = value; }
        }

        public List<Friend> Friends
        {
            get { return _friends; }
            set { _friends = value; }
        }

        public List<Friend> PendingFriends
        {
            get { return _pendingFriends; }
            set { _pendingFriends = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string SpoofAdress
        {
            get { return _spoofAdress; }
            set { _spoofAdress = value; }
        }

        public void addObject(ObjectFile file)
        {
            _objectList.Add(file);
        }

        public void addRedirection(RedirectionFile file)
        {
            _redirectionList.Add(file);
        }

        public void addPost(Post post)
        {
            _userPosts.Add(post);
        }

        public void addFriend(Friend friend)
        {
            _friends.Add(friend);
        }

        public void removeFriend(String fr)
        {
            foreach (Friend i in this._friends)
            {
                if (i.Name.CompareTo(fr) == 0)
                {
                    this.Friends.Remove(i);
                    return;
                }
            }
        }

        public Friend getFriendByUri(string uri)
        {       
            foreach (Friend friend in _friends)
                if (friend.Uris.ElementAt(0).CompareTo(uri) == 0)
                    return friend;

            throw new InvalidFriendUriException("No friend with such uri: " + uri);
        }

        public void addPendingFriend(Friend friend)
        {
            string newPendingFriendName = friend.Name;

            foreach (Friend pendingFriend in _pendingFriends)
                if (pendingFriend.Name.Equals(newPendingFriendName))
                    throw new DuplicatePendingFriendException("Duplicate pending friend");

            _pendingFriends.Add(friend);
        }

        public void removePendingFriend(string friendName)
        {
            foreach (Friend f in _pendingFriends)
            {
                if (f.Name.CompareTo(friendName) == 0)
                {
                    _pendingFriends.Remove(f);
                    return;
                }
            }
            throw new InvalidPendingFriendException("No such pending friend to remove");
        }

        public Friend getPendingFriend(string friendName)
        {
            foreach (Friend f in _pendingFriends)
            {
                if (f.Name.CompareTo(friendName) == 0)
                    return f;
            }
            throw new InvalidPendingFriendException("No such pending friend");
        }

        public User(String addr)
        {
            Random ran = new Random(DateTime.Now.Millisecond);
            _userPosts = new List<Post>();
            _friends = new List<Friend>();
            _pendingFriends = new List<Friend>();
            _redirectionList = new List<RedirectionFile>();
            _objectList = new List<ObjectFile>();

            _receivedNameMessages = new List<QueryByName>();
            _receivedFileMessages = new List<QueryByFile>();
            _sentMessages = new List<DateTime>();

            _spoofAdress = addr;
            _username = "Default" + ran.Next(1000);
        }
        public User()
        {
            Random ran = new Random(DateTime.Now.Millisecond);
            //_interests = new List<Interest>();
            _userPosts = new List<Post>();
            _friends = new List<Friend>();
            _pendingFriends = new List<Friend>();

            _redirectionList = new List<RedirectionFile>();
            _objectList = new List<ObjectFile>();

            _receivedNameMessages = new List<QueryByName>();
            _receivedFileMessages = new List<QueryByFile>();
            _sentMessages = new List<DateTime>();
            _spoofAdress = "";
            _username = "Default node ID";
            _username = "Default" + ran.Next(1000);
        }

    }
}