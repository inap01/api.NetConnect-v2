using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using Newtonsoft.Json;
using System.Data.SqlTypes;
using System.Data.Entity;

namespace MYSQL_Migration
{
    
    class Progrmawe
    {
        public static DataSet userSet = new DataSet();
        public static Dictionary<int, Tuple<string, string>> idToName = new Dictionary<int, Tuple<string, string>>();
        public static Dictionary<int, int> TournamentOldToNew = new Dictionary<int, int>();
        //static DataContext db = new DataContext(ConfigurationManager.AppSettings["NetConnectEntities"]);
        static void Main(string[] args)
        {
            string name = ConfigurationManager.ConnectionStrings["NetConnectEntities"].ConnectionString;
            DataContext db = new DataContext(name);

            string connString = @"server=lan-netconnect.de;uid=netconnect;pwd=L77bk12?;database=netconnect;convert zero datetime=True;";

            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
            try
            {
                DataSet set = new DataSet();
                MySqlDataAdapter adapter = new MySqlDataAdapter();


                adapter.SelectCommand = new MySqlCommand("Select * from user", conn);
                adapter.Fill(userSet, "user");
                foreach (DataRow entry in userSet.Tables["user"].Rows)
                {
                    idToName.Add(Convert.ToInt32(entry["ID"]), new Tuple<string, string>(entry["email"].ToString(), entry["nickname"].ToString()));
                }


                FinishedCalls(db, conn, set, adapter);
                #region TournamentTeam
                

                #endregion

            }
            finally{ conn.Close(); }
        }

