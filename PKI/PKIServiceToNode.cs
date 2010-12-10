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
        private Dictionary<UserEntry, byte[]> waitingChallenge;
        private Random _rand;

        public PKIServiceNodeToObject()
        {
            _rand = new Random(DateTime.Now.Millisecond);
            waitingChallenge = new Dictionary<UserEntry, byte[]>();
            userDB = new LinkedList<UserEntry>();
        }

        public byte[] Register(UserEntry entry)
        {
            Console.WriteLine("Got register request from: " + entry.NodeId);
            int challenge = _rand.Next();
            byte[] rawChallenge = Encoding.Default.GetBytes(challenge.ToString());
            waitingChallenge.Add(entry, rawChallenge);
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
                if (rsa.VerifyData(waitingChallenge[pendingUser], "SHA1", response.Signature))
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
            foreach (UserEntry ue in userDB)
            {
                if (ue.NodeId.Equals(id))
                    return PKIService.sign(ue);
            }
            return null;
        }

        
    }
}
