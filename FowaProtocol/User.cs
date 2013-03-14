
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FowaProtocol;
namespace FowaProtocol
{
    public class User : IContact
    {
        public int UID { get; set; }
        
        public string Email { get; set; }
        
        public string Nick { get; set; }
        
        public DateTime? LastMessage { get; set; }
        
        public string Pw { get; set; }
    }
}
