using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data
{
    public class User : BaseModel
    {
        public String email { get; set; }
        public String password { get; set; }
        public String password_reset { get; set; }
        public DateTime registered_since { get; set; }
        public String first_name { get; set; }
        public String last_name { get; set; }
        public String nickname { get; set; }
        public Boolean is_team { get; set; }
        public Boolean is_admin { get; set; }
        public Boolean is_vorstand { get; set; }
        public String image { get; set; }
        public String steam_id { get; set; }
        public String battle_tag { get; set; }
        public Boolean newsletter { get; set; }
    }
}
