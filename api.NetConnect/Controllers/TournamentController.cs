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
using api.NetConnect.Helper;
using api.NetConnect.data.ViewModel.Tournament.Backend;

namespace api.NetConnect.Controllers
{
    using TournamentListViewModel = ListViewModel<TournamentViewModelItem>;
    using BackendTournamentListViewModel = ListViewModel<BackendTournamentViewModelItem>;

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

            // TODO

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Backend_Detail(Int32 id)
        {
            BackendTournamentViewModel viewmodel = new BackendTournamentViewModel();

            BackendTournamentGameViewModelItem g = new BackendTournamentGameViewModelItem()
            {
                ID = 1,
                Name = "Counterstrike GO",
                TeamSize = 5,
                ImagePath = "",
                RulesPath = "",
                RequireBattleTag = false,
                RequireSteamID = true
            };
            BackendTournamentParticipantViewModelItem p = new BackendTournamentParticipantViewModelItem()
            {
                ID = 1,
                Name = "Marius Hartmann",
                Nickname = "iNap"
            };
            BackendTournamentTeamViewModelItem t = new BackendTournamentTeamViewModelItem()
            {
                ID = 1,
                Name = "Example Team Name",
                HasPassword = false
            };
            t.Player.Add(p);
            t.Player.Add(p);
            t.Player.Add(p);
            t.Player.Add(p);
            t.Player.Add(p);
            viewmodel.Data = new BackendTournamentViewModelItem()
            {
                ID = 1,
                ChallongeLink = "http://challonge.com",
                Mode = "5vs5",
                Start = DateTime.Now,
                End = DateTime.Now,
                Game = g
            };
            viewmodel.Data.Player.Add(p);
            viewmodel.Data.Player.Add(p);
            viewmodel.Data.Teams.Add(t);
            viewmodel.Data.Teams.Add(t);

            viewmodel.Form.Add("ID", new BackendBaseViewModel.InputInformation() { Type = "integer", Readonly = true });
            viewmodel.Form.Add("ChallongeLink", new BackendBaseViewModel.InputInformation() { Type = "string" });
            viewmodel.Form.Add("Mode", new BackendBaseViewModel.InputInformation() { Type = "string" });
            viewmodel.Form.Add("Start", new BackendBaseViewModel.InputInformation() { Type = "datetime" });
            viewmodel.Form.Add("End", new BackendBaseViewModel.InputInformation() { Type = "datetime" });
            var refGame = new Dictionary<string, BackendBaseViewModel.InputInformation>();
            refGame.Add("ID", new BackendBaseViewModel.InputInformation() { Type = "integer", Readonly = true });
            refGame.Add("Name", new BackendBaseViewModel.InputInformation() { Type = "string", Readonly = true });
            refGame.Add("TeamSize", new BackendBaseViewModel.InputInformation() { Type = "integer", Readonly = true });
            refGame.Add("ImagePath", new BackendBaseViewModel.InputInformation() { Type = "string", Readonly = true });
            refGame.Add("RulesPath", new BackendBaseViewModel.InputInformation() { Type = "string", Readonly = true });
            refGame.Add("RequireBattleTag", new BackendBaseViewModel.InputInformation() { Type = "boolean", Readonly = true });
            refGame.Add("RequireSteamID", new BackendBaseViewModel.InputInformation() { Type = "boolean", Readonly = true });
            viewmodel.Form.Add("Game", new BackendBaseViewModel.InputInformation() { Type = "reference", Reference = "Game", ReferenceForm = refGame });
            var refPlayer = new Dictionary<string, BackendBaseViewModel.InputInformation>();
            refPlayer.Add("ID", new BackendBaseViewModel.InputInformation() { Type = "integer", Readonly = true });
            refPlayer.Add("Name", new BackendBaseViewModel.InputInformation() { Type = "string", Readonly = true });
            refPlayer.Add("Nickname", new BackendBaseViewModel.InputInformation() { Type = "string", Readonly = true });
            viewmodel.Form.Add("Player", new BackendBaseViewModel.InputInformation() { Type = "reference", Reference = "Player", ReferenceForm = refPlayer });
            var refTeam = new Dictionary<string, BackendBaseViewModel.InputInformation>();
            refTeam.Add("ID", new BackendBaseViewModel.InputInformation() { Type = "integer", Readonly = true });
            refTeam.Add("Name", new BackendBaseViewModel.InputInformation() { Type = "string", Readonly = true });
            refTeam.Add("HasPassword", new BackendBaseViewModel.InputInformation() { Type = "boolean", Readonly = true });
            viewmodel.Form.Add("Teams", new BackendBaseViewModel.InputInformation() { Type = "reference", Reference = "Team", ReferenceForm = refTeam });

            return Ok(viewmodel);
        }

        [HttpPost]
        public IHttpActionResult Backend_Detail_Insert(TournamentViewModelItem request)
        {
            TournamentViewModel viewmodel = new TournamentViewModel();

            // TODO

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult Backend_Detail_Update(Int32 id, TournamentViewModelItem request)
        {
            TournamentViewModel viewmodel = new TournamentViewModel();

            // TODO

            return Ok(viewmodel);
        }

        [HttpDelete]
        public IHttpActionResult Backend_Delete(BackendTournamentDeleteRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();

            return Ok(viewmodel);
        }
        #endregion
    }
}
