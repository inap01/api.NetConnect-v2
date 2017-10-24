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
    
    public partial class Partner
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Partner()
        {
            this.PartnerDisplayRelation = new HashSet<PartnerDisplayRelation>();
            this.Tournament = new HashSet<Tournament>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string RefLink { get; set; }
        public string Content { get; set; }
        public System.Guid ImageContainerID { get; set; }
        public int PartnerPackID { get; set; }
        public bool IsActive { get; set; }
        public int Position { get; set; }
        public int ClickCount { get; set; }
        public byte[] RowVersion { get; set; }
    
        public virtual ImageContainer ImageContainer { get; set; }
        public virtual PartnerPack PartnerPack { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartnerDisplayRelation> PartnerDisplayRelation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tournament> Tournament { get; set; }
    }
}
