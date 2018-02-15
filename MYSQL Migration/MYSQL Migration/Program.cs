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
using System.Data.Entity.Validation;
using System.Security.Cryptography;

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
            string name = ConfigurationManager.ConnectionStrings["Entities"].ConnectionString;
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

                var i = db.User.Count();
                if (db.User.Count() <= 1) // Theken Nutzer
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
                string salt = RandomizeSalt();
                db.User.Add(
                new User()
                {
                    Email = entry["email"].ToString(),
                    Password = HashSHA256(salt + entry["password"].ToString()),
                    PasswordSalt = salt,
                    Registered = reg,
                    FirstName = entry["first_name"].ToString(),
                    LastName = entry["last_name"].ToString(),
                    Nickname = entry["nickname"].ToString(),
                    IsAdmin = Convert.ToBoolean(entry["is_admin"]),
                    BattleTag = entry["battle_tag"].ToString(),
                    SteamID = entry["steam_id"].ToString(),
                    Newsletter = Convert.ToBoolean(entry["newsletter"]),
                    IsTeam = Convert.ToBoolean(entry["is_admin"]),
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
                var atts = JsonToAttributes(entry["attributes"].ToString());

                var insertAtts = atts.Where(x => !db.CateringProductAttribute.Local.Any(y => x.Name == y.Name)).ToList();

                db.CateringProductAttribute.AddRange(insertAtts);


                var product = new CateringProduct()
                {
                    Name = entry["name"].ToString(),
                    Image = entry["image"].ToString(),
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
                var p = new Partner()
                {
                    RefLink = null,
                    ClickCount = Convert.ToInt32(entry["click_count"]),
                    Content = Convert.ToString(entry["content"]),
                    ImageOriginal = entry["image"].ToString(),
                    ImagePassive = entry["image"].ToString(),
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
                var seatnumber = Convert.ToInt32(entry["ID"].ToString());
                int state = Convert.ToInt32(entry["status"]);
                if (state == 0)
                {
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
                        if (db.User.Find(TupleToId(db, Convert.ToInt32(entry["user_id"]))).IsTeam)
                            state = 3;
                    }
                    catch(Exception ex)
                    {

                    }
                }
                seatnumber += 14;
                if (seatnumber > 74)
                    seatnumber -= 74;
                var s = new Seat()
                {
                    UserID = db.User.SingleOrDefault(x => x.Email == cond1 && x.Nickname == cond2).ID,
                    State = state,
                    Description = entry["description"].ToString(),
                    ReservationDate = GetFromMySqlDate(new MySqlDateTime(entry["date"].ToString())).Value,
                    Payed = Convert.ToInt32(entry["payed"]) > 0,
                    EventID = 10,
                    SeatNumber = seatnumber,
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
                    Registered = DateTime.Now
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
                        Amount = 1,
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
                var tg = new TournamentGame()
                {
                    RequireBattleTag = Convert.ToBoolean(entry["battletag"]),
                    Image = entry["icon"].ToString(),
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
                {
                    continue;
                }
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
                    TournamentParticipant part1 = null;
                    TournamentTeamParticipant part2 = null;

                    var tournament = db.Tournament.Find(tid);
                    if (tournament.TeamSize > 1)
                    {
                        var participant = new TournamentTeamParticipant()
                        {
                            Registered = GetFromMySqlDate(new MySqlDateTime(entry["registered"].ToString())).Value,
                            TournamentTeam = db.TournamentTeam.Find(tournamentTeamID),
                            UserID = offsetID,
                        };
                        part2 = participant;
                        if (participant.TournamentTeam == null)
                            continue;
                        db.TournamentTeamParticipant.Add(participant);
                    }
                    else
                    {
                        var participant = new TournamentParticipant()
                        {
                            Registered = GetFromMySqlDate(new MySqlDateTime(entry["registered"].ToString())).Value,
                            Tournament = db.Tournament.Find(tid),
                            UserID = offsetID,
                        };
                        part1 = participant;
                        if (participant.Tournament == null)
                            continue;
                        db.TournamentParticipant.Add(participant);
                    }
                    try {                        
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
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
        private static String HashSHA256(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        private static String RandomizeSalt(Int32 SaltLength = 10)
        {
            StringBuilder Sb = new StringBuilder();

            Random rand = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < SaltLength; i++)
            {
                Sb.Append(rand.Next(0, 9).ToString());
            }

            return Sb.ToString();
        }
    }

    
    class Progrmawe
    {
        public static DataSet userSet = new DataSet();
        public static Dictionary<int, Tuple<string, string>> idToName = new Dictionary<int, Tuple<string, string>>();
        public static Dictionary<int, int> TournamentOldToNew = new Dictionary<int, int>();

        static void Main(string[] args)
        {
            Migrator mig = new MYSQL_Migration.Migrator();

            mig.RunMigration();

            return;
        }
    }
}
