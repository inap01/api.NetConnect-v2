using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.ViewModels.Tournament.Details
{
    public class TournamentDetailsViewModel : BaseViewModel
    {
        public TournamentViewModelItem Data { get; set; }

        public TournamentDetailsViewModel()
        {
            Data = new TournamentViewModelItem();
        }
    }
}