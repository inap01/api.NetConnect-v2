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
        DateTime CateringOrders { get; set; }
        DateTime CateringProducts { get; set; }
        DateTime Chat { get; set; }
        DateTime News { get; set; }
        DateTime NewsCategories { get; set; }
        DateTime Partner { get; set; }
        DateTime PartnerPacks { get; set; }
        DateTime Seating { get; set; }
        DateTime Settings { get; set; }
        DateTime Tournament { get; set; }
        DateTime TournamentGame { get; set; }
        DateTime TournamentTeam { get; set; }
        DateTime TournamentParticipant { get; set; }
        DateTime User { get; set; }
    }
}
