using app.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.services
{
    public interface IAppObserver
    {
            void notifyTicketSold(Show show);

        
    }
}
