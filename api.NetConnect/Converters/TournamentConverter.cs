using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using api.NetConnect.data.ViewModel.Tournament;
using api.NetConnect.DataControllers;
using api.NetConnect.data.ViewModel.Tournament.Backend;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Game.Backend;
using api.NetConnect.data.ViewModel.User.Backend;
using api.NetConnect.Helper;

namespace api.NetConnect.Converters
{
    public static partial class ConverterExtensions
    {
        public static TournamentViewModelItem FromModel(this TournamentViewModelItem viewModel, Tournament model)
        {
            Int32 UserID = UserHelper.CurrentUserID;

            viewModel.ID = model.ID;
            viewModel.GameID = model.TournamentGameID;
            viewModel.TeamSize = model.TeamSize;
            viewModel.ChallongeLink = model.ChallongeLink;
            viewModel.Mode = model.Mode;
            viewModel.Start = model.Start;
            viewModel.End = model.End;
            viewModel.GameTitle = model.TournamentGame.Name;
            viewModel.Rules = model.TournamentGame.Rules;
            //viewModel.Image = Properties.Settings.Default.imageAbsolutePath + model.TournamentGame.ImageContainer.ThumbnailPath;
            viewModel.Image = "http://lan-netconnect.de/_api/images/gallery/8/__preview.jpg";
            viewModel.Event.FromModel(model.Event);

            viewModel.Player = model.TournamentParticipant.ToList().ConvertAll(x => {
                if (x.UserID == UserID)
                    viewModel.IsParticipant = true;

                var vm = new TournamentParticipantViewModelItem();
                vm.FromModel(x);
                return vm;
            });

            viewModel.Teams = model.TournamentTeam.ToList().Where(x => x.TournamentTeamParticipant.Count > 0).ToList().ConvertAll(x => {
                if (x.TournamentTeamParticipant.FirstOrDefault(p => p.UserID == UserID) != null)
                    viewModel.IsParticipant = true;

                var vm = new TournamentTeamViewModelItem();
                vm.FromModel(x);
                return vm;
            });

            if(model.Partner != null)
                viewModel.Partner.FromModel(model.Partner);

            viewModel.ParticipantCount = model.TournamentParticipant.Count;
            viewModel.Teams.ForEach(team => viewModel.ParticipantCount += team.Player.Count);

            return viewModel;
        }
        public static List<BackendTournamentViewModelItem> FromModel(this List<BackendTournamentViewModelItem> viewModel, List<Tournament> model)
        {
            viewModel = model.ConvertAll(x =>
            {
                return new BackendTournamentViewModelItem().FromModel(x);
            });

            return viewModel;
        }
        public static BackendTournamentViewModelItem FromModel(this BackendTournamentViewModelItem viewModel, Tournament model)
        {
            viewModel.ID = model.ID;
            viewModel.ChallongeLink = model.ChallongeLink;
            viewModel.Mode = model.Mode;
            viewModel.Start = model.Start;
            viewModel.End = model.End;
            viewModel.TeamSize = model.TeamSize;

            viewModel.Event.FromModel(model.Event);
            viewModel.Game.FromModel(model.TournamentGame);

            viewModel.Player = model.TournamentParticipant.ToList().ConvertAll(x => {
                var vm = new BackendUserViewModelItem();
                vm.FromModel(x);
                return vm;
            });

            viewModel.Teams = model.TournamentTeam.Where(x => x.TournamentTeamParticipant.Count > 0).ToList().ConvertAll(x => {
                var vm = new BackendTournamentTeamViewModelItem();
                vm.FromModel(x);
                return vm;
            });

            if (model.Partner != null)
                viewModel.Partner.FromModel(model.Partner);

            viewModel.ParticipantCount = model.TournamentParticipant.Count;
            viewModel.Teams.ForEach(team => viewModel.ParticipantCount += team.Player.Count);

            return viewModel;
        }

        // Tournament Participant
        public static TournamentParticipantViewModelItem FromModel(this TournamentParticipantViewModelItem viewModel, TournamentParticipant model)
        {
            viewModel.ID = model.ID;
            viewModel.UserID = model.UserID;
            viewModel.FirstName = model.User.FirstName;
            viewModel.LastName = model.User.LastName;
            viewModel.Nickname = model.User.Nickname;

            return viewModel;
        }
        public static TournamentParticipantViewModelItem FromModel(this TournamentParticipantViewModelItem viewModel, TournamentTeamParticipant model)
        {
            viewModel.ID = model.ID;
            viewModel.UserID = model.UserID;
            viewModel.FirstName = model.User.FirstName;
            viewModel.LastName = model.User.LastName;
            viewModel.Nickname = model.User.Nickname;

            return viewModel;
        }

