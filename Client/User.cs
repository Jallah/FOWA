using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class User
    {
        public User(int uid, string email, string nick, string ip)
        {
            UID = uid;
            Email = email;
            Nick = nick;
            Ip = ip;
        }

        public int UID { get; set; }
        public string Email { get; set; }
        public string Nick { get; set; }
        public string Ip { get; set; }
    }
}
