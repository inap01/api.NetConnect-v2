using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data
{
    public class DataContext : DbContext
    {
        public DbSet<Order> Order { get; set; }
        public DbSet<Partner> Partner { get; set; }
        public DbSet<Partner_Pack> Partner_Pack { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Seat> Seat { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Tournament> Tournament { get; set; }
        public DbSet<Tournament_Game> Tournament_Game { get; set; }
        public DbSet<Tournament_Participant> Tournament_Participant { get; set; }
        public DbSet<Tournament_Team> Tournament_Team { get; set; }
        public DbSet<User> User { get; set; }

        public DataContext(String connectionString)
            : base(connectionString)
        {
            
        }
    }
}
