using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data
{
    public class _Tournament : BaseModel
    {
        public Int32 lan_id { get; set; }
        public Int32 game_id { get; set; }
        public Int32 team { get; set; }
        public String link { get; set; }
        public String mode { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public Boolean pause_game { get; set; }
        public Int32 powered_by { get; set; }
    }
}
