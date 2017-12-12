using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using api.NetConnect.data.ViewModel.Account;
using api.NetConnect.data.ViewModel;
using api.NetConnect.DataControllers;

namespace api.NetConnect.Converters
{
    public static partial class ConverterExtensions
    {
        public static AccountViewModelItem FromModel(this AccountViewModelItem viewModel, User model)
        {
            viewModel.ID = model.ID;
            viewModel.FirstName = model.FirstName;
            viewModel.LastName = model.LastName;
            viewModel.Nickname = model.Nickname;
            viewModel.Image = "http://lan-netconnect.de/_api/images/team/no_image.png";
            viewModel.Email = model.Email;
            viewModel.SteamID = model.SteamID;
            viewModel.BattleTag = model.BattleTag;
            viewModel.OldPassword = null;
            viewModel.NewPassword1 = null;
            viewModel.NewPassword2 = null;

            return viewModel;
        }

        public static User ToModel(this User model, AccountViewModelItem viewModel)
        {
            model.FirstName = viewModel.FirstName;
            model.LastName = viewModel.LastName;
            model.Nickname = viewModel.Nickname;
            model.Email = viewModel.Email;
            model.SteamID = viewModel.SteamID;
            model.BattleTag = viewModel.BattleTag;

            return model;
        }
    }
}