using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FowaProtocol
{
    public interface IContact
    {
        int UserId { get; set; }
        string Email { get; set; }
        string Pw { get; set; }
        string Nick { get; set; }
        DateTime? LastMessage { get; set; }
    }
}
