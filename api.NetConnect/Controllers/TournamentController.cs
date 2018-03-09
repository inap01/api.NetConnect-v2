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
    public class TournamentController : BaseController
    {
        #region Frontend
        [HttpGet]
        public IHttpActionResult Get(Int32 eventID)
        {
            TournamentListViewModel viewmodel = new TournamentListViewModel();
            TournamentDataController dataCtrl = new TournamentDataController();
            EventDataController eventDataCtrl = new EventDataController();


            var e = eventDataCtrl.GetItem(eventID);
            var tournaments = dataCtrl.GetItems().Where(x => x.EventID == eventID).ToList();
            
            if (e.End > DateTime.Now)
                if(tournaments.Count() > 0)
                    foreach (var tournament in tournaments)
                    {
                        TournamentViewModelItem item = new TournamentViewModelItem();
                        item.FromModel(tournament);
                        viewmodel.Data.Add(item);
                    }
                else
                    return Info(viewmodel, "Es wurden keine Turniere für dieses Event angelegt.");
            else
                return Warning(viewmodel, "Das Event ist vorbei.");

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Detail(Int32 eventID, Int32 tournamentID)
        {
            TournamentViewModel viewmodel = new TournamentViewModel();
            TournamentDataController dataCtrl = new TournamentDataController();

            try
            {
                viewmodel.Data.FromModel(dataCtrl.GetItem(tournamentID));
            }
            catch(Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [HttpPost]
        public IHttpActionResult CreateTeam(Int32 eventID, Int32 tournamentID, CreateTeamRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();
            TournamentDataController dataCtrl = new TournamentDataController();
            SeatDataController seatDataCtrl = new SeatDataController();
            TournamentTeamDataController teamDataCtrl = new TournamentTeamDataController();
            TournamentTeamParticipantDataController teamParticipantDataCtrl = new TournamentTeamParticipantDataController();

            try
            {
                if(seatDataCtrl.GetCurrentUserSeats(eventID).FindAll(x => x.State >= 2).Count == 0)
                {
                    return Error(viewmodel, "Du bist kein Teilnehmer dieser Veranstaltung. Bitte reserviere einen Platz.");
                }

                var team = teamDataCtrl.Insert(request.ToModel(tournamentID));
                JoinTournamentRequest _tmp = new JoinTournamentRequest() { TeamID = team.ID };
                teamParticipantDataCtrl.Insert(_tmp.ToTeamModel());
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Team wurde erstellt.");
        }

        [HttpPost]
        public IHttpActionResult Join(Int32 eventID, Int32 tournamentID, JoinTournamentRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();
            SeatDataController seatDataCtrl = new SeatDataController();
            TournamentParticipantDataController participantDataCtrl = new TournamentParticipantDataController();
            TournamentTeamParticipantDataController teamParticipantDataCtrl = new TournamentTeamParticipantDataController();

            try
            {
                if (seatDataCtrl.GetCurrentUserSeats(eventID).FindAll(x => x.State >= 2).Count == 0)
                {
                    return Error(viewmodel, "Du bist kein Teilnehmer dieser Veranstaltung. Bitte reserviere einen Platz.");
                }

                if (request.TeamID == null)
                {
                    participantDataCtrl.Insert(request.ToModel(tournamentID));
                }
                else
                {
                    teamParticipantDataCtrl.Insert(request.ToTeamModel());
                }
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Anmeldung erfolgreich.");
        }

        [HttpPut]
        public IHttpActionResult Leave(Int32 eventID, Int32 tournamentID)
        {
            BaseViewModel viewmodel = new BaseViewModel();
            TournamentParticipantDataController participantDataCtrl = new TournamentParticipantDataController();
            TournamentTeamParticipantDataController teamParticipantDataCtrl = new TournamentTeamParticipantDataController();

            try
            {
                var participant = participantDataCtrl.GetItems().SingleOrDefault(x => x.TournamentID == tournamentID && x.UserID == UserHelper.CurrentUserID);
                if(participant != null)
                {
                    participantDataCtrl.Delete(participant.ID);
                }
                else
                {
                    var teamParticipant = teamParticipantDataCtrl.GetItemByTournament(tournamentID);
                    if (teamParticipant != null)
                    {
                        teamParticipantDataCtrl.Delete(teamParticipant.ID);
                    }
                    else
                    {
                        return Warning(viewmodel, "Du bist nicht angemeldet.");
                    }
                }

            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Abmeldung erfolgreich.");
        }
        #endregion
        #region Backend
        [Authorize(Roles = "Admin,Team")]
        [HttpGet]
        public IHttpActionResult Backend_Get()
        {
            BackendTournamentListViewModel viewmodel = new BackendTournamentListViewModel();
            BackendTournamentListArgs args = new BackendTournamentListArgs();
            TournamentDataController dataCtrl = new TournamentDataController();
            TournamentGameDataController gameDataCtrl = new TournamentGameDataController();
            EventDataController eventDataCtrl = new EventDataController();

            try
            {
                viewmodel.Filter.GameOptions = gameDataCtrl.GetItems().OrderBy(x => x.Name).ToList().ConvertAll(x => {
                    return new BackendTournamentFilter.TournamentFilterGame()
                    {
                        ID = x.ID,
                        Name = x.Name
                    };
                });
                viewmodel.Filter.EventOptions = eventDataCtrl.GetItems().ToList().ConvertAll(x => {
                    return new BackendTournamentFilter.TournamentFilterEvent()
                    {
                        ID = x.ID,
                        Name = x.EventType.Name + " Vol. " + x.Volume.ToString()
                    };
                }).OrderByDescending(x => x.ID).ToList();
                viewmodel.Filter.EventSelected = viewmodel.Filter.EventOptions[0];
                args.Filter.EventSelected = viewmodel.Filter.EventSelected;

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
        public IHttpActionResult Backend_FilterList(BackendTournamentListArgs args)
        {
            BackendTournamentListViewModel viewmodel = new BackendTournamentListViewModel();
            TournamentDataController dataCtrl = new TournamentDataController();
            TournamentGameDataController gameDataCtrl = new TournamentGameDataController();
            EventDataController eventDataCtrl = new EventDataController();

            try
            {
                viewmodel.Filter.GameSelected = args.Filter.GameSelected;
                viewmodel.Filter.EventSelected = args.Filter.EventSelected;
                viewmodel.Filter.GameOptions = gameDataCtrl.GetItems().OrderBy(x => x.Name).ToList().ConvertAll(x => {
                    return new BackendTournamentFilter.TournamentFilterGame()
                    {
                        ID = x.ID,
                        Name = x.Name
                    };
                });
                viewmodel.Filter.EventOptions = eventDataCtrl.GetItems().ToList().ConvertAll(x => {
                    return new BackendTournamentFilter.TournamentFilterEvent()
                    {
                        ID = x.ID,
                        Name = x.EventType.Name + " Vol. " + x.Volume.ToString()
                    };
                }).OrderByDescending(x => x.ID).ToList();
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
            BackendTournamentViewModel viewmodel = new BackendTournamentViewModel();
            TournamentDataController dataCtrl = new TournamentDataController();
            TournamentGameDataController gameDataCtrl = new TournamentGameDataController();
            EventDataController eventDataCtrl = new EventDataController();

            try
            {
                viewmodel.EventOptions = eventDataCtrl.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendEventViewModelItem().FromModel(x);
                }).OrderByDescending(x => x.ID).ToList();
                viewmodel.GameOptions = gameDataCtrl.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendGameViewModelItem().FromModel(x);
                }).OrderBy(x => x.Name).ToList();

                viewmodel.Data.FromModel(dataCtrl.GetItem(id));
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpGet]
        public IHttpActionResult Backend_Detail_New()
        {
            BackendTournamentViewModel viewmodel = new BackendTournamentViewModel();
            TournamentGameDataController gameDataCtrl = new TournamentGameDataController();
            EventDataController eventDataCtrl = new EventDataController();

            try
            {
                viewmodel.EventOptions = eventDataCtrl.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendEventViewModelItem().FromModel(x);
                }).OrderByDescending(x => x.ID).ToList();
                viewmodel.GameOptions = gameDataCtrl.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendGameViewModelItem().FromModel(x);
                }).OrderBy(x => x.Name).ToList();

                viewmodel.Data.Event = viewmodel.EventOptions[0];
                viewmodel.Data.Start = viewmodel.Data.Event.Start;
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpPost]
        public IHttpActionResult Backend_Detail_Insert(BackendTournamentViewModelItem request)
        {
            BackendTournamentViewModel viewmodel = new BackendTournamentViewModel();
            TournamentDataController dataCtrl = new TournamentDataController();
            TournamentGameDataController gameDataCtrl = new TournamentGameDataController();
            EventDataController eventDataCtrl = new EventDataController();


            try
            {
                viewmodel.EventOptions = eventDataCtrl.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendEventViewModelItem().FromModel(x);
                }).OrderByDescending(x => x.ID).ToList();
                viewmodel.GameOptions = gameDataCtrl.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendGameViewModelItem().FromModel(x);
                }).OrderBy(x => x.Name).ToList();

                var data = dataCtrl.Insert(request.ToModel());
                viewmodel.Data.FromModel(data);
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Das Turnier wurder erstellt.");
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpPut]
        public IHttpActionResult Backend_Detail_Update(Int32 id, BackendTournamentViewModelItem request)
        {
            BackendTournamentViewModel viewmodel = new BackendTournamentViewModel();
            TournamentDataController dataCtrl = new TournamentDataController();
            TournamentGameDataController gameDataCtrl = new TournamentGameDataController();
            EventDataController eventDataCtrl = new EventDataController();

            try
            {
                viewmodel.EventOptions = eventDataCtrl.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendEventViewModelItem().FromModel(x);
                }).OrderByDescending(x => x.ID).ToList();
                viewmodel.GameOptions = gameDataCtrl.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendGameViewModelItem().FromModel(x);
                }).OrderBy(x => x.Name).ToList();

                var data = dataCtrl.Update(request.ToModel());
                viewmodel.Data.FromModel(data);
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Speichern erfolgreich.");
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpDelete]
        public IHttpActionResult Backend_Delete(Int32[] IDs)
        {
            BaseViewModel viewmodel = new BaseViewModel();
            TournamentDataController dataCtrl = new TournamentDataController();

            try
            {
                foreach(var id in IDs)
                    dataCtrl.Delete(id);
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            if(IDs.Count() <= 1)
                return Ok(viewmodel, "Eintrag wurden gelöscht");
            return Ok(viewmodel, IDs.Count() + " Einträge wurden gelöscht");
        }
        #endregion
    }
}
