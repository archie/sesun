using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using PADIbookCommonLib;

namespace Client
{
    static class ClientApp
    {
        public static User _user;

        public static ClientForm _form;

        public static string _myUri;

        public static string _serverUri;

        public static Boolean _connected;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length == 2)
            {
                _user = new User();
                _form = new ClientForm(Int32.Parse(args[0]), args[1]);
            }
            else
                _form = new ClientForm();

            if (_connected)
                Application.Run(_form);
        }
    }
}
