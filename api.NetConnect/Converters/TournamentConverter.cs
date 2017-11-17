using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using api.NetConnect.data.ViewModel.Tournament;
using api.NetConnect.DataControllers;
using api.NetConnect.data.ViewModel.Tournament.Backend;

namespace api.NetConnect.Converters
{
    public static partial class ConverterExtensions
    {
        public static void FromModel(this TournamentViewModelItem viewModel, Tournament model)
        {
            viewModel.GameID = model.TournamentGameID;
            viewModel.TeamSize = model.TeamSize;
            viewModel.ChallongeLink = model.ChallongeLink;
            viewModel.Mode = model.Mode;
            viewModel.Start = model.Start;
            viewModel.End = model.End;
            viewModel.GameTitel = model.TournamentGame.Name;
            viewModel.Rules = model.TournamentGame.Rules;
            viewModel.Image = "http://lan-netconnect.de/_api/images/" + model.TournamentGame.ImageContainer.ThumbnailPath;

            viewModel.Player = model.TournamentParticipant.ToList().ConvertAll(x => {
                var vm = new TournamentParticipantViewModelItem();
                vm.FromModel(x);
                return vm;
            });

            viewModel.Teams = model.TournamentTeam.ToList().ConvertAll(x => {
                var vm = new TournamentTeamViewModelItem();
                vm.FromModel(x);
                return vm;
            });

            if(model.Partner != null)
                viewModel.Partner.FromModel(model.Partner);

            viewModel.ParticipantCount = model.TournamentParticipant.Count;
            viewModel.Teams.ForEach(team => viewModel.ParticipantCount += team.Player.Count);
        }
        public static void FromModel(this BackendTournamentViewModelItem viewModel, Tournament model)
        {
            viewModel.ID = model.ID;
            viewModel.ChallongeLink = model.ChallongeLink;
            viewModel.Mode = model.Mode;
            viewModel.Start = model.Start;
            viewModel.End = model.End;
            viewModel.TeamSize = model.TeamSize;

            viewModel.Game.FromModel(model.TournamentGame);

            viewModel.Player = model.TournamentParticipant.ToList().ConvertAll(x => {
                var vm = new BackendTournamentParticipantViewModelItem();
                vm.FromModel(x);
                return vm;
            });

            viewModel.Teams = model.TournamentTeam.ToList().ConvertAll(x => {
                var vm = new BackendTournamentTeamViewModelItem();
                vm.FromModel(x);
                return vm;
            });

            if (model.Partner != null)
                viewModel.Partner.FromModel(model.Partner);

            viewModel.ParticipantCount = model.TournamentParticipant.Count;
            viewModel.Teams.ForEach(team => viewModel.ParticipantCount += team.Player.Count);
        }

        #region FromModel Private Functions
        // Tournament Participant
        private static void FromModel(this TournamentParticipantViewModelItem viewModel, TournamentParticipant model)
        {
            viewModel.ID = model.ID;
            viewModel.FirstName = model.User.FirstName;
            viewModel.LastName = model.User.LastName;
            viewModel.Nickname = model.User.Nickname;
        }
        private static void FromModel(this BackendTournamentParticipantViewModelItem viewModel, TournamentParticipant model)
        {
            viewModel.ID = model.ID;
            viewModel.FirstName = model.User.FirstName;
            viewModel.LastName = model.User.LastName;
            viewModel.Nickname = model.User.Nickname;
        }
        private static void FromModel(this TournamentParticipantViewModelItem viewModel, TournamentTeamParticipant model)
        {
            viewModel.ID = model.ID;
            viewModel.FirstName = model.User.FirstName;
            viewModel.LastName = model.User.LastName;
            viewModel.Nickname = model.User.Nickname;
        }
        private static void FromModel(this BackendTournamentParticipantViewModelItem viewModel, TournamentTeamParticipant model)
        {
            viewModel.ID = model.ID;
            viewModel.FirstName = model.User.FirstName;
            viewModel.LastName = model.User.LastName;
            viewModel.Nickname = model.User.Nickname;
        }

        // Tournament Team
        private static void FromModel(this TournamentTeamViewModelItem viewModel, TournamentTeam model)
        {
            viewModel.ID = model.ID;
            viewModel.Name = model.Name;
            viewModel.HasPassword = !String.IsNullOrEmpty(model.Password);
            viewModel.Player = model.TournamentTeamParticipant.ToList().ConvertAll(x => {
                var vm = new TournamentParticipantViewModelItem();
                vm.FromModel(x);
                return vm;
            });
        }
        private static void FromModel(this BackendTournamentTeamViewModelItem viewModel, TournamentTeam model)
        {
            viewModel.ID = model.ID;
            viewModel.Name = model.Name;
            viewModel.HasPassword = !String.IsNullOrEmpty(model.Password);
            viewModel.Player = model.TournamentTeamParticipant.ToList().ConvertAll(x => {
                var vm = new BackendTournamentParticipantViewModelItem();
                vm.FromModel(x);
                return vm;
            });
        }

        // Tournament Partner
        private static void FromModel(this TournamentViewModelItem.TournamentPartner viewModel, Partner model)
        {
            viewModel.ID = model.ID;
            viewModel.Name = model.Name;
        }
        private static void FromModel(this BackendTournamentViewModelItem.BackendTournamentPartner viewModel, Partner model)
        {
            viewModel.ID = model.ID;
            viewModel.Name = model.Name;
        }

        // Tournament Game
        private static void FromModel(this BackendTournamentGameViewModelItem viewModel, TournamentGame model)
        {
            viewModel.ID = model.ID;
            viewModel.Name = model.Name;
            viewModel.ImagePath = "";
            viewModel.RulesPath = "";
            viewModel.RequireSteamID = model.RequireSteamID;
            viewModel.RequireBattleTag = model.RequireBattleTag;
        }
        #endregion

        public static Tournament ToModel(TournamentViewModelItem viewModel)
        {
            Tournament model = new Tournament();
            if (viewModel.ID != 0)
                model = TournamentDataController.GetItem(viewModel.ID);

            model.ID = viewModel.ID;
            model.TeamSize = viewModel.TeamSize;
            model.ChallongeLink = viewModel.ChallongeLink;
            model.Mode = viewModel.Mode;
            model.Start = viewModel.Start;
            model.End = viewModel.End;
            model.TournamentGameID = viewModel.GameID;

            model.TournamentParticipant = viewModel.Player.ConvertAll(x =>
            {
                var m = new TournamentParticipant();
                m.FromViewModel(x);
                return m;
            });

            model.TournamentTeam = viewModel.Teams.ConvertAll(x =>
            {
                var m = new TournamentTeam();
                m.FromViewModel(x);
                return m;
            });

            return model;
        }

        public static void FromViewModel(this TournamentTeam model, TournamentTeamViewModelItem viewModel)
        {

        }

        public static void FromViewModel(this TournamentParticipant model, TournamentParticipantViewModelItem viewModel)
        {

        }
    }
}