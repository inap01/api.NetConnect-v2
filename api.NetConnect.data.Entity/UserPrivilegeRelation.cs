//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace api.NetConnect.data.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserPrivilegeRelation
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int UserPrivilegeID { get; set; }
        public byte[] RowVersion { get; set; }
    
        public virtual User User { get; set; }
        public virtual UserPrivilege UserPrivilege { get; set; }
    }
}
