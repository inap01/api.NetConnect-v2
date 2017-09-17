using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data
{
    public class _Seat : BaseModel
    {
        public Int32 user_id { get; set; }
        public Int32 status { get; set; }
        public String description { get; set; }
        public DateTime date { get; set; }
        public DateTime payed { get; set; }
    }
}
