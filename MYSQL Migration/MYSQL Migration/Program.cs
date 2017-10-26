﻿using api.NetConnect.data.Entity;
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
using System.Data.Entity.Validation;

namespace MYSQL_Migration
{

    public class Migrator
    {

        public static DataSet userSet = new DataSet();
        public static Dictionary<int, Tuple<string, string>> idToName = new Dictionary<int, Tuple<string, string>>();
        public static Dictionary<int, int> TournamentOldToNew = new Dictionary<int, int>();
        public Dictionary<int, int> oldIDTonewID = new Dictionary<int, int>();
        public void RunMigration()
        {
            string name = ConfigurationManager.ConnectionStrings["NetConnectEntities"].ConnectionString;
            DataContext db = new DataContext(name);

            string connString = @"server=localhost;Port=3306;uid=root;database=netconnect;convert zero datetime=True;";

            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                conn.Open();
                DataSet set = new DataSet();
                MySqlDataAdapter adapter = new MySqlDataAdapter();


                adapter.SelectCommand = new MySqlCommand("Select * from user", conn);
                adapter.Fill(userSet, "user");
                foreach (DataRow entry in userSet.Tables["user"].Rows)
                {
                    idToName.Add(Convert.ToInt32(entry["ID"]), new Tuple<string, string>(entry["email"].ToString(), entry["nickname"].ToString()));
                }


                if (db.User.Count() == 0)
                    RunMigrationFor<User>(() => MigrateUser(db, userSet));
                else
                    Console.WriteLine($"{DateTime.Now.ToLongTimeString()} : Skipped Migrating {typeof(User)} Already Exists(?)");


                if (db.CateringProduct.Count() == 0)
                    RunMigrationFor<CateringProduct>(() => MigrateProducts(db, conn, set, adapter));
                else
                    Console.WriteLine($"{DateTime.Now.ToLongTimeString()} : Skipped Migrating {typeof(CateringProduct)} Already Exists(?)");


                if (db.PartnerPack.Count() == 0)
                    RunMigrationFor<PartnerPack>(() => MigratePartnerPack(db, conn, set, adapter));                
                else
                    Console.WriteLine($"{DateTime.Now.ToLongTimeString()} : Skipped Migrating {typeof(PartnerPack)} Already Exists(?)");


                if (db.Partner.Count() == 0)
                    RunMigrationFor<Partner>(() => MigratePartner(db, conn, set, adapter));
                else
                    Console.WriteLine($"{DateTime.Now.ToLongTimeString()} : Skipped Migrating {typeof(Partner)} Already Exists(?)");


                if (db.Seat.Count() == 0)
                    RunMigrationFor<Seat>(() => MigrateSeating(db, conn, new DataSet(), adapter));
                else
                    Console.WriteLine($"{DateTime.Now.ToLongTimeString()} : Skipped Migrating {typeof(Seat)} Already Exists(?)");


                if (db.CateringOrder.Count() == 0)
                    RunMigrationFor<CateringOrder>(() => MigrateOrdersAndDetails(db, conn, new DataSet(), adapter));
                else
                    Console.WriteLine($"{DateTime.Now.ToLongTimeString()} : Skipped Migrating {typeof(CateringOrder)} Already Exists(?)");


                if (db.TournamentGame.Count() == 0)
                    RunMigrationFor<TournamentGame>(() => MigrateTournamentGame(db, conn, new DataSet(), adapter));
                else
                    Console.WriteLine($"{DateTime.Now.ToLongTimeString()} : Skipped Migrating {typeof(TournamentGame)} Already Exists(?)");


                if (db.Tournament.Count() == 0)
                    RunMigrationFor<Tournament>(() => MigrateTournament(db, conn, new DataSet(), adapter));
                else
                    Console.WriteLine($"{DateTime.Now.ToLongTimeString()} : Skipped Migrating {typeof(Tournament)} Already Exists(?)");


                if (db.TournamentTeam.Count() == 0)
                    RunMigrationFor<TournamentTeam>(() => MigrateTournamentTeam(db, conn, new DataSet(), adapter));
                else
                    Console.WriteLine($"{DateTime.Now.ToLongTimeString()} : Skipped Migrating {typeof(TournamentTeam)} Already Exists(?)");


                if (db.TournamentParticipant.Count() == 0)
                    RunMigrationFor<TournamentParticipant>(() => MigrateTournamentParticipant(db, conn, new DataSet(), adapter));
                else
                    Console.WriteLine($"{DateTime.Now.ToLongTimeString()} : Skipped Migrating {typeof(TournamentParticipant)} Already Exists(?)");



            }
            catch(DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            catch(Exception ex)
            {

            }
            finally { conn.Close(); }
        }

