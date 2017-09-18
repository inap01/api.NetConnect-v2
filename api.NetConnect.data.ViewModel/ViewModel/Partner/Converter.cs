using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.ViewModels.Profile
{
    public static class PartnerConverterExtension
    {
        public static PartnerViewModelItem ToProfileViewModelItem(this Partner model)
        {
            return new PartnerViewModelItem()
            {
                ID = model.ID,
                //FirstName = model.FirstName,
                //LastName = model.LastName,
                //Nickname = model.Nickname,
                //Email = model.Email,
                //SteamID = model.SteamID,
                //BattleTag = model.BattleTag,
                //Newsletter = model.Newsletter
            };
        }
    }

    public class PartnerConverter
    {

    }
}