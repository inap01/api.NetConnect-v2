using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class SettingsDataController : GenericDataController<Settings>
    {
        public static Settings GetFirst()
        {
            return db.Settings.FirstOrDefault();
        }

        public static Settings Update (Settings item)
        {
            Settings dbItem = GetItem(item.ID);

            dbItem.Volume = item.Volume;
            dbItem.ReservationCost = item.ReservationCost;
            dbItem.Start = item.Start;
            dbItem.End = item.End;
            dbItem.IsActiveReservation = item.IsActiveReservation;
            dbItem.BankAccountCheck = item.BankAccountCheck;
            dbItem.ReservedDays = item.ReservedDays;
            dbItem.IsActiveCatering = item.IsActiveCatering;
            dbItem.IsActiveFeedback = item.IsActiveFeedback;
            dbItem.FeedbackLink = item.FeedbackLink;
            dbItem.IsActiveChat = item.IsActiveChat;
            dbItem.BankAccountOwner = item.BankAccountOwner;
            dbItem.IBAN = item.IBAN;
            dbItem.BLZ = item.BLZ;
            dbItem.BankAccountNumber = item.BankAccountNumber;
            dbItem.BIC = item.BIC;

            db.SaveChanges();

            return dbItem;
        }
    }
}