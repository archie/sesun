using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using PADIbookCommonLib;

namespace PKI
{

    class PKIServiceNodeToObject : MarshalByRefObject, PKIServices
    {
        private LinkedList<UserEntry> userDB;
        private Dictionary<UserEntry, PendingChallenge> waitingChallenge;
        private Random _rand;

        class PendingChallenge
        {
            public PendingChallenge(byte[] challenge, DateTime sent)
            {
                this._challenge = challenge;
                this._sent = sent;
            }
            byte[] _challenge;
            DateTime _sent;
            public byte[] Challenge
            {
                get { return _challenge; }
            }
            public DateTime Sent
            {
                get { return _sent; }
            }
        }

        public PKIServiceNodeToObject()
        {
            _rand = new Random(DateTime.Now.Millisecond);
            waitingChallenge = new Dictionary<UserEntry, PendingChallenge>();
            userDB = new LinkedList<UserEntry>();
        }

        public byte[] Register(UserEntry entry)
        {
            Console.WriteLine("Got register request from: " + entry.NodeId);
            
            if (IsRegistered(entry.NodeId)) // check pending as well...
                return null; // user with same id is already registered, deny
            
            int challenge = _rand.Next();
            byte[] rawChallenge = Encoding.Default.GetBytes(challenge.ToString());
            PendingChallenge pc = new PendingChallenge(rawChallenge, DateTime.Now);
            waitingChallenge.Add(entry, pc);

            Console.WriteLine("Added <" + entry.NodeId + "," + challenge + ">");

            return rawChallenge;
        }

        public bool ChallengeResponse(CipheredChallenge response)
        {
            Console.WriteLine("Got ciphered challenge (" + response.Signature 
                + ") response from " + response.Sender);

            UserEntry pendingUser = null;
            
            foreach (UserEntry e in waitingChallenge.Keys) 
            {
                if (e.NodeId.Equals(response.Sender)) 
                {
                    pendingUser = e;
                    break;
                }
            }

            if (pendingUser == null)
                return false;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(pendingUser.PubKey); // load pending user's pubkey

            try
            {
                if (rsa.VerifyData(waitingChallenge[pendingUser].Challenge, "SHA1", response.Signature))
                {
                    Console.WriteLine("Received response matches the challenge sent. " +
                        "(Verified with " + response.Sender + " public key)");
                    userDB.AddFirst(pendingUser);
                    waitingChallenge.Remove(pendingUser);
                    return true;
                }
            }
            catch (CryptographicException ce)
            {
                Console.WriteLine("Could not confirm challenge for user: " + response.Sender + "\n"
                    + ce.Message);
                waitingChallenge.Remove(pendingUser);
                return false;
            }
            
            return false;
        }

        public bool IsRegistered(string id)
        {
            foreach (UserEntry ue in userDB)
            {
                if (ue.NodeId.Equals(id))
                    return true;
            }
            return false;
        }

        public SignedEntry GetPublicKey(string id)
        {
            Console.WriteLine("Request for public key: " + id);
            foreach (UserEntry ue in userDB)
            {
                if (ue.NodeId.Equals(id))
                {
                    Console.WriteLine("Returning user entry (with public key) for id: " + ue.NodeId);
                    Console.WriteLine("-------------------- Key ------------------------");
                    Console.WriteLine(ue.PubKey);
                    Console.WriteLine("------------------ End Key ----------------------");
                    return PKIService.sign(ue);
                }
            }
            return null;
        }

        public LinkedList<UserEntry> GetUserDatabase()
        {
            return userDB;
        }

        public LinkedList<UserEntry> GetPendingUserDatabase()
        {
            return new LinkedList<UserEntry>(waitingChallenge.Keys);
        }

        public void EmptyPendingUsers()
        {
            foreach (UserEntry e in waitingChallenge.Keys)
            {
                if (waitingChallenge[e].Sent.AddSeconds(10) > DateTime.Now)
                    waitingChallenge.Remove(e);
            }
        }

        public void ClearDatabase()
        {
            userDB.Clear();
        }
    }
}
