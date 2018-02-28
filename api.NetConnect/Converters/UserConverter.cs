using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using api.NetConnect.data.ViewModel.User;
using api.NetConnect.data.ViewModel.User.Backend;
using api.NetConnect.data.ViewModel;
using api.NetConnect.DataControllers;
using api.NetConnect.data.ViewModel.Auth;

namespace api.NetConnect.Converters
{
    public static partial class ConverterExtensions
    {
        public static UserViewModelItem FromModel(this UserViewModelItem viewmodel, User model)
        {
            viewmodel.ID = model.ID;
            viewmodel.FirstName = model.FirstName;
            viewmodel.LastName = model.LastName;
            viewmodel.Nickname = model.Nickname;
            viewmodel.Image = "http://lan-netconnect.de/_api/images/team/no_image.png"; // TODO
            viewmodel.Email = model.Email;
            viewmodel.SteamID = model.SteamID;
            viewmodel.BattleTag = model.BattleTag;

            return viewmodel;
        }
        public static List<BackendUserViewModelItem> FromModel(this List<BackendUserViewModelItem> viewmodel, List<User> modelList)
        {
            foreach (var model in modelList)
                viewmodel.Add(new BackendUserViewModelItem().FromModel(model));

            return viewmodel;
        }
        public static BackendUserViewModelItem FromModel(this BackendUserViewModelItem viewmodel, User model)
        {
            viewmodel.ID = model.ID;
            viewmodel.FirstName = model.FirstName;
            viewmodel.LastName = model.LastName;
            viewmodel.Nickname = model.Nickname;
            viewmodel.Image = "http://lan-netconnect.de/_api/images/team/no_image.png"; // TODO
            viewmodel.Email = model.Email;
            viewmodel.SteamID = model.SteamID;
            viewmodel.BattleTag = model.BattleTag;
            viewmodel.Newsletter = model.Newsletter;
            viewmodel.IsAdmin = model.IsAdmin;
            viewmodel.IsTeam = model.IsTeam;

            return viewmodel;
        }
        public static BackendUserViewModelItem FromModel(this BackendUserViewModelItem viewmodel, TournamentParticipant model)
        {
            viewmodel.ID = model.ID;
            viewmodel.FirstName = model.User.FirstName;
            viewmodel.LastName = model.User.LastName;
            viewmodel.Nickname = model.User.Nickname;
            viewmodel.Image = "http://lan-netconnect.de/_api/images/team/no_image.png"; // TODO
            viewmodel.Email = model.User.Email;
            viewmodel.SteamID = model.User.SteamID;
            viewmodel.BattleTag = model.User.BattleTag;
            viewmodel.Newsletter = model.User.Newsletter;
            viewmodel.IsAdmin = model.User.IsAdmin;
            viewmodel.IsTeam = model.User.IsTeam;

            return viewmodel;
        }
        public static BackendUserViewModelItem FromModel(this BackendUserViewModelItem viewmodel, TournamentTeamParticipant model)
        {
            viewmodel.ID = model.ID;
            viewmodel.FirstName = model.User.FirstName;
            viewmodel.LastName = model.User.LastName;
            viewmodel.Nickname = model.User.Nickname;
            viewmodel.Image = "http://lan-netconnect.de/_api/images/team/no_image.png"; // TODO
            viewmodel.Email = model.User.Email;
            viewmodel.SteamID = model.User.SteamID;
            viewmodel.BattleTag = model.User.BattleTag;
            viewmodel.Newsletter = model.User.Newsletter;
            viewmodel.IsAdmin = model.User.IsAdmin;
            viewmodel.IsTeam = model.User.IsTeam;

            return viewmodel;
        }

        public static User ToModel(this UserViewModelItem viewmodel)
        {
            User model = new User();

            model.ID = viewmodel.ID;
            model.FirstName = viewmodel.FirstName;
            model.LastName = viewmodel.LastName;
            model.Nickname = viewmodel.Nickname;
            model.Email = viewmodel.Email;
            model.SteamID = viewmodel.SteamID;
            model.BattleTag = viewmodel.BattleTag;

            return model;
        }

        public static User ToModel(this RegisterRequest viewmodel, String HashedPassword, String Salt)
        {
            User model = new User();

            model.FirstName = viewmodel.FirstName;
            model.LastName = viewmodel.LastName;
            model.Nickname = viewmodel.Nickname;
            model.Email = viewmodel.Email;
            model.Password = HashedPassword;
            model.PasswordSalt = Salt;
            model.Newsletter = viewmodel.Newsletter;

            model.Registered = DateTime.Now;
            model.IsActive = true;
            model.IsAdmin = false;
            model.IsTeam = false;

            return model;
        }

        public static User ToModel(this BackendUserViewModelItem viewmodel)
        {
            User model = new User();

            model.ID = viewmodel.ID;
            model.FirstName = viewmodel.FirstName;
            model.LastName = viewmodel.LastName;
            model.Nickname = viewmodel.Nickname;
            model.Email = viewmodel.Email;
            model.SteamID = viewmodel.SteamID;
            model.BattleTag = viewmodel.BattleTag;
            model.Newsletter = viewmodel.Newsletter;
            model.IsAdmin = viewmodel.IsAdmin;
            model.IsTeam = viewmodel.IsTeam;

            return model;
        }
    }
}