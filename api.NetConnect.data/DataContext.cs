using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data
{
    public class DataContext : DbContext
    {
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            OnModelCreatingEntity_User(modelBuilder.Entity<User>());
        }

        private void OnModelCreatingEntity_User(EntityTypeConfiguration<User> e)
        {
            e.ToTable("User", "dbo");
            e.HasKey(a => new { a.ID });

            PrimitivePropertyConfiguration pP;
            pP = e.Property(a => a.ID);
            pP.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            StringPropertyConfiguration pS;

            // FirstName
            pS = e.Property(a => a.FirstName);
            pS.HasMaxLength(255);
            pS.IsRequired();

            // LastName
            pS = e.Property(a => a.LastName);
            pS.HasMaxLength(255);
            pS.IsRequired();

            // Nickname
            pS = e.Property(a => a.Nickname);
            pS.HasMaxLength(255);
            pS.IsRequired();
            pS.IsUnicode(true);

            // Email
            pS = e.Property(a => a.Email);
            pS.HasMaxLength(255);
            pS.IsRequired();
            pS.IsUnicode(true);

            // Password
            pS = e.Property(a => a.Password);
            pS.HasMaxLength(64);
            pS.IsRequired();

            // PasswordReset
            pS = e.Property(a => a.PasswordReset);
            pS.HasMaxLength(64);
            pS.IsOptional();

            // Image
            pS = e.Property(a => a.Image);
            pS.IsOptional();

            // SteamID
            pS = e.Property(a => a.SteamID);
            pS.IsOptional();

            // BattleTag
            pS = e.Property(a => a.BattleTag);
            pS.IsOptional();
        }

        public DataContext(String connectionString)
            : base(connectionString)
        {
            
        }
    }
}
