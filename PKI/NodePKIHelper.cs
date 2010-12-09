using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.IO;

namespace PKI
{
    public class NodePKIHelper
    {
        private PKIServiceToNode pki;

        /* default address: "tcp://192.168.174.128" */
        public NodePKIHelper(string pkiAddress, int port)
        {
            pki = ((PKIServiceToNode)Activator.GetObject(typeof(PKIServiceToNode),
                pkiAddress + ":" + port.ToString() + "/" + "PKIService"));
        }

        public void register(UserEntry entryData)
        {
            RemoteAsyncUserRegisterDelegate registerDelegate = 
                new RemoteAsyncUserRegisterDelegate(pki.Register);
            registerDelegate.BeginInvoke(entryData, new AsyncCallback(remoteChallenge), null);
            // send reply on challange
        }

        public UserEntry getUserPublicKey(string userId)
        {
            RemoteAsyncGetPubKeyDelegate pubkeyDelegate = 
                new RemoteAsyncGetPubKeyDelegate(pki.GetPublicKey);
            pubkeyDelegate.BeginInvoke(userId, new AsyncCallback(remotePubKeyCallback), null);
            // get return value from callback
            return null;
        }

        private void remoteChallenge(IAsyncResult result)
        {
            try
            {
                RemoteAsyncUserRegisterDelegate del = 
                    (RemoteAsyncUserRegisterDelegate)((AsyncResult)result).AsyncDelegate;

                byte[] challenge = del.EndInvoke(result);
                // cipher challenge with private key
                CipheredChallenge cc = new CipheredChallenge();
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                
                cc.EncryptedResponse = rsa.Encrypt(challenge, false);
                cc.Sender = "0-1"; // get node id properly
 
                // send to pki
                RemoteAsyncUserRegisterChallengeResponseDelegate responseDelegate =
                    new RemoteAsyncUserRegisterChallengeResponseDelegate(pki.ChallengeResponse);
                responseDelegate.BeginInvoke(cc, null, null);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not parse challenge. Dying a little.");
                Console.WriteLine("\t" + e.Message);
            }
        }

        private void remotePubKeyCallback(IAsyncResult result)
        {
            try
            {
                RemoteAsyncGetPubKeyDelegate del = (RemoteAsyncGetPubKeyDelegate)((AsyncResult)result).AsyncDelegate;
                SignedEntry signedPubkey = del.EndInvoke(result);

                byte[] data = Encoding.Default.GetBytes(signedPubkey.Entry.ToString());
                
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

                TextReader tr = new StreamReader("pki_pub.xml");
                rsa.FromXmlString(tr.ReadToEnd());

                byte[] signature = signedPubkey.Signature;

                //Verify the hash and the signature
                if (rsa.VerifyData(data, "SHA1", signature))
                {
                    Console.WriteLine("The signature was verified.");
                }
                else
                {
                    Console.WriteLine("The signature was not verified.");
                }
             
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void remoteIsRegistered(IAsyncResult result)
        {
            try
            {
                RemoteAsyncUserIsRegisteredDelegate del = (RemoteAsyncUserIsRegisteredDelegate)((AsyncResult)result).AsyncDelegate;

                bool isRegistered = del.EndInvoke(result);
                Console.WriteLine("Test got: " + isRegistered);

            }
            catch (Exception e) 
            { 
                Console.WriteLine("Could not parse isRegistered result");
                Console.WriteLine("\t" + e.Message);
            }
        }

        /* Debug functions */
        public void TestRegister()
        {
            PKIServiceToNode pki;
            pki = ((PKIServiceToNode)Activator.GetObject(typeof(PKIServiceToNode),
                "tcp://192.168.174.128:50000" + "/" + "NodeToPKIService"));
            RemoteAsyncUserRegisterDelegate ueRegister = new RemoteAsyncUserRegisterDelegate(pki.Register);
            UserEntry ue = new UserEntry();
            ue.Address = "localhost";
            ue.Port = 50001;
            ue.NodeId = "0-1";
            ue.PubKey = "someweirdkeythatisnotsoeasytoconverttoaprivatekey";
            ueRegister.BeginInvoke(ue, null, null);
        }

        public void TestGetKey()
        {
            PKIServiceToNode pki;
            pki = ((PKIServiceToNode)Activator.GetObject(typeof(PKIServiceToNode),
                "tcp://192.168.174.128:50000" + "/" + "NodeToPKIService"));
            RemoteAsyncGetPubKeyDelegate pubkeyDelegate = new RemoteAsyncGetPubKeyDelegate(pki.GetPublicKey);
            pubkeyDelegate.BeginInvoke("0-1", new AsyncCallback(remotePubKeyCallback), null);
        }

        public void Test()
        {
            PKIServiceToNode pki;
            AsyncCallback callback = new AsyncCallback(remoteIsRegistered);
            pki = ((PKIServiceToNode)Activator.GetObject(typeof(PKIServiceToNode),
                    "tcp://192.168.174.128:50000" + "/" + "NodeToPKIService"));
            RemoteAsyncUserIsRegisteredDelegate ueDelegate = new RemoteAsyncUserIsRegisteredDelegate(pki.IsRegistered);
            ueDelegate.BeginInvoke("0-1", callback, null);
        }
    }
}
