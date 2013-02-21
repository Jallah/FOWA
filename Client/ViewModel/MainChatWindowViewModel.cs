using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class MainChatWindowViewModel
    {
        public ObservableCollection<UserChatViewModel> UserChats { get; set; }
  
        public MainChatWindowViewModel()
        {
            UserChats = new ObservableCollection<UserChatViewModel>()
                            {
                                new UserChatViewModel(new User(234, "Hans@gmx.net", "Hans", "123.123.123.123:3000")),
                                new UserChatViewModel(new User(555, "Peter@gmx.de", "Peter", "123.123.123.111:3000"))
                            };
        }
    }
}
