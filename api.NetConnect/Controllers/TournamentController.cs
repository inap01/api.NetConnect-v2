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
using System.Data.Entity.Validation;

namespace api.NetConnect.Controllers
{
    public class TournamentController : ApiController
    {
        #region Frontend
        [HttpGet]
        public IHttpActionResult Get(Int32 eventID)
        {
            TournamentListViewModel viewmodel = new TournamentListViewModel();
            viewmodel.Authenticated = UserHelper.Authenticated;
            var e = EventDataController.GetItem(eventID);
            var tournaments = TournamentDataController.GetByEvent(eventID);
            
            if (e.End > DateTime.Now)
                if(tournaments.Count > 0)
                    foreach (var tournament in tournaments)
                    {
                        TournamentViewModelItem item = new TournamentViewModelItem();
                        item.FromModel(tournament);
                        viewmodel.Data.Add(item);
                    }
                else
                    viewmodel.AddInfoAlert("Es wurden keine Turniere für dieses Event angelegt.");
            else
                viewmodel.AddWarningAlert("Das Event ist vorbei.");

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Detail(Int32 eventID, Int32 tournamentID)
        {
            TournamentViewModel viewmodel = new TournamentViewModel();
            viewmodel.Authenticated = UserHelper.Authenticated;

            try
            {
                viewmodel.Data.FromModel(TournamentDataController.GetItem(tournamentID));
            }
            catch(Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPost]
        public IHttpActionResult CreateTeam(Int32 eventID, Int32 tournamentID, CreateTeamRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();

            try
            {
                var team = TournamentTeamDataController.Insert(request.ToModel(tournamentID));
                JoinTournamentRequest _tmp = new JoinTournamentRequest() { TeamID = team.ID };
                TournamentTeamParticipantDataController.Insert(_tmp.ToTeamModel());

                viewmodel.AddSuccessAlert("Team wurde erstellt.");
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPost]
        public IHttpActionResult Join(Int32 eventID, Int32 tournamentID, JoinTournamentRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();

            try
            {
                if(request.TeamID == null)
                {
                    TournamentParticipantDataController.Insert(request.ToModel(tournamentID));
                }
                else
                {
                    TournamentTeamParticipantDataController.Insert(request.ToTeamModel());
                }

                viewmodel.AddSuccessAlert("Anmeldung erfolgreich.");
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult Leave(Int32 eventID, Int32 tournamentID)
        {
            BaseViewModel viewmodel = new BaseViewModel();

            try
            {
                var participant = TournamentParticipantDataController.GetByTournament(tournamentID);
                if(participant != null)
                {
                    TournamentParticipantDataController.Delete(tournamentID);
                    viewmodel.AddSuccessAlert("Abmeldung erfolgreich.");
                }
                else
                {
                    var teamParticipant = TournamentTeamParticipantDataController.GetByTournament(tournamentID);
                    if (teamParticipant != null)
                    {
                        TournamentTeamParticipantDataController.Delete(tournamentID);
                        viewmodel.AddSuccessAlert("Abmeldung erfolgreich.");
                    }
                    viewmodel.Success = false;
                    viewmodel.AddWarningAlert("Du bist nicht angemeldet.");
                }

            }
            catch (Exception ex)
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
                viewmodel.EventOptions = EventDataController.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendEventViewModelItem().FromModel(x);
                }).OrderByDescending(x => x.ID).ToList();
                viewmodel.GameOptions = TournamentGameDataController.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendGameViewModelItem().FromModel(x);
                }).OrderBy(x => x.Name).ToList();

                viewmodel.Data.FromModel(TournamentDataController.GetItem(id));
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
        public IHttpActionResult Backend_Detail_New()
        {
            BackendTournamentViewModel viewmodel = new BackendTournamentViewModel();

            try
            {
                viewmodel.EventOptions = EventDataController.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendEventViewModelItem().FromModel(x);
                }).OrderByDescending(x => x.ID).ToList();
                viewmodel.GameOptions = TournamentGameDataController.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendGameViewModelItem().FromModel(x);
                }).OrderBy(x => x.Name).ToList();

            viewmodel.Data.Start = DateTime.Now;
                viewmodel.Data.End = DateTime.Now;
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPost]
        public IHttpActionResult Backend_Detail_Insert(BackendTournamentViewModelItem request)
        {
            BackendTournamentViewModel viewmodel = new BackendTournamentViewModel();

            try
            {
                viewmodel.EventOptions = EventDataController.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendEventViewModelItem().FromModel(x);
                }).OrderByDescending(x => x.ID).ToList();
                viewmodel.GameOptions = TournamentGameDataController.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendGameViewModelItem().FromModel(x);
                }).OrderBy(x => x.Name).ToList();

                var data = TournamentDataController.Insert(request.ToModel());
                viewmodel.Data.FromModel(data);

                viewmodel.AddSuccessAlert("Das Turnier wurder erstellt.");
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
        public IHttpActionResult Backend_Detail_Update(Int32 id, BackendTournamentViewModelItem request)
        {
            BackendTournamentViewModel viewmodel = new BackendTournamentViewModel();

            try
            {
                viewmodel.EventOptions = EventDataController.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendEventViewModelItem().FromModel(x);
                }).OrderByDescending(x => x.ID).ToList();
                viewmodel.GameOptions = TournamentGameDataController.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendGameViewModelItem().FromModel(x);
                }).OrderBy(x => x.Name).ToList();

                var data = TournamentDataController.Update(request.ToModel());
                viewmodel.Data.FromModel(data);

                viewmodel.AddSuccessAlert("Speichern erfolgreich.");
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
