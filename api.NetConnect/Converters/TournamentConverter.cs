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

namespace api.NetConnect.Converters
{
    public static partial class ConverterExtensions
    {
        public static TournamentViewModelItem FromModel(this TournamentViewModelItem viewModel, Tournament model)
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

            viewModel.Teams = model.TournamentTeam.ToList().Where(x => x.TournamentTeamParticipant.Count > 0).ToList().ConvertAll(x => {
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
        public static BackendTournamentViewModelItem FromModel(this BackendTournamentViewModelItem viewModel, Tournament model)
        {
            viewModel.ID = model.ID;
            viewModel.ChallongeLink = model.ChallongeLink;
            viewModel.Mode = model.Mode;
            viewModel.Start = model.Start;
            viewModel.End = model.End;
            viewModel.TeamSize = model.TeamSize;

            viewModel.Event.FromModel(model.Event);
            viewModel.GameSelected.FromModel(model.TournamentGame);

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
            viewModel.FirstName = model.User.FirstName;
            viewModel.LastName = model.User.LastName;
            viewModel.Nickname = model.User.Nickname;

            return viewModel;
        }
        public static TournamentParticipantViewModelItem FromModel(this TournamentParticipantViewModelItem viewModel, TournamentTeamParticipant model)
        {
            viewModel.ID = model.ID;
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

    public class TournamentConverter
    {
        public static List<BackendTournamentViewModelItem> FilterList(ListArgsRequest<BackendTournamentFilter> args, out Int32 TotalCount)
        {
            List<BackendTournamentViewModelItem> result = new List<BackendTournamentViewModelItem>();

            var items = TournamentDataController.GetItems().OrderByDescending(x => x.EventID).ToList();

            if (args.Filter.GameSelected.ID != -1)
                items = items.Where(x => x.TournamentGameID == args.Filter.GameSelected.ID).ToList();

            if (args.Filter.EventSelected.ID != -1)
                items = items.Where(x => x.EventID == args.Filter.EventSelected.ID).ToList();

            TotalCount = items.Count;

            items = items.Skip(args.Pagination.ItemsPerPageSelected * (args.Pagination.Page - 1))
                 .Take(args.Pagination.ItemsPerPageSelected)
                 .ToList();

            foreach (var model in items)
            {
                BackendTournamentViewModelItem item = new BackendTournamentViewModelItem();
                item.FromModel(model);
                result.Add(item);
            }

            return result;
        }
    }

    public class GameConverter
    {
        public static List<BackendGameViewModelItem> FilterList(ListArgsRequest<BackendGameFilter> args, out Int32 TotalCount)
        {
            List<BackendGameViewModelItem> result = new List<BackendGameViewModelItem>();

            var items = TournamentGameDataController.GetItems().OrderBy(x => x.Name).ToList();
            
            items = items.Where(x => x.Name.ToLower().IndexOf(args.Filter.Name.ToLower()) != -1).ToList();

            TotalCount = items.Count;

            items = items.Skip(args.Pagination.ItemsPerPageSelected * (args.Pagination.Page - 1))
                 .Take(args.Pagination.ItemsPerPageSelected)
                 .ToList();

            foreach (var model in items)
            {
                BackendGameViewModelItem item = new BackendGameViewModelItem();
                item.FromModel(model);
                result.Add(item);
            }

            return result;
        }
    }
}