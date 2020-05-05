using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking
{
    [Serializable()]
    public class UserDTO
    {
        public int Id { get; }
        public string Username { get; }
        public string Password { get; }

        public UserDTO(int id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }
    }

    
}