        private void RunMigrationFor<Type>(Action ExecuteMigration)
        {
            Console.WriteLine($"{DateTime.Now.ToLongTimeString()} : Start Migrating {typeof(Type)}!");            
            ExecuteMigration();
            Console.WriteLine($"{DateTime.Now.ToLongTimeString()} : Finish Migrating {typeof(Type)}!");
        }
        private void MigrateUser(DataContext db, DataSet set)
        {
            int count = 1;
            foreach (DataRow entry in set.Tables["user"].Rows)
            {
                if (String.IsNullOrEmpty(entry["password"].ToString()))
                    continue;
                DateTime reg = GetFromMySqlDate(new MySqlDateTime(entry["registered_since"].ToString())).Value;
                int? ceo;
                if (Convert.ToInt32(entry["is_vorstand"]) == 0)
                    ceo = null;
                else
                    ceo = Convert.ToInt32(entry["is_vorstand"]);
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
                    CEO = ceo,
                    IsActive = true,
                    PasswordReset = entry["password_reset"].ToString(),
                });
                oldIDTonewID.Add(Convert.ToInt32(entry["ID"]), count);
                count++;
            }

            db.SaveChanges();
        }

        private void MigrateProducts(DataContext db, MySqlConnection conn, DataSet set, MySqlDataAdapter adapter)
        {
            adapter.SelectCommand = new MySqlCommand("Select * from catering_products", conn);
            adapter.Fill(set, "catering_products");

            foreach (DataRow entry in set.Tables["catering_products"].Rows)
            {
                ImageContainer img = new ImageContainer()
                {
                    SID = Guid.NewGuid(),
                    ThumbnailPath = entry["image"].ToString(),
                    OriginalPath = entry["image"].ToString(),
                };

                var atts = JsonToAttributes(entry["attributes"].ToString());

                var insertAtts = atts.Where(x => !db.CateringProductAttribute.Local.Any(y => x.Name == y.Name)).ToList();

                db.CateringProductAttribute.AddRange(insertAtts);


                var product = new CateringProduct()
                {
                    Name = entry["name"].ToString(),
                    ImageContainer = img,
                    Price = Convert.ToDecimal(entry["price"]),
                    SingleChoice = Convert.ToBoolean(entry["single_choice"]),
                    IsActive = true,
                    
                };
                foreach (var att in atts)
                {
                    db.CateringProductAttributeRelation.Add(new CateringProductAttributeRelation()
                    {
                        CateringProduct = product,
                        CateringProductAttribute = db.CateringProductAttribute.Local.FirstOrDefault(x => x.Name == att.Name),

                    });
                }
                db.CateringProduct.Add(product);
            }
            db.SaveChanges();
        }

        public static void MigratePartnerPack(DataContext db, MySqlConnection conn, DataSet set, MySqlDataAdapter adapter)
        {
            adapter.SelectCommand = new MySqlCommand("Select * from partner_packs", conn);
            adapter.Fill(set, "partner_packs");
            foreach (DataRow row in set.Tables["partner_packs"].Rows)
            {
                var pp = new PartnerPack()
                {
                    Name = Convert.ToString(row["name"]),
                    IsActive = true,
                };
                db.PartnerPack.Add(pp);
            }

            db.SaveChanges();
        }
        public static void MigratePartner(DataContext db, MySqlConnection conn, DataSet set, MySqlDataAdapter adapter)
        {
            adapter.SelectCommand = new MySqlCommand("Select * from partner", conn);
            adapter.Fill(set, "partner");
            foreach (DataRow entry in set.Tables["partner"].Rows)
            {
                ImageContainer img = new ImageContainer()
                {
                    SID = Guid.NewGuid(),
                    ThumbnailPath = entry["image"].ToString(),
                    OriginalPath = entry["image"].ToString(),
                };                

                var p = new Partner()
                {
                    RefLink = "TmpLink not available",
                    ClickCount = Convert.ToInt32(entry["click_count"]),
                    Content = Convert.ToString(entry["content"]),
                    ImageContainer = img,
                    IsActive = Convert.ToBoolean(entry["active"]),
                    Link = Convert.ToString(entry["link"]),
                    Name = Convert.ToString(entry["name"]),
                    Position = Convert.ToInt32(entry["position"]),
                    PartnerPack = db.PartnerPack.Find(Convert.ToInt32(entry["status"])),
                };
                db.Partner.Add(p);
            }
            db.SaveChanges();
        }
        private void MigrateSeating(DataContext db, MySqlConnection conn, DataSet set, MySqlDataAdapter adapter)
        {
            adapter.SelectCommand = new MySqlCommand("select * from seating", conn);
            adapter.Fill(set, "seating");

            foreach (DataRow entry in set.Tables["seating"].Rows)
            {
                int state = Convert.ToInt32(entry["status"]);
                if (state == 0)
                {

                    Console.WriteLine("Skipping Seat : {0} status was free", Convert.ToInt32(entry["id"]));
                    continue;
                }
                int id = Convert.ToInt32(entry["user_id"]);
                var cond1 = idToName[id].Item1;
                var cond2 = idToName[id].Item2;

                if(state == 99)
                {
                    state = -1;
                }
                else
                {
                    try
                    {

                        if (db.User.Find(oldIDTonewID[id]).IsTeam)
                            state = 3;
                    }
                    catch(Exception ex)
                    {

                    }
                }
                var s = new Seat()
                {
                    UserID = db.User.SingleOrDefault(x => x.Email == cond1 && x.Nickname == cond2).ID,
                    State = state,
                    Description = entry["description"].ToString(),
                    ReservationDate = GetFromMySqlDate(new MySqlDateTime(entry["date"].ToString())).Value,
                    Payed = Convert.ToInt32(entry["payed"]) > 0,
                    EventID = 10,
                    SeatNumber = id,
                    IsActive = true,
                };
                db.Seat.Add(s);
            }
            db.SaveChanges();
        }

        private void MigrateOrdersAndDetails(DataContext db, MySqlConnection conn, DataSet set, MySqlDataAdapter adapter)
        {
            adapter.SelectCommand = new MySqlCommand("Select * from catering_orders", conn);
            adapter.Fill(set, "catering_orders");

            

            foreach (DataRow entry in set.Tables["catering_orders"].Rows)
            {
                int seatID = Convert.ToInt32(entry["seat_id"]);
                if (db.Seat.FirstOrDefault(x => x.SeatNumber == seatID) == null)
                {
                    Console.WriteLine("Skipping OrderDetail SeatID : {0} does not exist" , Convert.ToInt32(entry["seat_id"]));
                    continue;
                }
                var offsetID = TupleToId(db, Convert.ToInt32(entry["user_id"]));
                int lanID = Convert.ToInt32(entry["lan_id"]);
                var seat = db.Seat.FirstOrDefault(x => x.SeatNumber == seatID);
                var order = new CateringOrder()
                {
                    Event = (from eve in db.Event where eve.EventTypeID == 1 && eve.Volume == lanID select eve).First(),
                    UserID = offsetID,
                    Seat = seat,
                    OrderState = Convert.ToInt32(entry["complete_status"]),
                    
                };
                Detail test = null;
                string json = @entry["details"].ToString();
                var obj = JsonConvert.DeserializeObject<List<Detail>>(json);
                foreach (Detail det in obj)
                {
                    test = det;
                    var details = new CateringOrderDetail()
                    {
                        CateringOrder = order,
                        CateringProductID = det.ID.Value,
                        Attributes = JsonConvert.SerializeObject(det.attributes),                        
                        
                    };
                    db.CateringOrderDetail.Add(details);
                }
            }
            db.SaveChanges();
        }

        private void MigrateTournamentGame(DataContext db, MySqlConnection conn, DataSet set, MySqlDataAdapter adapter)
        {
            adapter.SelectCommand = new MySqlCommand("select * from tournaments_games", conn);
            adapter.Fill(set, "tournaments_games");

            foreach (DataRow entry in set.Tables["tournaments_games"].Rows)
            {
                var image = new ImageContainer()
                {
                    ThumbnailPath = entry["icon"].ToString(),
                    OriginalPath = entry["icon"].ToString(),
                    SID = Guid.NewGuid(),                    
                };

                var tg = new TournamentGame()
                {
                    RequireBattleTag = Convert.ToBoolean(entry["battletag"]),
                    ImageContainer = image,
                    Name = Convert.ToString(entry["name"]),
                    Rules = Convert.ToString(entry["rules"]),
                    RequireSteamID = Convert.ToBoolean(entry["steam"]),
                    IsActive = true,                    
                };
                db.TournamentGame.Add(tg);
            }

            db.SaveChanges();
        }

        private void MigrateTournament(DataContext db, MySqlConnection conn, DataSet set, MySqlDataAdapter adapter)
        {

            adapter.SelectCommand = new MySqlCommand("select * from tournaments", conn);
            adapter.Fill(set, "tournaments");

            foreach (DataRow entry in set.Tables["tournaments"].Rows)
            {
                DateTime start = GetFromMySqlDate(new MySqlDateTime(entry["start"].ToString())).Value;
                DateTime? end = GetFromMySqlDate(new MySqlDateTime(entry["end"].ToString()), true);

                var lanID = Convert.ToInt32(entry["lan_id"]);
                var gameID = Convert.ToInt32(entry["game_id"]);

                var eve = db.Event.FirstOrDefault(x => x.Volume == lanID && x.EventTypeID == 1);
                var game = db.TournamentGame.FirstOrDefault(x => x.ID == gameID);

                db.Tournament.Add(
                    new Tournament()
                    {
                        TournamentGame = game,
                        Event = eve,
                        Start = start,
                        End = end,
                        Partner = null,
                        TeamSize = Convert.ToInt32(entry["team"]),
                        ChallongeLink = entry["link"].ToString(),
                        Mode = entry["mode"].ToString(),
                    });
            }
            db.SaveChanges();
        }

        private void MigrateTournamentTeam(DataContext db, MySqlConnection conn, DataSet set, MySqlDataAdapter adapter)
        {

            adapter.SelectCommand = new MySqlCommand("select * from tournaments_teams", conn);
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

            db.SaveChanges();
        }

        private void MigrateTournamentParticipant(DataContext db, MySqlConnection conn, DataSet set, MySqlDataAdapter adapter)
        {

            adapter.SelectCommand = new MySqlCommand("select * from tournaments_participants", conn);
            adapter.Fill(set, "tournaments_participants");
            try
            {
                foreach (DataRow entry in set.Tables["tournaments_participants"].Rows)
                {
                    if (Convert.ToInt32(entry["user_id"]) == 0 || new int[] { 12, 14, 20 }.Contains(Convert.ToInt32(entry["tournament_id"])))
                        continue;
                    int tournamentTeamID = -1;
                    int offsetID = 0;
                    try { offsetID = TupleToId(db, Convert.ToInt32(entry["user_id"])); }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Skipping Participant : {0}", Convert.ToInt32(entry["user_id"]));
                        continue;
                    }
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


                    var tournament = db.Tournament.Find(tid);
                    if (tournament.TeamSize > 1)
                    {
                        var participant = new TournamentTeamParticipant()
                        {
                            Registered = GetFromMySqlDate(new MySqlDateTime(entry["registered"].ToString())).Value,
                            TournamentTeamID = tournamentTeamID,
                            UserID = offsetID,
                        };
                        db.TournamentTeamParticipant.Add(participant);
                    }
                    else
                    {
                        var participant = new TournamentParticipant()
                        {
                            Registered = GetFromMySqlDate(new MySqlDateTime(entry["registered"].ToString())).Value,
                            TournamentID = tid,
                            UserID = offsetID,
                        };
                        db.TournamentParticipant.Add(participant);
                    }
                }
            }
            catch(Exception ex)
            {

            }

        }

        private List<CateringProductAttribute> JsonToAttributes(string json)
        {
            List<CateringProductAttribute> attributes = new List<CateringProductAttribute>();
            List<string> values = JsonConvert.DeserializeObject<List<string>>(json);
            foreach (string val in values)
                attributes.Add(new CateringProductAttribute()
                {
                    IsActive = true,
                    Name = val,
                });
            return attributes;
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
        public class Detail
        {
            public Int32? ID { get; set; }
            public Int32? count { get; set; }
            public string[] attributes { get; set; }
        }
        private static Int32 TupleToId(DataContext db, Int32 param)
        {
            try
            {
                var tup = idToName[param];
                var cond1 = tup.Item1;
                var cond2 = tup.Item2;
                return db.User.SingleOrDefault(x => x.Email == cond1 && x.Nickname == cond2).ID;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    
    class Progrmawe
    {
        public static DataSet userSet = new DataSet();
        public static Dictionary<int, Tuple<string, string>> idToName = new Dictionary<int, Tuple<string, string>>();
        public static Dictionary<int, int> TournamentOldToNew = new Dictionary<int, int>();
        //static DataContext db = new DataContext(ConfigurationManager.AppSettings["NetConnectEntities"]);
        static void Main(string[] args)
        {
            Migrator mig = new MYSQL_Migration.Migrator();

            mig.RunMigration();

            return;
        }
        //    string name = ConfigurationManager.ConnectionStrings["NetConnectEntities"].ConnectionString;
        //    DataContext db = new DataContext(name);

        //    string connString = @"server=https://pub01.schlenter-simon.de;uid=netconnect-db01;pwd=7uneqa9eb;database=lannetconnect_db01;convert zero datetime=True;";

        //    MySqlConnection conn = new MySqlConnection(connString);
        //    conn.Open();
        //    try
        //    {
        //        DataSet set = new DataSet();
        //        MySqlDataAdapter adapter = new MySqlDataAdapter();


        //        adapter.SelectCommand = new MySqlCommand("Select * from user", conn);
        //        adapter.Fill(userSet, "user");
        //        foreach (DataRow entry in userSet.Tables["user"].Rows)
        //        {
        //            idToName.Add(Convert.ToInt32(entry["ID"]), new Tuple<string, string>(entry["email"].ToString(), entry["nickname"].ToString()));
        //        }


        //        FinishedCalls(db, conn, set, adapter);

        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //}

        //private static void MigrateTournamentParticipants(DataContext db, MySqlConnection conn, DataSet set, MySqlDataAdapter adapter)
        //{
        //    adapter.SelectCommand = new MySqlCommand("Select * from tournaments_participants", conn);
        //    adapter.Fill(set, "tournaments_participants");
        //    foreach (DataRow entry in set.Tables["tournaments_participants"].Rows)
        //    {
        //        if (Convert.ToInt32(entry["user_id"]) == 0 || new int[] { 12, 14, 20 }.Contains(Convert.ToInt32(entry["tournament_id"])))
        //            continue;
        //        int? tournamentTeamID = null;
        //        var offsetID = TupleToId(db, Convert.ToInt32(entry["user_id"]));
        //        if (Convert.ToInt32(entry["tournament_id"]) == 0)
        //            continue;
        //        int tid = Convert.ToInt32(entry["tournament_id"]) - 7;
        //        if (Convert.ToInt32(entry["tournament_id"]) > 20)
        //            tid -= 4;
        //        else
        //        if (Convert.ToInt32(entry["tournament_id"]) > 16)
        //            tid -= 3;
        //        else
        //        if (Convert.ToInt32(entry["tournament_id"]) > 11)
        //            tid -= 1;

        //        if (Convert.ToInt32(entry["team_id"]) != 0)
        //        {
        //            MySqlDataAdapter internalAdaper = new MySqlDataAdapter();
        //            DataSet internalSet = new DataSet();
        //            internalAdaper.SelectCommand = new MySqlCommand($"Select ID from tournaments_teams where ID={Convert.ToInt32(entry["team_id"])}", conn);
        //            internalAdaper.Fill(internalSet, "tournaments_teams");

        //            var ID = Convert.ToInt32(internalSet.Tables["tournaments_teams"].Rows[0]["ID"]);
        //            tournamentTeamID = TournamentOldToNew[ID];
        //        }

        //        var participant = new TournamentParticipant()
        //        {
        //            TournamentID = tid,
        //            Registered = GetFromMySqlDate(new MySqlDateTime(entry["registered"].ToString())).Value,
        //            TournamentTeamID = tournamentTeamID,
        //            UserID = offsetID,
        //        };
        //        db.TournamentParticipant.Add(participant);
        //    }
        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private static Int32 TupleToId(DataContext db, Int32 param)
        //{
        //    var tup = idToName[param];
        //    var cond1 = tup.Item1;
        //    var cond2 = tup.Item2;
        //    return db.User.SingleOrDefault(x => x.Email == cond1 && x.Nickname == cond2).ID;
        //}
        

        //private static void FinishedCalls(DataContext db, MySqlConnection conn, DataSet set, MySqlDataAdapter adapter)
        //{

        //    //Phase 1
            
        //    MigrateUser(db, userSet);
        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //    adapter.SelectCommand = new MySqlCommand("Select * from tournaments_games", conn);
        //    set = new DataSet();
        //    adapter.Fill(set, "tournaments_games");
        //    MigrateTournamentGames(db, set);

        //    db.SaveChanges();

        //    adapter.SelectCommand = new MySqlCommand("Select * from partner_packs", conn);
        //    set = new DataSet();
        //    adapter.Fill(set, "partner_packs");
        //    MigratePartnerPack(db, set);

        //    db.SaveChanges();

        //    adapter.SelectCommand = new MySqlCommand("Select * from partner", conn);
        //    set = new DataSet();
        //    adapter.Fill(set, "partner");
        //    MigratePartner(db, set);

        //    db.SaveChanges();

        //    MigrateProducts(db, conn, set, adapter);

        //    // Phase 2

        //    set = new DataSet();
        //    adapter.SelectCommand = new MySqlCommand("Select * from tournaments", conn);
        //    adapter.Fill(set, "tournaments");
        //    MigrateTournament(db, set);

        //    db.SaveChanges();


        //    set = new DataSet();
        //    DataSet set2 = new DataSet();

        //    adapter.SelectCommand = new MySqlCommand("Select * from seating", conn);
        //    adapter.Fill(set, "seating");

        //    MigrateSeating(db, set, userSet);

        //    db.SaveChanges();

        //    MigrateOrdersAndDetails(db, conn, set, adapter);

        //    MigrateTournamentTeam(db, conn, set, adapter);

        //    MigrateTournamentParticipants(db, conn, set, adapter);
        //}

        //public static DateTime? GetFromMySqlDate(MySqlDateTime date, bool IsNullable = false)
        //{
            
        //    if (date.Day == 0001 && date.Month == 1 && IsNullable)
        //        return null;
        //    if (date.Day < SqlDateTime.MinValue.Value.Year || (date.Day == 0000 && date.Month == 0) || (date.Day == 0001 && date.Month == 1))
        //        return (DateTime)SqlDateTime.MinValue;
        //    DateTime retDate = new DateTime(date.Day, date.Month, date.Year, date.Hour, date.Minute, date.Second, date.Millisecond);
        //    return retDate;
        //}
        //public class Detail
        //{
        //    public Int32? ID { get; set; }
        //    public Int32? count { get; set; }
        //    public string[] attributes { get; set; }
        //}
        //private static void MigrateUser(DataContext db, DataSet set)
        //{
        //    foreach (DataRow entry in set.Tables["user"].Rows)
        //    {
        //        if (String.IsNullOrEmpty(entry["password"].ToString()))
        //            continue;
        //        DateTime reg = GetFromMySqlDate(new MySqlDateTime(entry["registered_since"].ToString())).Value;
        //        db.User.Add(
        //        new User()
        //        {
        //            Email = entry["email"].ToString(),
        //            Password = entry["password"].ToString(),
        //            Registered = reg,
        //            FirstName = entry["first_name"].ToString(),
        //            LastName = entry["last_name"].ToString(),
        //            Nickname = entry["nickname"].ToString(),
        //            IsAdmin = Convert.ToBoolean(entry["is_admin"]),
        //            BattleTag = entry["battle_tag"].ToString(),
        //            SteamID = entry["steam_id"].ToString(),
        //            Newsletter = Convert.ToBoolean(entry["newsletter"]),
        //            Image = entry["image"].ToString(),
        //            IsTeam = false,
        //            CEO = Convert.ToInt32(entry["is_vorstand"]),
        //            IsActive = true,
        //            PasswordReset = entry["password_reset"].ToString(),
        //        });
        //    }
        //}
        //public static void MigrateTournamentGames(DataContext db, DataSet set)
        //{
        //    foreach (DataRow row in set.Tables["tournaments_games"].Rows)
        //    {
        //        var tg = new TournamentGame()
        //        {
        //            BattleTag = Convert.ToBoolean(row["battletag"]),
        //            Icon = Convert.ToString(row["icon"]),
        //            Name = Convert.ToString(row["name"]),
        //            Rules = Convert.ToString(row["rules"]),
        //            SteamID = Convert.ToBoolean(row["steam"])
        //        };
        //        db.TournamentGame.Add(tg);
        //    }
        //}
        //public static void MigratePartnerPack(DataContext db, DataSet set)
        //{
        //    foreach (DataRow row in set.Tables["partner_packs"].Rows)
        //    {
        //        var pp = new PartnerPack()
        //        {
        //            Name = Convert.ToString(row["name"])
        //        };
        //        db.PartnerPack.Add(pp);
        //    }
        //}
        //public static void MigratePartner(DataContext db, DataSet set)
        //{
        //    foreach (DataRow row in set.Tables["partner"].Rows)
        //    {
        //        int id = Convert.ToInt32(row["status"]);
        //        Console.WriteLine(id);
        //        var p = new Partner()
        //        {
                    
        //            ClickCount = Convert.ToInt32(row["click_count"]),
        //            Content = Convert.ToString(row["content"]),
        //            Image = Convert.ToString(row["image"]),
        //            ImageAlt = Convert.ToString(row["image_alt"]),
        //            IsActive = Convert.ToBoolean(row["active"]),
        //            Link = Convert.ToString(row["link"]),
        //            Name = Convert.ToString(row["name"]),
        //            Position = Convert.ToInt32(row["position"]),
        //            PartnerPackID = db.PartnerPack.Where(x=> x.ID == id).Select(x=> x.ID).First()
        //        };
        //        db.Partner.Add(p);
        //    }
        //}
        //private static void MigrateTournament(DataContext db, DataSet set)
        //{
        //    foreach (DataRow entry in set.Tables["tournaments"].Rows)
        //    {
        //        DateTime start = GetFromMySqlDate(new MySqlDateTime(entry["start"].ToString())).Value;
        //        DateTime? end = GetFromMySqlDate(new MySqlDateTime(entry["end"].ToString()), true);

        //        int? partnerID;
        //        if (Convert.ToInt32(entry["powered_by"]) == 0)
        //            partnerID = null;

        //        else
        //            partnerID = Convert.ToInt32(entry["powered_by"]);
        //        db.Tournament.Add(
        //        new Tournament()
        //        {
        //            Volume = Convert.ToInt32(entry["lan_id"]),
        //            TournamentGameID = Convert.ToInt32(entry["game_id"]),
        //            Start = start,
        //            End = end,
        //            TeamSize = Convert.ToInt32(entry["team"]),
        //            ChallongeLink = entry["link"].ToString(),
        //            PartnerID = partnerID,
        //            IsPauseGame = Convert.ToBoolean(entry["pause_game"]),
        //            Mode = entry["mode"].ToString(),
        //        });
        //    }
        //}
        //private static void MigrateSeating(DataContext db, DataSet set, DataSet set2)
        //{           
        //    foreach (DataRow entry in set.Tables["seating"].Rows)
        //    {
        //        int id = Convert.ToInt32(entry["user_id"]);
        //        var cond1 = idToName[id].Item1;
        //        var cond2 = idToName[id].Item2;
        //        var s = new Seat()
        //        {
        //            UserID = db.User.SingleOrDefault(x => x.Email == cond1 && x.Nickname == cond2).ID,
        //            State = Convert.ToInt32(entry["status"]),
        //            Description = entry["description"].ToString(),
        //            ReservationDate = GetFromMySqlDate(new MySqlDateTime(entry["date"].ToString())).Value,
        //            Payed = Convert.ToInt32(entry["payed"]) > 0,
        //            IsTeam = Convert.ToInt32(entry["payed"]) >= 2,
        //        };
        //        db.Seat.Add(s);
        //    }
        //}
        //private static void MigrateProducts(DataContext db, MySqlConnection conn, DataSet set, MySqlDataAdapter adapter)
        //{
        //    adapter.SelectCommand = new MySqlCommand("Select * from catering_products", conn);
        //    adapter.Fill(set, "catering_products");

        //    foreach (DataRow entry in set.Tables["catering_products"].Rows)
        //    {
        //        var product = new CateringProduct()
        //        {
        //            Name = entry["name"].ToString(),
        //            Description = entry["description"].ToString(),
        //            Image = entry["image"].ToString(),
        //            Price = Convert.ToDecimal(entry["price"]),
        //            Attributes = entry["attributes"].ToString(),
        //            SingleChoice = Convert.ToBoolean(entry["single_choice"]),
        //        };
        //        db.CateringProduct.Add(product);
        //    }
        //    db.SaveChanges();
        //}
        //private static void MigrateOrdersAndDetails(DataContext db, MySqlConnection conn, DataSet set, MySqlDataAdapter adapter)
        //{
        //    adapter.SelectCommand = new MySqlCommand("Select * from catering_orders", conn);
        //    adapter.Fill(set, "catering_orders");


        //    foreach (DataRow entry in set.Tables["catering_orders"].Rows)
        //    {
        //        var offsetID = TupleToId(db, Convert.ToInt32(entry["user_id"]));
        //        var order = new CateringOrder()
        //        {
        //            Volume = Convert.ToInt32(entry["lan_id"]),
        //            UserID = offsetID,
        //            SeatID = Convert.ToInt32(entry["seat_id"]),
        //            CompletionState = Convert.ToInt32(entry["complete_status"]),
        //        };
        //        Detail test = null;
        //        var _order = db.CateringOrder.Add(order);
        //        db.SaveChanges();
        //        string json = @entry["details"].ToString();
        //        var obj = JsonConvert.DeserializeObject<List<Detail>>(json);
        //        foreach (Detail det in obj)
        //        {
        //            test = det;
        //            var details = new CateringOrderDetail()
        //            {
        //                CateringOrderID = _order.ID,
        //                CateringProductID = det.ID.Value,
        //                Attributes = JsonConvert.SerializeObject(det.attributes),
        //            };
        //            db.CateringOrderDetail.Add(details);
        //        }
        //    }
        //    db.SaveChanges();
        //}
        //private static void MigrateTournamentTeam(DataContext db, MySqlConnection conn, DataSet set, MySqlDataAdapter adapter)
        //{
        //    adapter.SelectCommand = new MySqlCommand("Select * from tournaments_teams", conn);
        //    adapter.Fill(set, "tournaments_teams");
        //    foreach (DataRow entry in set.Tables["tournaments_teams"].Rows)
        //    {
        //        if (Convert.ToInt32(entry["tournament_id"]) == 0)
        //            continue;
        //        int tid = Convert.ToInt32(entry["tournament_id"]) - 7;
        //        if (Convert.ToInt32(entry["tournament_id"]) > 20)
        //            tid -= 4;
        //        else
        //        if (Convert.ToInt32(entry["tournament_id"]) > 16)
        //            tid -= 3;
        //        else
        //        if (Convert.ToInt32(entry["tournament_id"]) > 11)
        //            tid -= 1;
        //        var team = new TournamentTeam()
        //        {
        //            Name = entry["name"].ToString(),
        //            TournamentID = tid,
        //            Password = entry["password"].ToString(),
        //        };
        //        db.TournamentTeam.Add(team);
        //        db.SaveChanges();
        //        TournamentOldToNew.Add(Convert.ToInt32(entry["ID"]), team.ID);
        //    }
        //}

    }
}
