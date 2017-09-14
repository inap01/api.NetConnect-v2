using api.NetConnect.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.ViewModels.Profile
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
                Newsletter = model.Newsletter
            };
        }
    }

    public class ProfileConverter
    {

    }
}