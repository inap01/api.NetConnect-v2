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
    
    public partial class SeatTransferLog
    {
        public int ID { get; set; }
        public int SeatID { get; set; }
        public int SourceUserID { get; set; }
        public int DestinationUserID { get; set; }
        public System.DateTime TransferDate { get; set; }
        public byte[] RowVersion { get; set; }
    
        public virtual Seat Seat { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
