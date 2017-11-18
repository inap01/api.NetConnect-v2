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

namespace api.NetConnect.Controllers
{
    using TournamentListViewModel = ListViewModel<TournamentViewModelItem>;
    using BackendTournamentListArgs = ListArgsRequest<BackendTournamentFilter>;
    using BackendTournamentListViewModel = ListArgsViewModel<BackendTournamentViewModelItem, BackendTournamentFilter>;

    public class TournamentController : ApiController
    {
        #region Frontend
        [HttpGet]
        public IHttpActionResult Get(Int32 eventID)
        {
            TournamentListViewModel viewmodel = new TournamentListViewModel();
            //var tournaments = TournamentDataController.GetByEvent(eventID);
            var tournaments = TournamentDataController.GetByEvent(8);

            foreach (var tournament in tournaments)
            {
                TournamentViewModelItem item = new TournamentViewModelItem();
                item.FromModel(tournament);
                viewmodel.Data.Add(item);
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Detail(Int32 id)
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
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }
        #endregion
        #region Backend
        [HttpGet]
        public IHttpActionResult Backend_Get()
        {
            BackendTournamentListViewModel viewmodel = new BackendTournamentListViewModel();
            BackendTournamentListArgs args = new BackendTournamentListArgs();

            try
            {
                viewmodel.Filter.GameOptions = TournamentGameDataController.GetItems().OrderBy(x => x.Name).ToList().ConvertAll(x => {
                    return new BackendTournamentFilter.TournamentFilterGame()
                    {
                        ID = x.ID,
                        Name = x.Name
                    };
                });
                viewmodel.Filter.EventOptions = EventDataController.GetItems().OrderBy(x => x.EventType.Name).ToList().ConvertAll(x => {
                    return new BackendTournamentFilter.TournamentFilterEvent()
                    {
                        ID = x.ID,
                        Name = x.EventType.Name + " Vol. " + x.Volume.ToString()
                    };
                });

                Int32 TotalItemsCount = 0;
                viewmodel.Data = TournamentConverter.FilterList(args, out TotalItemsCount);

                viewmodel.Pagination.TotalItemsCount = TotalItemsCount;
            }
            catch(Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult Backend_FilterList(BackendTournamentListArgs args)
        {
            BackendTournamentListViewModel viewmodel = new BackendTournamentListViewModel();

            try
            {
                viewmodel.Filter.GameSelected = args.Filter.GameSelected;
                viewmodel.Filter.EventSelected = args.Filter.EventSelected;
                viewmodel.Filter.GameOptions = TournamentGameDataController.GetItems().OrderBy(x => x.Name).ToList().ConvertAll(x => {
                    return new BackendTournamentFilter.TournamentFilterGame()
                    {
                        ID = x.ID,
                        Name = x.Name
                    };
                });
                viewmodel.Filter.EventOptions = EventDataController.GetItems().OrderBy(x => x.EventType.Name).ToList().ConvertAll(x => {
                    return new BackendTournamentFilter.TournamentFilterEvent()
                    {
                        ID = x.ID,
                        Name = x.EventType.Name + " Vol. " + x.Volume.ToString()
                    };
                });
                viewmodel.Pagination = args.Pagination;

                Int32 TotalItemsCount = 0;
                viewmodel.Data = TournamentConverter.FilterList(args, out TotalItemsCount);

                viewmodel.Pagination.TotalItemsCount = TotalItemsCount;
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Backend_Detail(Int32 id)
        {
            BackendTournamentViewModel viewmodel = new BackendTournamentViewModel();

            try
            {
                viewmodel.GameOptions = TournamentGameDataController.GetItems().ConvertAll(x =>
                {
                    return new BackendTournamentGameViewModelItem().FromModel(x);
                }).OrderBy(x => x.Name).ToList();
                viewmodel.Data.FromModel(TournamentDataController.GetItem(id));
            }
            catch(Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPost]
        public IHttpActionResult Backend_Detail_Insert(TournamentViewModelItem request)
        {
            TournamentViewModel viewmodel = new TournamentViewModel();

            try
            {
                // TODO
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult Backend_Detail_Update(Int32 id, TournamentViewModelItem request)
        {
            TournamentViewModel viewmodel = new TournamentViewModel();

            try
            {
                // TODO
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpDelete]
        public IHttpActionResult Backend_Delete(BackendTournamentDeleteRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();

            try
            {
                // TODO
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }
        #endregion
    }
}
