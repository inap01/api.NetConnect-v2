using api.NetConnect.DataControllers;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Tournament;
using api.NetConnect.data.ViewModel.Tournament.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api.NetConnect.Converters;
using api.NetConnect.Helper;
using api.NetConnect.data.ViewModel.Event.Backend;
using api.NetConnect.data.ViewModel.Game.Backend;

namespace api.NetConnect.Controllers
{
    public class GameController : BaseController
    {
        #region Backend
        [Authorize(Roles = "Admin,Team")]
        [HttpGet]
        public IHttpActionResult Backend_Get()
        {
            BackendGameListViewModel viewmodel = new BackendGameListViewModel();
            BackendGameListArgs args = new BackendGameListArgs();
            TournamentGameDataController dataCtrl = new TournamentGameDataController();

            try
            {
                Int32 TotalItemsCount = 0;
                viewmodel.Data.FromModel(dataCtrl.FilterList(args, out TotalItemsCount));

                viewmodel.Pagination.TotalItemsCount = TotalItemsCount;
            }
            catch(Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpPut]
        public IHttpActionResult Backend_FilterList(BackendGameListArgs args)
        {
            BackendGameListViewModel viewmodel = new BackendGameListViewModel();
            TournamentGameDataController dataCtrl = new TournamentGameDataController();

            try
            {
                viewmodel.Filter.Name = args.Filter.Name;
                viewmodel.Pagination = args.Pagination;

                Int32 TotalItemsCount = 0;
                viewmodel.Data.FromModel(dataCtrl.FilterList(args, out TotalItemsCount));

                viewmodel.Pagination.TotalItemsCount = TotalItemsCount;
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpGet]
        public IHttpActionResult Backend_Detail(Int32 id)
        {
            BackendGameViewModel viewmodel = new BackendGameViewModel();
            TournamentGameDataController dataCtrl = new TournamentGameDataController();

            try
            {
                viewmodel.Data.FromModel(dataCtrl.GetItem(id));
            }
            catch(Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpPost]
        public IHttpActionResult Backend_Detail_Insert(BackendGameViewModelItem request)
        {
            BackendGameViewModel viewmodel = new BackendGameViewModel();

            try
            {
                // TODO
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpPut]
        public IHttpActionResult Backend_Detail_Update(Int32 id, BackendGameViewModelItem request)
        {
            BackendGameViewModel viewmodel = new BackendGameViewModel();
            TournamentGameDataController dataCtrl = new TournamentGameDataController();

            try
            {
                var data = dataCtrl.Update(request.ToModel());
                viewmodel.Data.FromModel(data);
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Eintrag wurde gespeichert.");
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpDelete]
        public IHttpActionResult Backend_Delete(Int32[] IDs)
        {
            BaseViewModel viewmodel = new BaseViewModel();

            try
            {
                // TODO
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }
        #endregion
    }
}
