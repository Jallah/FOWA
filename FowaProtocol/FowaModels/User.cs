using System;

namespace FowaProtocol.FowaModels
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
