using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.Entity
{
    public class BaseModel
    {
        public Int32 ID { get; set; }
        public DateTime last_change { get; set; }
    }
}
