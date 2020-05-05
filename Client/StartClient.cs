using app.client;
using app.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    class StartClient
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            //IChatServer server=new ChatServerMock();          
            //IServices server = new ServicesRpcProxy("127.0.0.1", 55555);
            //ClientCtrl ctrl = new ClientCtrl(server);
            LoginPage win  = new LoginPage();
            Application.Run(win);
        }
    }
}
