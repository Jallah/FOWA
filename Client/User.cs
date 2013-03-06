using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FowaProtocol;

namespace Client
{
    public class User : IContact
    {
        public User(int uid, string email, string nick, string ip, string pw)
        {
            UID = uid;
            Email = email;
            Nick = nick;
            Ip = ip;
            Pw = pw;
        }

        public int UID { get; set; }
        public string Email { get; set; }
        public string Nick { get; set; }
        public string Ip { get; set; }
        public string Pw { get; set; }

    }
}
