using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.ViewModels.Tournament
{
    public class TournamentViewModel : BaseViewModel
    {
        public List<TournamentViewModelItem> Data { get; set; }
        public Int32 TotalCount { get; set; }

        public TournamentViewModel()
        {
            Data = new List<TournamentViewModelItem>();
            TotalCount = 0;
        }
    }

    public class TournamentViewModelItem
    {
        public Int32 LanID { get; set; }
        public Int32 GameID { get; set; }
        public Int32 TeamSize { get; set; }
        public String Link { get; set; }
        public String Mode { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Boolean IsPauseGame { get; set; }
        public String Name { get; set; }
        public String Icon { get; set; }
        public String Rules { get; set; }
        public Boolean BattleTag { get; set; }
        public Boolean Steam { get; set; }
        public Int32 TeilnehmerAnzahl { get; set; }
        public List<ParticipantViewModel> Player { get; set; }
        public List<TeamViewModel> Teams { get; set; }
        public String PoweredBy { get; set; }
        public String BrandIcon { get; set; }
        public String BrandText { get; set; }
        public Int32 ID { get; set; }
        public Int32 LatestChange { get; set; }

        public TournamentViewModelItem()
        {
            Player = new List<ParticipantViewModel>();
            Teams = new List<TeamViewModel>();
        }
    }
    
    public class TeamViewModel
    {
        public String Name { get; set; }
        public String Password { get; set; }
        public List<ParticipantViewModel> Player { get; set; }
        public Int32 ID { get; set; }
        public Int32 LatestChange { get; set; }

        public TeamViewModel()
        {
            Player = new List<ParticipantViewModel>();
        }
    }

    public class ParticipantViewModel
    {
        public Int32 ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }
        public Int32 LatestChange { get; set; }
    }
}