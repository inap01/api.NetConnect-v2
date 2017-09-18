using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.Entity
{
    public sealed class DataContext : NetConnect.data.Entity.NetConnectEntities
    {
        public DataContext(String ConnectionString)
            :base(ConnectionString)
        {

        }
    }
}