        // Tournament Team
        public static TournamentTeamViewModelItem FromModel(this TournamentTeamViewModelItem viewModel, TournamentTeam model)
        {
            viewModel.ID = model.ID;
            viewModel.Name = model.Name;
            viewModel.HasPassword = !String.IsNullOrEmpty(model.Password);
            viewModel.Player = model.TournamentTeamParticipant.ToList().ConvertAll(x => {
                var vm = new TournamentParticipantViewModelItem();
                vm.FromModel(x);
                return vm;
            });

            return viewModel;
        }
        public static BackendTournamentTeamViewModelItem FromModel(this BackendTournamentTeamViewModelItem viewModel, TournamentTeam model)
        {
            viewModel.ID = model.ID;
            viewModel.Name = model.Name;
            viewModel.HasPassword = !String.IsNullOrEmpty(model.Password);
            viewModel.Player = model.TournamentTeamParticipant.ToList().ConvertAll(x => {
                var vm = new BackendUserViewModelItem();
                vm.FromModel(x);
                return vm;
            });

            return viewModel;
        }
        public static TournamentTeam ToModel(this CreateTeamRequest request, Int32 TournamentID)
        {
            TournamentTeam model = new TournamentTeam();

            model.Name = request.Name;
            model.Password = !String.IsNullOrEmpty(request.Password) ? request.Password : null;
            model.TournamentID = TournamentID;

            return model;
        }

        // Tournament Partner
        public static TournamentViewModelItem.TournamentPartner FromModel(this TournamentViewModelItem.TournamentPartner viewModel, Partner model)
        {
            viewModel.ID = model.ID;
            viewModel.Name = model.Name;

            return viewModel;
        }
        public static BackendTournamentViewModelItem.BackendTournamentPartner FromModel(this BackendTournamentViewModelItem.BackendTournamentPartner viewModel, Partner model)
        {
            viewModel.ID = model.ID;
            viewModel.Name = model.Name;

            return viewModel;
        }

        // Tournament Game
        public static List<BackendGameViewModelItem> FromModel(this List<BackendGameViewModelItem> viewModel, List<TournamentGame> model)
        {
            viewModel = model.ConvertAll(x =>
            {
                return new BackendGameViewModelItem().FromModel(x);
            });

            return viewModel;
        }

        public static BackendGameViewModelItem FromModel(this BackendGameViewModelItem viewModel, TournamentGame model)
        {
            viewModel.ID = model.ID;
            viewModel.Name = model.Name;
            viewModel.ImagePath = "";
            viewModel.RulesPath = "";
            viewModel.RequireSteamID = model.RequireSteamID;
            viewModel.RequireBattleTag = model.RequireBattleTag;

            return viewModel;
        }

        public static TournamentParticipant ToModel(this JoinTournamentRequest viewModel, Int32 TournamentID)
        {
            TournamentParticipant model = new TournamentParticipant();
            
            model.UserID = UserHelper.CurrentUserID;
            model.TournamentID = TournamentID;
            model.Registered = DateTime.Now;

            return model;
        }

        public static TournamentTeamParticipant ToTeamModel(this JoinTournamentRequest viewModel)
        {
            TournamentTeamParticipant model = new TournamentTeamParticipant();

            model.UserID = UserHelper.CurrentUserID;
            model.TournamentTeamID = viewModel.TeamID ?? default(int);
            model.Registered = DateTime.Now;

            return model;
        }

        public static Tournament ToModel(this BackendTournamentViewModelItem viewmodel)
        {
            Tournament model = new Tournament();

            model.ID = viewmodel.ID;
            model.EventID = viewmodel.Event.ID;
            model.TournamentGameID = viewmodel.Game.ID;
            model.Mode = viewmodel.Mode;
            model.TeamSize = viewmodel.TeamSize;
            model.Start = viewmodel.Start;
            model.End = viewmodel.End;
            model.ChallongeLink = viewmodel.ChallongeLink;
            if(viewmodel.Partner != null)
                model.PartnerID = viewmodel.Partner.ID;

            return model;
        }
    }
}