using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking
{
    [Serializable()]
    public class TicketDTO
    {
        public int Id { get; }
        public int NrSeatsWanted { get; }
        public string BuyerName { get; }
        public int IdShow { get;}

        public TicketDTO(int id, int nrSeatsWanted, string bName, int idShow)
        {
            Id = id;
            NrSeatsWanted = nrSeatsWanted;
            BuyerName = bName;
            IdShow = idShow;
        }
    }
}
