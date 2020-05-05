using app.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.services;

namespace client
{
    public class ClientCtrl : IAppObserver
    {
        public event EventHandler<UserEventArgs> updateEvent; //ctrl calls it when it has received an update (manually defined custom delegate)
        private readonly IServices server;


        public ClientCtrl(IServices server)
        {
            this.server = server;
        }


        public Show[] getAllShows()
        {
            return this.server.findAllShows();
        }


        public void ticketsSold(Show selected,Ticket t)
        {
            this.server.ticketsSold(selected,t);
        }


        public void logout(Employee usr)
        {
            Console.WriteLine("Ctrl logout");
            server.logout(usr, this);
        }

        public void login(Employee usr)
        {
            Console.WriteLine("Ctrl login");
            Console.WriteLine(usr.Password);
            server.login(usr, this);
        }



        protected virtual void OnUserEvent(UserEventArgs e)
        {
            if (updateEvent == null) return;
            updateEvent(this, e);
            Console.WriteLine("Update Event called");
        }


        public void notifyTicketSold(Show s) // CALLED BY SERVICE (this is a server random update) to refresh GUI
        {
            Console.WriteLine("Show updated" +s);
            UserEventArgs userArgs = new UserEventArgs(UpdateType.TICKETS_SOLD, s);
            OnUserEvent(userArgs);
        }

    }
}

