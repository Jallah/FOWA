using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class MainChatWindowViewModel
    {
        public ObservableCollection<UserChatViewModel> UserChats { get; set; }
  
        public MainChatWindowViewModel()
        {
            UserChats = new ObservableCollection<UserChatViewModel>()
                            {
                                new UserChatViewModel(new User(234, "Hans@gmx.net", "Hans", DateTime.Now, "hallo")),
                                new UserChatViewModel(new User(555, "Peter@gmx.de", "Peter", DateTime.Now, "hallo"))
                            };
        }
    }
}
