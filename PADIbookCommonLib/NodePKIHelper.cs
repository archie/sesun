using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.IO;
using System.Threading;

namespace PADIbookCommonLib
{
    
    public class NodePKIHelper
    {
        private PKIServices pki;
        private RSACryptoServiceProvider rsa;
        private RSACryptoServiceProvider pkiRsaProvider;

        /* default address: "tcp://localhost:50000" */
        public NodePKIHelper(RSACryptoServiceProvider provider, 
            RSACryptoServiceProvider rsa_provider_for_pki, string pkiAddress)
        {
            rsa = provider;
            pkiRsaProvider = rsa_provider_for_pki;

            pki = ((PKIServices)Activator.GetObject(typeof(PKIServices),
                pkiAddress + "/" + "PKIService"));
        }

        public bool Register(UserEntry entryData)
        {    
            RemoteAsyncUserRegisterDelegate registerDelegate = 
                new RemoteAsyncUserRegisterDelegate(pki.Register);
            byte[] challenge = registerDelegate(entryData);

            if (challenge == null)
                return false; // wasn't allowed to register

            // cipher challenge with private key
            CipheredChallenge cc = new CipheredChallenge();
            cc.Signature = rsa.SignData(challenge, "SHA1");
            cc.Sender = entryData.NodeId; 
            
            // send to pki
            RemoteAsyncUserRegisterChallengeResponseDelegate responseDelegate =
                new RemoteAsyncUserRegisterChallengeResponseDelegate(pki.ChallengeResponse);
            bool result = responseDelegate(cc);
          
            return result;
        }

        public UserEntry GetVerifiedUserPublicKey(string userId)
        {
            SignedEntry response = null;
            RemoteAsyncGetPubKeyDelegate pubkeyDelegate = 
                new RemoteAsyncGetPubKeyDelegate(pki.GetPublicKey);
            response = pubkeyDelegate(userId);

            if (response != null)
            {
                // verify authenticity
                byte[] data = Encoding.Default.GetBytes(response.Entry.ToString());

                //Verify the hash and the signature
                if (rsa.VerifyData(data, "SHA1", response.Signature))
                {
                    return response.Entry;
                }
            }
            return null;
        }

    }
}
