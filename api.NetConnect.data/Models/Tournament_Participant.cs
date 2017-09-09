using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data
{
    public class Tournament_Participant : BaseModel
    {
        public Int32 user_id { get; set; }
        public Int32 tournament_id { get; set; }
        public Int32 team_id { get; set; }
        public DateTime registered { get; set; }
    }
}
