//------------------------------------------------------------------------------
// <auto-generated>
//    Dieser Code wurde aus einer Vorlage generiert.
//
//    Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten Ihrer Anwendung.
//    Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using Server.BL.Contracts;

namespace Server.DL
{
    using System;
    using System.Collections.Generic;
    
    public partial class user : IEntity
    {
        public user()
        {
            this.friends = new HashSet<friends>();
        }
    
        public int ID { get; set; }
        public string email { get; set; }
        public string pw { get; set; }
        public string nick { get; set; }
        public Nullable<System.DateTime> lastMessage { get; set; }
    
        public virtual ICollection<friends> friends { get; set; }
    }
}
