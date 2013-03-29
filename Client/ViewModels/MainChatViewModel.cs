using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Caliburn.Micro;
using FowaProtocol;
using FowaProtocol.FowaModels;

namespace Client.ViewModels
{
    public class MainChatViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public MainChatViewModel()
        {}

        public void OpenTab(ContactViewModel contactViewModel, Friend friend)
        {
            ActivateItem(new UserChatViewModel(contactViewModel, friend));
        }
    }
}
