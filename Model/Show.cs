using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Model
{
    public class Show : HasID<int>
    {
        public string Location { get; set; }
        public DateTime ShowDateTime { get; set; }
        public int NrAvailableSeats { get; set; }
        public int NrSoldSeats { get; set; }
        public string ArtistName { get; set; }

    }
}
