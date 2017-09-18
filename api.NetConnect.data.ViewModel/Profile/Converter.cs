using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Profile
{
    public static class ProfileConverterExtensions
    {
        public static ProfileViewModelItem ToProfileViewModelItem(this User model)
        {
            return new ProfileViewModelItem()
            {
                ID = model.ID,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Nickname = model.Nickname,
                Email = model.Email,
                SteamID = model.SteamID,
                BattleTag = model.BattleTag,
                Newsletter = model.Newsletter,
                Password1 = null,
                Password2 = null
            };
        }
    }

    public class ProfileConverter
    {

    }
}