using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking
{
    [Serializable()]
    public class ArtistDTO
    {
        public int Id { get; }

        public string Name
        {
            get;
        }

        public ArtistDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
