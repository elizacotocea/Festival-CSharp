using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking
{
    [Serializable()]
    public class ShowDTO
    {
        public int Id { get; }
        public string Location { get;  }
        public DateTime ShowDateTime { get;  }
        public int NrAvailableSeats { get; }
        public int NrSoldSeats { get; }
        public string ArtistName { get; }

        public ShowDTO(int id, string location, DateTime showDateTime, int nrAvailableSeats, int nrSoldSeats, string aName)
        {
            Id = id;
            Location = location;
            ShowDateTime = showDateTime;
            NrAvailableSeats = nrAvailableSeats;
            NrSoldSeats = nrSoldSeats;
            ArtistName = aName;
        }
    }
}
