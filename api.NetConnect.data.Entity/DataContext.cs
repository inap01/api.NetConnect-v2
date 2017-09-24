using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.Entity
{
    public partial class Entities
    {
        public Entities(String conn)
            : base(conn)
        {

        }
        //public Dictionary<Type, byte[]> GetChanges()
        //{
        //    var dic = new Dictionary<Type, byte[]>();

        //    dic.Add(typeof(Partner), this.Partner.Max(x => x.LastChange));
        //}
    }
    public sealed class DataContext : NetConnect.data.Entity.Entities
    {
        public DataContext(String ConnectionString)
            :base(ConnectionString)
        {

        }
        public override int SaveChanges()
        {
            DateTime saveTime = DateTime.UtcNow;
            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == (EntityState)System.Data.Entity.EntityState.Added || e.State == (EntityState)System.Data.Entity.EntityState.Modified))
            {
                if (entry.Property("LastChange").CurrentValue == null)
                    entry.Property("LastChange").CurrentValue = BitConverter.GetBytes(DateTime.MinValue.Ticks + 6);
            }
            return base.SaveChanges();
        }
    }
    
}
