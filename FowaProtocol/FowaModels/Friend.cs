using System.Xml.Serialization;

namespace FowaProtocol.FowaModels
{
    public class Friend : IContact
    {
        public string Email { get; set; }

        public string Nick { get; set; }

        public int UserId { get; set; }
    }
}
