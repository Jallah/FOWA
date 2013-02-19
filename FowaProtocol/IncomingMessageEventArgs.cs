﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FowaProtocol.FowaMessages;

namespace FowaProtocol
{
    public class IncomingMessageEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public IncomingMessageEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
