//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace api.NetConnect.data.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TournamentGame
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TournamentGame()
        {
            this.Tournament = new HashSet<Tournament>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Rules { get; set; }
        public bool IsActive { get; set; }
        public bool BattleTag { get; set; }
        public bool SteamID { get; set; }
        public byte[] RowVersion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tournament> Tournament { get; set; }
    }
}
