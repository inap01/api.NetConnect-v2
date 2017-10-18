using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Tournament.Backend
{
    public class TournamentViewModel : BackendBaseViewModel
    {
        public TournamentViewModelItem Data { get; set; }

        public TournamentViewModel()
        {
            Data = new TournamentViewModelItem();
        }
    }

    public class TournamentViewModelItem : BaseViewModelItem
    {
        public Int32 Volume { get; set; }
        public Int32 GameID { get; set; }
        public Int32 TeamSize { get; set; }
        public String ChallongeLink { get; set; }
        public String Mode { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public Boolean IsPauseGame { get; set; }
        public String Name { get; set; }
        public String Icon { get; set; }
        public String Rules { get; set; }
        public Boolean BattleTag { get; set; }
        public Boolean Steam { get; set; }
        public Int32 TeilnehmerAnzahl { get; set; }
        public List<TournamentParticipantViewModelItem> Player { get; set; }
        public List<TournamentTeamViewModelItem> Teams { get; set; }
        public TournamentPartner Partner { get; set; }

        public TournamentViewModelItem()
        {
        }

        public class TournamentPartner : BaseViewModelItem
        {
            public String Name { get; set; }
            public String Image { get; set; }
        }
    }

    public class TournamentTeamViewModelItem : BaseViewModelItem
    {
        public String Name { get; set; }
        public Boolean HasPassword { get; set; }
        public List<TournamentParticipantViewModelItem> Player { get; set; }
    }

    public class TournamentParticipantViewModelItem : BaseViewModelItem
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }
    }
}