using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data
{
    public class _User : BaseModel
    {

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String PasswordReset { get; set; }
        public DateTime Registered { get; set; }
        public Boolean IsTeam { get; set; }
        public Boolean IsAdmin { get; set; }
        public Boolean IsVorstand { get; set; }
        public String Image { get; set; }
        public String SteamID { get; set; }
        public String BattleTag { get; set; }        
        public Boolean Newsletter { get; set; }
    }
}
