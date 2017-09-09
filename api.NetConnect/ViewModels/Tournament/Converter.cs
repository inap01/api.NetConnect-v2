using api.NetConnect.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.ViewModels.Tournament
{
    public class TournamentConverter : Converter
    {
        public static List<TournamentViewModelItem> DataToViewModel(Int32 lanID)
        {
            List<TournamentViewModelItem> returnResult = new List<TournamentViewModelItem>();
            DataContext db = new DataContext(_connectionString);
            var qry = db.Tournament.Where(x => x.ID == lanID);

            foreach(var item in qry)
            {
                returnResult.Add(ConvertSingleItem(item));
            }

            return returnResult;
        }

        public static TournamentViewModelItem DataToViewModelDetail(Int32 id)
        {
            TournamentViewModelItem returnResult = new TournamentViewModelItem();
            DataContext db = new DataContext(_connectionString);

            data.Tournament item = db.Tournament.FirstOrDefault(x => x.ID == id);
            returnResult = ConvertSingleItem(item);

            return returnResult;
        }

        public static TournamentViewModelItem ConvertSingleItem(data.Tournament item)
        {
            TournamentViewModelItem model = new TournamentViewModelItem();
            DataContext db = new DataContext(_connectionString);

            model.ID = item.ID;
            model.LanID = item.lan_id;
            model.GameID = item.game_id;
            model.TeamSize = item.team;
            model.Link = item.link;
            model.Mode = item.mode;
            model.StartTime = item.start;
            model.EndTime = item.end;
            model.IsPauseGame = item.pause_game;
            
            if(item.powered_by != 0)
            {
                var partner = db.Partner.FirstOrDefault(x => x.ID == item.powered_by);
                model.PoweredBy = partner.name;
            }

            var teams = db.Tournament_Team.Where(x => x.tournament_id == item.ID);
            foreach(var t in teams)
            {
                var player = db.Tournament_Participant.Where(x => x.team_id == t.ID);
                TeamViewModel tv = new TeamViewModel();

                foreach(var p in player)
                {
                    ParticipantViewModel pv = new ParticipantViewModel();
                    pv.ID = p.ID;
                }
            }

            return model;
        }
    }
}