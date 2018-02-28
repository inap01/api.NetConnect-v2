using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.ChangeSet
{
    public class ChangeSetViewModel : BaseViewModel
    {
        public ChangeSetViewModelItem Data { get; set; }

        public ChangeSetViewModel()
        {
            Data = new ChangeSetViewModelItem();
        }
    }

    public class ChangeSetViewModelItem : BaseViewModelItem
    {
        public DateTime? CateringOrder { get; set; }
        public DateTime? CateringProduct { get; set; }
        public DateTime? CateringOrderDetail { get; set; }
        public DateTime? Logs { get; set; }
        public DateTime? Partner { get; set; }
        public DateTime? PartnerPack { get; set; }
        public DateTime? Seat { get; set; }
        public DateTime? Tournament { get; set; }
        public DateTime? TournamentGame { get; set; }
        public DateTime? TournamentTeam { get; set; }
        public DateTime? TournamentParticipant { get; set; }
        public DateTime? TournamentTeamParticipant { get; set; }
        public DateTime? User { get; set; }
    }
}
