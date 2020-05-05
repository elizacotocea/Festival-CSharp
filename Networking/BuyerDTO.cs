using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking
{
    [Serializable()]
    public class BuyerDTO
    {
        public int Id { get; }
        public string Name { get;}

        public BuyerDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