        private static void MigrateTournamentParticipants(DataContext db, MySqlConnection conn, DataSet set, MySqlDataAdapter adapter)
        {
            adapter.SelectCommand = new MySqlCommand("Select * from tournaments_participants", conn);
            adapter.Fill(set, "tournaments_participants");
            foreach (DataRow entry in set.Tables["tournaments_participants"].Rows)
            {
                if (Convert.ToInt32(entry["user_id"]) == 0 || new int[] { 12, 14, 20 }.Contains(Convert.ToInt32(entry["tournament_id"])))
                    continue;
                int? tournamentTeamID = null;
                var offsetID = TupleToId(db, Convert.ToInt32(entry["user_id"]));
                if (Convert.ToInt32(entry["tournament_id"]) == 0)
                    continue;
                int tid = Convert.ToInt32(entry["tournament_id"]) - 7;
                if (Convert.ToInt32(entry["tournament_id"]) > 20)
                    tid -= 4;
                else
                if (Convert.ToInt32(entry["tournament_id"]) > 16)
                    tid -= 3;
                else
                if (Convert.ToInt32(entry["tournament_id"]) > 11)
                    tid -= 1;

                if (Convert.ToInt32(entry["team_id"]) != 0)
                {
                    MySqlDataAdapter internalAdaper = new MySqlDataAdapter();
                    DataSet internalSet = new DataSet();
                    internalAdaper.SelectCommand = new MySqlCommand($"Select ID from tournaments_teams where ID={Convert.ToInt32(entry["team_id"])}", conn);
                    internalAdaper.Fill(internalSet, "tournaments_teams");

                    var ID = Convert.ToInt32(internalSet.Tables["tournaments_teams"].Rows[0]["ID"]);
                    tournamentTeamID = TournamentOldToNew[ID];
                }

                var participant = new TournamentParticipant()
                {
                    TournamentID = tid,
                    Registered = GetFromMySqlDate(new MySqlDateTime(entry["registered"].ToString())).Value,
                    TournamentTeamID = tournamentTeamID,
                    UserID = offsetID,
                };
                db.TournamentParticipant.Add(participant);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        private static Int32 TupleToId(DataContext db, Int32 param)
        {
            var tup = idToName[param];
            var cond1 = tup.Item1;
            var cond2 = tup.Item2;
            return db.User.SingleOrDefault(x => x.Email == cond1 && x.Nickname == cond2).ID;
        }
        

        private static void FinishedCalls(DataContext db, MySqlConnection conn, DataSet set, MySqlDataAdapter adapter)
        {

            //Phase 1
            
            MigrateUser(db, userSet);

            db.SaveChanges();

            adapter.SelectCommand = new MySqlCommand("Select * from tournaments_games", conn);
            set = new DataSet();
            adapter.Fill(set, "tournaments_games");
            MigrateTournamentGames(db, set);

            db.SaveChanges();

            adapter.SelectCommand = new MySqlCommand("Select * from partner_packs", conn);
            set = new DataSet();
            adapter.Fill(set, "partner_packs");
            MigratePartnerPack(db, set);

            db.SaveChanges();

            adapter.SelectCommand = new MySqlCommand("Select * from partner", conn);
            set = new DataSet();
            adapter.Fill(set, "partner");
            MigratePartner(db, set);

            db.SaveChanges();

            MigrateProducts(db, conn, set, adapter);

            // Phase 2

            set = new DataSet();
            adapter.SelectCommand = new MySqlCommand("Select * from tournaments", conn);
            adapter.Fill(set, "tournaments");
            MigrateTournament(db, set);

            db.SaveChanges();


            set = new DataSet();
            DataSet set2 = new DataSet();

            adapter.SelectCommand = new MySqlCommand("Select * from seating", conn);
            adapter.Fill(set, "seating");

            MigrateSeating(db, set, userSet);

            db.SaveChanges();

            MigrateOrdersAndDetails(db, conn, set, adapter);

            MigrateTournamentTeam(db, conn, set, adapter);

            MigrateTournamentParticipants(db, conn, set, adapter);
        }

        public static DateTime? GetFromMySqlDate(MySqlDateTime date, bool IsNullable = false)
        {
            
            if (date.Day == 0001 && date.Month == 1 && IsNullable)
                return null;
            if (date.Day < SqlDateTime.MinValue.Value.Year || (date.Day == 0000 && date.Month == 0) || (date.Day == 0001 && date.Month == 1))
                return (DateTime)SqlDateTime.MinValue;
            DateTime retDate = new DateTime(date.Day, date.Month, date.Year, date.Hour, date.Minute, date.Second, date.Millisecond);
            return retDate;
        }
        public class CateringDetails
        {
            public Detail[] details = null;
            
        }
        public class Detail
        {
            public Int32? ID { get; set; }
            public Int32? count { get; set; }
            public string[] attributes { get; set; }
        }
        private static void MigrateUser(DataContext db, DataSet set)
        {
            foreach (DataRow entry in set.Tables["user"].Rows)
            {
                if (String.IsNullOrEmpty(entry["password"].ToString()))
                    continue;
                DateTime reg = GetFromMySqlDate(new MySqlDateTime(entry["registered_since"].ToString())).Value;
                db.User.Add(
                new User()
                {
                    Email = entry["email"].ToString(),
                    Password = entry["password"].ToString(),
                    Registered = reg,
                    FirstName = entry["first_name"].ToString(),
                    LastName = entry["last_name"].ToString(),
                    Nickname = entry["nickname"].ToString(),
                    IsAdmin = Convert.ToBoolean(entry["is_admin"]),
                    BattleTag = entry["battle_tag"].ToString(),
                    SteamID = entry["steam_id"].ToString(),
                    Newsletter = Convert.ToBoolean(entry["newsletter"]),
                    Image = entry["image"].ToString(),
                    IsTeam = false,
                    IsCEO = Convert.ToBoolean(entry["is_vorstand"])
                });
            }
        }
        public static void MigrateTournamentGames(DataContext db, DataSet set)
        {
            foreach (DataRow row in set.Tables["tournaments_games"].Rows)
            {
                var tg = new TournamentGame()
                {
                    BattleTag = Convert.ToBoolean(row["battletag"]),
                    Icon = Convert.ToString(row["icon"]),
                    Name = Convert.ToString(row["name"]),
                    Rules = Convert.ToString(row["rules"]),
                    SteamID = Convert.ToBoolean(row["steam"])
                };
                db.TournamentGame.Add(tg);
            }
        }
        public static void MigratePartnerPack(DataContext db, DataSet set)
        {
            foreach (DataRow row in set.Tables["partner_packs"].Rows)
            {
                var pp = new PartnerPack()
                {
                    Name = Convert.ToString(row["name"])
                };
                db.PartnerPack.Add(pp);
            }
        }
        public static void MigratePartner(DataContext db, DataSet set)
        {
            foreach (DataRow row in set.Tables["partner"].Rows)
            {
                int id = Convert.ToInt32(row["status"]);
                Console.WriteLine(id);
                var p = new Partner()
                {
                    ClickCount = Convert.ToInt32(row["click_count"]),
                    Content = Convert.ToString(row["content"]),
                    Image = Convert.ToString(row["image"]),
                    ImageAlt = Convert.ToString(row["image_alt"]),
                    IsActive = Convert.ToBoolean(row["active"]),
                    Link = Convert.ToString(row["link"]),
                    Name = Convert.ToString(row["name"]),
                    Position = Convert.ToInt32(row["position"]),
                    PartnerPackID = db.PartnerPack.Where(x=> x.ID == id).Select(x=> x.ID).First()
                };
                db.Partner.Add(p);
            }
        }
        private static void MigrateTournament(DataContext db, DataSet set)
        {
            foreach (DataRow entry in set.Tables["tournaments"].Rows)
            {
                DateTime start = GetFromMySqlDate(new MySqlDateTime(entry["start"].ToString())).Value;
                DateTime? end = GetFromMySqlDate(new MySqlDateTime(entry["end"].ToString()), true);

                int? partnerID;
                if (Convert.ToInt32(entry["powered_by"]) == 0)
                    partnerID = null;

                else
                    partnerID = Convert.ToInt32(entry["powered_by"]);
                db.Tournament.Add(
                new Tournament()
                {
                    Volume = Convert.ToInt32(entry["lan_id"]),
                    TournamentGameID = Convert.ToInt32(entry["game_id"]),
                    Start = start,
                    End = end,
                    TeamSize = Convert.ToInt32(entry["team"]),
                    ChallongeLink = entry["link"].ToString(),
                    PartnerID = partnerID,
                    IsPauseGame = Convert.ToBoolean(entry["pause_game"]),
                    Mode = entry["mode"].ToString(),
                });
            }
        }
        private static void MigrateSeating(DataContext db, DataSet set, DataSet set2)
        {           
            foreach (DataRow entry in set.Tables["seating"].Rows)
            {
                int id = Convert.ToInt32(entry["user_id"]);
                var cond1 = idToName[id].Item1;
                var cond2 = idToName[id].Item2;
                var s = new Seat()
                {
                    UserID = db.User.SingleOrDefault(x => x.Email == cond1 && x.Nickname == cond2).ID,
                    State = Convert.ToInt32(entry["status"]),
                    Description = entry["description"].ToString(),
                    ReservationDate = GetFromMySqlDate(new MySqlDateTime(entry["date"].ToString())).Value,
                    Payed = Convert.ToInt32(entry["payed"]) > 0,
                    IsTeam = Convert.ToInt32(entry["payed"]) >= 2,
                };
                db.Seat.Add(s);
            }
        }
        private static void MigrateProducts(DataContext db, MySqlConnection conn, DataSet set, MySqlDataAdapter adapter)
        {
            adapter.SelectCommand = new MySqlCommand("Select * from catering_products", conn);
            adapter.Fill(set, "catering_products");

            foreach (DataRow entry in set.Tables["catering_products"].Rows)
            {
                var product = new CateringProduct()
                {
                    Name = entry["name"].ToString(),
                    Description = entry["description"].ToString(),
                    Image = entry["image"].ToString(),
                    Price = Convert.ToDecimal(entry["price"]),
                    Attributes = entry["attributes"].ToString(),
                    SingleChoice = Convert.ToBoolean(entry["single_choice"]),
                };
                db.CateringProduct.Add(product);
            }
            db.SaveChanges();
        }
        private static void MigrateOrdersAndDetails(DataContext db, MySqlConnection conn, DataSet set, MySqlDataAdapter adapter)
        {
            adapter.SelectCommand = new MySqlCommand("Select * from catering_orders", conn);
            adapter.Fill(set, "catering_orders");


            foreach (DataRow entry in set.Tables["catering_orders"].Rows)
            {
                var offsetID = TupleToId(db, Convert.ToInt32(entry["user_id"]));
                var order = new CateringOrder()
                {
                    Volume = Convert.ToInt32(entry["lan_id"]),
                    UserID = offsetID,
                    SeatID = Convert.ToInt32(entry["seat_id"]),
                    CompletionState = Convert.ToInt32(entry["complete_status"]),
                };
                Detail test = null;
                var _order = db.CateringOrder.Add(order);
                db.SaveChanges();
                string json = @entry["details"].ToString();
                var obj = JsonConvert.DeserializeObject<List<Detail>>(json);
                foreach (Detail det in obj)
                {
                    test = det;
                    var details = new CateringOrderDetail()
                    {
                        CateringOrderID = _order.ID,
                        CateringProductID = det.ID.Value,
                        Attributes = JsonConvert.SerializeObject(det.attributes),
                    };
                    db.CateringOrderDetail.Add(details);
                }
            }
            db.SaveChanges();
        }
        private static void MigrateTournamentTeam(DataContext db, MySqlConnection conn, DataSet set, MySqlDataAdapter adapter)
        {
            adapter.SelectCommand = new MySqlCommand("Select * from tournaments_teams", conn);
            adapter.Fill(set, "tournaments_teams");
            foreach (DataRow entry in set.Tables["tournaments_teams"].Rows)
            {
                if (Convert.ToInt32(entry["tournament_id"]) == 0)
                    continue;
                int tid = Convert.ToInt32(entry["tournament_id"]) - 7;
                if (Convert.ToInt32(entry["tournament_id"]) > 20)
                    tid -= 4;
                else
                if (Convert.ToInt32(entry["tournament_id"]) > 16)
                    tid -= 3;
                else
                if (Convert.ToInt32(entry["tournament_id"]) > 11)
                    tid -= 1;
                var team = new TournamentTeam()
                {
                    Name = entry["name"].ToString(),
                    TournamentID = tid,
                    Password = entry["password"].ToString(),
                };
                db.TournamentTeam.Add(team);
                db.SaveChanges();
                TournamentOldToNew.Add(Convert.ToInt32(entry["ID"]), team.ID);
            }
        }

    }
}
