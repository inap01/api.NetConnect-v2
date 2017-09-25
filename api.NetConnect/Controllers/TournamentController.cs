using api.NetConnect.DataControllers;
using api.NetConnect.data.ViewModel.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api.NetConnect.Converters;
using api.NetConnect.data.ViewModel;

namespace api.NetConnect.Controllers
{
    using TournamentListViewModel = ListArgsQuery<TournamentViewModelItem, TournamentFilter, TournamentSortSettings>;

    public class TournamentController : ApiController
    {
        [HttpPut]
        public IHttpActionResult FilterList()
        {
            TournamentListViewModel viewmodel = new TournamentListViewModel();

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult GetItem(Int32 id)
        {
            TournamentViewModel viewmodel = new TournamentViewModel();

            try
            {
                viewmodel.Data.FromModel(TournamentDataController.GetItem(id));
            }
            catch(Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ex.Message);
            }

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult UpdateItem(Int32 id, TournamentViewModelItem requestViewModel)
        {
            TournamentViewModel viewmodel = new TournamentViewModel();

            try
            {
                
            }
            catch(Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ex.Message);
            }

            return Ok(viewmodel);
        }
    }
}
