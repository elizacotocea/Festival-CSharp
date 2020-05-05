using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Model
{
    public class Buyer : HasID<int>
    {
        public string Name { get; set; }
    }
}
