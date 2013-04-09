using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FowaProtocol.EventArgs
{
    public class UserDisconnetedEventArgs : System.EventArgs
    {
        public int UserId { get; set; }

        public UserDisconnetedEventArgs(int uid)
        {
            UserId = uid;
        }
    }
}
