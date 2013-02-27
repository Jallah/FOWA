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
        string Nick { get; set; }
        string Email { get; set; }
        int UID { get; set; }
        string Ip { get; set; }

        // additional Properties (Port etc.)
    }
}
