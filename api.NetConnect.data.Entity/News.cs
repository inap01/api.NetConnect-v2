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
    
    public partial class News
    {
        public int ID { get; set; }
        public int NewsCategoryID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public System.DateTime Date { get; set; }
        public string Text { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; }
        public byte[] RowVersion { get; set; }
    
        public virtual NewsCategory NewsCategory { get; set; }
    }
}
