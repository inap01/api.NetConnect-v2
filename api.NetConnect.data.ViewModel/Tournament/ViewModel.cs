using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Tournament
{
    public class TournamentViewModel : BaseViewModel
    {
        public TournamentViewModelItem Data { get; set; }

        public TournamentViewModel()
        {
            Data = new TournamentViewModelItem();
        }
    }

    public class TournamentViewModelItem : BaseViewModelItem
    {
        public Int32 LanID { get; set; }
        public Int32 GameI { get; set; }
        public Int32 TeamSize { get; set; }
        public String ChallongeLink { get; set; }
        public String Mode { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Boolean IsPauseGame { get; set; }
        public String Name { get; set; }
        public String Icon { get; set; }
        public String Rules { get; set; }
        public Boolean BattleTag { get; set; }
        public Boolean Steam { get; set; }
        public Int32 TeilnehmerAnzahl { get; set; }
        public List<TournamentParticipant> Player { get; set; }
        public List<TournamentTeam> Teams { get; set; }
        public TournamentPartner Partner { get; set; }

        public TournamentViewModelItem()
        {
        }

        public class TournamentTeam : BaseViewModelItem
        {

        }

        public class TournamentParticipant : BaseViewModelItem
        {

        }

        public class TournamentPartner : BaseViewModelItem
        {

        }
    }
}