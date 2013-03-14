using System;
using System.Xml.Serialization;

namespace FowaProtocol
{
    public class Friend
    {
        [XmlAttribute("email")]
        public string Email { get; set; }

        [XmlAttribute("nickname")]
        public string Nick { get; set; }

        [XmlAttribute("uid")]
        public int Uid { get; set; }
    }
}
