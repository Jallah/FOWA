using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class UserChatViewModel
    {

        public UserChatViewModel(User chatWith)
        {
            UID = chatWith.UID;
            Nick = chatWith.Nick;
        }

        //public override string ToString()
        //{
        //    return Nick;
        //}

        public int UID { get; set; }
        public string Nick { get; set; }
    }
}
