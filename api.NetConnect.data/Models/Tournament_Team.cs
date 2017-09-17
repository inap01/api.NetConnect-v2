using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data
{
    public class _Tournament_Team : BaseModel
    {
        public String name { get; set; }
        public Int32 tournament_id { get; set; }
        public String password { get; set; }
    }
}
