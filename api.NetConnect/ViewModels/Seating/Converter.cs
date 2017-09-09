using api.NetConnect.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace api.NetConnect.ViewModels.Seating
{
    public class TournamentConverter : Converter
    {
        public static List<SeatingViewModelItem> DataToViewModel()
        {
            List<SeatingViewModelItem> returnResult = new List<SeatingViewModelItem>();
            DataContext db = new DataContext(_connectionString);
            var qry = db.Seat.OrderBy(x => x.ID);

            foreach(var item in qry)
            {
                returnResult.Add(ConvertSingleItem(item));
            }

            return returnResult;
        }

        public static SeatingViewModelItem DataToViewModelDetail(Int32 id)
        {
            SeatingViewModelItem returnResult = new SeatingViewModelItem();
            DataContext db = new DataContext(_connectionString);

            Seat item = db.Seat.FirstOrDefault(x => x.ID == id);
            returnResult = ConvertSingleItem(item);

            return returnResult;
        }

        public static SeatingViewModelItem ConvertSingleItem(Seat item)
        {
            SeatingViewModelItem model = new SeatingViewModelItem();
            DataContext db = new DataContext(_connectionString);

            model.ID = item.ID;
            model.Status = item.status;
            model.Description = item.description;
            model.ReservationDate = item.date;
            model.Payed = item.payed;

            var u = db.User.FirstOrDefault(x => x.ID == item.ID);

            model.User = new SeatingUser()
            {
                ID = u.ID,
                Email = u.email,
                FirstName = u.first_name,
                LastName = u.last_name,
                Nickname = u.nickname
            };

            return model;
        }
    }
}