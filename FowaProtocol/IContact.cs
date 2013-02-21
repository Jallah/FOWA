using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FowaProtocol
{
    public interface IContact
    {
        string Nick { get; set; }
        int UID { get; set; }

        // additional Properties (ip etc.)
    }
}
