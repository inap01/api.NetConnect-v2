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
    
    public partial class CateringProductAttributeRelation
    {
        public int ID { get; set; }
        public int CateringProductID { get; set; }
        public int CateringProductAttributeID { get; set; }
        public byte[] RowVersion { get; set; }
    
        public virtual CateringProduct CateringProduct { get; set; }
        public virtual CateringProductAttribute CateringProductAttribute { get; set; }
    }
}
