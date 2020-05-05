using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Model
{
    public class Employee : HasID<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
