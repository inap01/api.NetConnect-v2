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
    }
    public sealed class DataContext : NetConnect.data.Entity.Entities
    {
        public DataContext(String ConnectionString)
            :base(ConnectionString)
        {

        }        
    }
    
}
