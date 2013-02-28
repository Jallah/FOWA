using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FowaProtocol
{

    public interface IContact
    {
        int UID { get; set; }
        string Email { get; set; }
        string Pw { get; set; }
        string Nick { get; set; }
        string Ip { get; set; }

        // additional Properties (Port etc.)
    }
}
