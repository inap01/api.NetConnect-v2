using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data
{
    public class _Order : BaseModel
    {
        public Int32 lan_id { get; set; }
        public Int32 user_id { get; set; }
        public Int32 seat_id { get; set; }
        public String details { get; set; }
        public Decimal price { get; set; }
        public Int32 complete_status { get; set; }
    }
}
