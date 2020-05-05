using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Model
{
    public class Ticket : HasID<int>
    {
        public int NrSeatsWanted { get; set; }
        public string BuyerName { get; set; }
        public int IdShow { get; set; }
        
      
    }
}