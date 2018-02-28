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
        public static TournamentViewModelItem FromModel(this TournamentViewModelItem viewmodel, Tournament model)
        {
            Int32 UserID = UserHelper.CurrentUserID;

            viewmodel.ID = model.ID;
            viewmodel.GameID = model.TournamentGameID;
            viewmodel.TeamSize = model.TeamSize;
            viewmodel.ChallongeLink = model.ChallongeLink;
            viewmodel.Mode = model.Mode;
            viewmodel.Start = model.Start;
            viewmodel.End = model.End;
            viewmodel.GameTitle = model.TournamentGame.Name;
            viewmodel.Rules = model.TournamentGame.Rules;
            viewmodel.Image = Properties.Settings.Default.imageAbsolutePath + model.TournamentGame.Image;
            viewmodel.Event.FromModel(model.Event);

            viewmodel.Player = model.TournamentParticipant.ToList().ConvertAll(x => {
                if (x.UserID == UserID)
                    viewmodel.IsParticipant = true;

                var vm = new TournamentParticipantViewModelItem();
                vm.FromModel(x);
                return vm;
            });

            viewmodel.Teams = model.TournamentTeam.ToList().Where(x => x.TournamentTeamParticipant.Count > 0).ToList().ConvertAll(x => {
                if (x.TournamentTeamParticipant.FirstOrDefault(p => p.UserID == UserID) != null)
                    viewmodel.IsParticipant = true;

                var vm = new TournamentTeamViewModelItem();
                vm.FromModel(x);
                return vm;
            });

            if(model.Partner != null)
                viewmodel.Partner.FromModel(model.Partner);

            viewmodel.ParticipantCount = model.TournamentParticipant.Count;
            viewmodel.Teams.ForEach(team => viewmodel.ParticipantCount += team.Player.Count);

            return viewmodel;
        }
        public static List<BackendTournamentViewModelItem> FromModel(this List<BackendTournamentViewModelItem> viewmodel, List<Tournament> modelList)
        {
            foreach (var model in modelList)
                viewmodel.Add(new BackendTournamentViewModelItem().FromModel(model));

            return viewmodel;
        }
        public static BackendTournamentViewModelItem FromModel(this BackendTournamentViewModelItem viewmodel, Tournament model)
        {
            viewmodel.ID = model.ID;
            viewmodel.ChallongeLink = model.ChallongeLink;
            viewmodel.Mode = model.Mode;
            viewmodel.Start = model.Start;
            viewmodel.End = model.End;
            viewmodel.TeamSize = model.TeamSize;

            viewmodel.Event.FromModel(model.Event);
            viewmodel.Game.FromModel(model.TournamentGame);

            viewmodel.Player = model.TournamentParticipant.ToList().ConvertAll(x => {
                var vm = new BackendUserViewModelItem();
                vm.FromModel(x);
                return vm;
            });

            viewmodel.Teams = model.TournamentTeam.Where(x => x.TournamentTeamParticipant.Count > 0).ToList().ConvertAll(x => {
                var vm = new BackendTournamentTeamViewModelItem();
                vm.FromModel(x);
                return vm;
            });

            if (model.Partner != null)
                viewmodel.Partner.FromModel(model.Partner);

            viewmodel.ParticipantCount = model.TournamentParticipant.Count;
            viewmodel.Teams.ForEach(team => viewmodel.ParticipantCount += team.Player.Count);

            return viewmodel;
        }

        // Tournament Participant
        public static TournamentParticipantViewModelItem FromModel(this TournamentParticipantViewModelItem viewmodel, TournamentParticipant model)
        {
            viewmodel.ID = model.ID;
            viewmodel.UserID = model.UserID;
            viewmodel.FirstName = model.User.FirstName;
            viewmodel.LastName = model.User.LastName;
            viewmodel.Nickname = model.User.Nickname;

            return viewmodel;
        }
        public static TournamentParticipantViewModelItem FromModel(this TournamentParticipantViewModelItem viewmodel, TournamentTeamParticipant model)
        {
            viewmodel.ID = model.ID;
            viewmodel.UserID = model.UserID;
            viewmodel.FirstName = model.User.FirstName;
            viewmodel.LastName = model.User.LastName;
            viewmodel.Nickname = model.User.Nickname;

            return viewmodel;
        }

        // Tournament Team
        public static TournamentTeamViewModelItem FromModel(this TournamentTeamViewModelItem viewmodel, TournamentTeam model)
        {
            viewmodel.ID = model.ID;
            viewmodel.Name = model.Name;
            viewmodel.HasPassword = !String.IsNullOrEmpty(model.Password);
            viewmodel.Player = model.TournamentTeamParticipant.ToList().ConvertAll(x => {
                var vm = new TournamentParticipantViewModelItem();
                vm.FromModel(x);
                return vm;
            });

            return viewmodel;
        }
        public static BackendTournamentTeamViewModelItem FromModel(this BackendTournamentTeamViewModelItem viewmodel, TournamentTeam model)
        {
            viewmodel.ID = model.ID;
            viewmodel.Name = model.Name;
            viewmodel.HasPassword = !String.IsNullOrEmpty(model.Password);
            viewmodel.Player = model.TournamentTeamParticipant.ToList().ConvertAll(x => {
                var vm = new BackendUserViewModelItem();
                vm.FromModel(x);
                return vm;
            });

            return viewmodel;
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
        public static TournamentViewModelItem.TournamentPartner FromModel(this TournamentViewModelItem.TournamentPartner viewmodel, Partner model)
        {
            viewmodel.ID = model.ID;
            viewmodel.Name = model.Name;

            return viewmodel;
        }
        public static BackendTournamentViewModelItem.BackendTournamentPartner FromModel(this BackendTournamentViewModelItem.BackendTournamentPartner viewmodel, Partner model)
        {
            viewmodel.ID = model.ID;
            viewmodel.Name = model.Name;

            return viewmodel;
        }

        // Tournament Game
        public static List<BackendGameViewModelItem> FromModel(this List<BackendGameViewModelItem> viewmodel, List<TournamentGame> modelList)
        {
            foreach (var model in modelList)
                viewmodel.Add(new BackendGameViewModelItem().FromModel(model));

            return viewmodel;
        }

        public static BackendGameViewModelItem FromModel(this BackendGameViewModelItem viewmodel, TournamentGame model)
        {
            viewmodel.ID = model.ID;
            viewmodel.Name = model.Name;
            viewmodel.ImagePath = model.Image;
            viewmodel.RulesPath = model.Rules;
            viewmodel.RequireSteamID = model.RequireSteamID;
            viewmodel.RequireBattleTag = model.RequireBattleTag;

            return viewmodel;
        }

        public static TournamentParticipant ToModel(this JoinTournamentRequest viewmodel, Int32 TournamentID)
        {
            TournamentParticipant model = new TournamentParticipant();
            
            model.UserID = UserHelper.CurrentUserID;
            model.TournamentID = TournamentID;
            model.Registered = DateTime.Now;

            return model;
        }

        public static TournamentTeamParticipant ToTeamModel(this JoinTournamentRequest viewmodel)
        {
            TournamentTeamParticipant model = new TournamentTeamParticipant();

            model.UserID = UserHelper.CurrentUserID;
            model.TournamentTeamID = viewmodel.TeamID ?? default(int);
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

        public static TournamentGame ToModel(this BackendGameViewModelItem viewmodel)
        {
            TournamentGame model = new TournamentGame();

            model.ID = viewmodel.ID;
            model.Name = viewmodel.Name;
            model.Image = viewmodel.ImagePath;
            model.Rules = viewmodel.RulesPath;
            model.RequireBattleTag = viewmodel.RequireBattleTag;
            model.RequireSteamID = viewmodel.RequireSteamID;

            return model;
        }
    }
}