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
    
    public partial class TournamentWinnerTeam
    {
        public int TournamentWinnerID { get; set; }
        public int TournamentTeamID { get; set; }
        public byte[] RowVersion { get; set; }
    
        public virtual TournamentTeam TournamentTeam { get; set; }
        public virtual TournamentWinner TournamentWinner { get; set; }
    }
}
