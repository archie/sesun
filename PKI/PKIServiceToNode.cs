using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace PKI
{
    public interface PKIServiceToNode
    {
        byte[] Register(UserEntry entry);
        bool IsRegistered(string id);
        SignedEntry GetPublicKey(string id);
        bool ChallengeResponse(CipheredChallenge response);
    }

    public delegate bool RemoteAsyncUserIsRegisteredDelegate(string id);
    public delegate byte[] RemoteAsyncUserRegisterDelegate(UserEntry ue);
    public delegate bool RemoteAsyncUserRegisterChallengeResponseDelegate(CipheredChallenge cc);
    public delegate SignedEntry RemoteAsyncGetPubKeyDelegate(string id);
    public delegate void RemoteAsyncUserResponseDelegate(CipheredChallenge response);

    class PKIServiceNodeToObject : MarshalByRefObject, PKIServiceToNode 
    {
        private LinkedList<UserEntry> userDB;
        private Dictionary<UserEntry, byte[]> waitingChallenge; 

        public PKIServiceNodeToObject()
        {
            waitingChallenge = new Dictionary<UserEntry, byte[]>();
            userDB = new LinkedList<UserEntry>();
        }

        public byte[] Register(UserEntry entry)
        {
            Random rand = new Random(123);
            int challenge = rand.Next();
            byte[] rawChallenge = Encoding.Default.GetBytes(challenge.ToString());
            waitingChallenge.Add(entry, rawChallenge);
            return rawChallenge;
        }

        public bool ChallengeResponse(CipheredChallenge response)
        {
            UserEntry pendingUser = null;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            foreach (UserEntry e in waitingChallenge.Keys) 
            {
                if (e.NodeId.Equals(response)) 
                {
                    pendingUser = e;
                    break;
                }
            }

            rsa.FromXmlString(pendingUser.PubKey);
            byte[] receivedChallenge = rsa.Decrypt(response.EncryptedResponse, false);
            waitingChallenge.Remove(pendingUser);

            if (waitingChallenge[pendingUser].Equals(receivedChallenge))
            {
                userDB.AddFirst(pendingUser);
                return true;
            }
            else
            {
                return false;
            }
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
