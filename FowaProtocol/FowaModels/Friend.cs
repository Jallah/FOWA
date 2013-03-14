using System.Xml.Serialization;

namespace FowaProtocol.FowaModels
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
