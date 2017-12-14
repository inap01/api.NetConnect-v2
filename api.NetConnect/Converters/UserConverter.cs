using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using api.NetConnect.data.ViewModel.User;
using api.NetConnect.data.ViewModel.User.Backend;
using api.NetConnect.data.ViewModel;
using api.NetConnect.DataControllers;

namespace api.NetConnect.Converters
{
    public static partial class ConverterExtensions
    {
        public static UserViewModelItem FromModel(this UserViewModelItem viewModel, User model)
        {
            viewModel.ID = model.ID;
            viewModel.FirstName = model.FirstName;
            viewModel.LastName = model.LastName;
            viewModel.Nickname = model.Nickname;
            viewModel.Image = "http://lan-netconnect.de/_api/images/team/no_image.png";
            viewModel.Email = model.Email;
            viewModel.SteamID = model.SteamID;
            viewModel.BattleTag = model.BattleTag;

            return viewModel;
        }
        public static BackendUserViewModelItem FromModel(this BackendUserViewModelItem viewModel, User model)
        {
            viewModel.ID = model.ID;
            viewModel.FirstName = model.FirstName;
            viewModel.LastName = model.LastName;
            viewModel.Nickname = model.Nickname;
            viewModel.Image = "http://lan-netconnect.de/_api/images/team/no_image.png";
            viewModel.Email = model.Email;
            viewModel.SteamID = model.SteamID;
            viewModel.BattleTag = model.BattleTag;
            viewModel.Newsletter = model.Newsletter;
            viewModel.IsAdmin = model.IsAdmin;
            viewModel.IsTeam = model.IsTeam;

            return viewModel;
        }
        public static BackendUserViewModelItem FromModel(this BackendUserViewModelItem viewModel, TournamentParticipant model)
        {
            viewModel.ID = model.ID;
            viewModel.FirstName = model.User.FirstName;
            viewModel.LastName = model.User.LastName;
            viewModel.Nickname = model.User.Nickname;
            viewModel.Image = "http://lan-netconnect.de/_api/images/team/no_image.png";
            viewModel.Email = model.User.Email;
            viewModel.SteamID = model.User.SteamID;
            viewModel.BattleTag = model.User.BattleTag;
            viewModel.Newsletter = model.User.Newsletter;
            viewModel.IsAdmin = model.User.IsAdmin;
            viewModel.IsTeam = model.User.IsTeam;

            return viewModel;
        }
        public static BackendUserViewModelItem FromModel(this BackendUserViewModelItem viewModel, TournamentTeamParticipant model)
        {
            viewModel.ID = model.ID;
            viewModel.FirstName = model.User.FirstName;
            viewModel.LastName = model.User.LastName;
            viewModel.Nickname = model.User.Nickname;
            viewModel.Image = "http://lan-netconnect.de/_api/images/team/no_image.png";
            viewModel.Email = model.User.Email;
            viewModel.SteamID = model.User.SteamID;
            viewModel.BattleTag = model.User.BattleTag;
            viewModel.Newsletter = model.User.Newsletter;
            viewModel.IsAdmin = model.User.IsAdmin;
            viewModel.IsTeam = model.User.IsTeam;

            return viewModel;
        }

        public static User ToModel(this UserViewModelItem viewModel)
        {
            User model = new User();

            model.ID = viewModel.ID;
            model.FirstName = viewModel.FirstName;
            model.LastName = viewModel.LastName;
            model.Nickname = viewModel.Nickname;
            model.Email = viewModel.Email;
            model.SteamID = viewModel.SteamID;
            model.BattleTag = viewModel.BattleTag;

            return model;
        }

        public static User ToModel(this BackendUserViewModelItem viewModel)
        {
            User model = new User();

            model.ID = viewModel.ID;
            model.FirstName = viewModel.FirstName;
            model.LastName = viewModel.LastName;
            model.Nickname = viewModel.Nickname;
            model.Email = viewModel.Email;
            model.SteamID = viewModel.SteamID;
            model.BattleTag = viewModel.BattleTag;
            model.Newsletter = viewModel.Newsletter;
            model.IsAdmin = viewModel.IsAdmin;
            model.IsTeam = viewModel.IsTeam;

            return model;
        }
    }

    public class UserConverter
    {
        public static List<BackendUserViewModelItem> FilterList(ListArgsRequest<BackendProfileFilter> args, out Int32 TotalCount)
        {
            List<BackendUserViewModelItem> result = new List<BackendUserViewModelItem>();

            var users = UserDataController.GetItems();

            users = users.Where(x => x.FirstName.ToLower().Contains(args.Filter.FirstName.ToLower())).ToList();
            users = users.Where(x => x.LastName.ToLower().Contains(args.Filter.LastName.ToLower())).ToList();
            users = users.Where(x => x.Nickname.ToLower().Contains(args.Filter.Nickname.ToLower())).ToList();

            TotalCount = users.Count();

            var items = users.Skip(args.Pagination.ItemsPerPageSelected * (args.Pagination.Page - 1))
                 .Take(args.Pagination.ItemsPerPageSelected)
                 .ToList();

            foreach (var model in items)
            {
                BackendUserViewModelItem item = new BackendUserViewModelItem();
                item.FromModel(model);
                result.Add(item);
            }

            return result;
        }
    }
}