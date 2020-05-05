using app.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.services
{
    public interface IServices
    {
   
        Show[] findAllShows();
        void login(Employee user, IAppObserver client);
        void logout(Employee user, IAppObserver client);

        Show ticketsSold(Show show, Ticket t);
    }
}
