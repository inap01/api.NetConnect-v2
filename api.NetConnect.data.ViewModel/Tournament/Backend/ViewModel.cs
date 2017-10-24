using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Tournament.Backend
{
    public class BackendTournamentViewModel : BackendBaseViewModel
    {
        public BackendTournamentViewModelItem Data { get; set; }

        public BackendTournamentViewModel()
        {
            Data = new BackendTournamentViewModelItem();
        }
    }

    public class BackendTournamentViewModelItem : BaseViewModelItem
    {
        public String ChallongeLink { get; set; }
        public String Mode { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public Int32 TeilnehmerAnzahl { get; set; }
        public BackendTournamentGameViewModelItem Game { get; set; }
        public List<BackendTournamentParticipantViewModelItem> Player { get; set; }
        public List<BackendTournamentTeamViewModelItem> Teams { get; set; }
        public BackendTournamentPartner Partner { get; set; }

        public BackendTournamentViewModelItem()
        {
            Player = new List<BackendTournamentParticipantViewModelItem>();
            Teams = new List<BackendTournamentTeamViewModelItem>();
        }

        public class BackendTournamentPartner : BaseViewModelItem
        {
            public String Name { get; set; }
            public String ImagePath { get; set; }
        }
    }

    public class BackendTournamentGameViewModelItem : BaseViewModelItem
    {
        public String Name { get; set; }
        public Int32 TeamSize { get; set; }
        public String ImagePath { get; set; }
        public String RulesPath { get; set; }
        public Boolean RequireBattleTag { get; set; }
        public Boolean RequireSteamID { get; set; }
    }

    public class BackendTournamentTeamViewModelItem : BaseViewModelItem
    {
        public String Name { get; set; }
        public Boolean HasPassword { get; set; }
        public List<BackendTournamentParticipantViewModelItem> Player { get; set; }

        public BackendTournamentTeamViewModelItem()
        {
            Player = new List<BackendTournamentParticipantViewModelItem>();
        }
    }

    public class BackendTournamentParticipantViewModelItem : BaseViewModelItem
    {
        public String Name { get; set; }
        public String Nickname { get; set; }
    }
}