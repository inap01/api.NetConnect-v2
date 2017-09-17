using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data
{
    public class _Tournament_Game : BaseModel
    {
        public String name { get; set; }
        public String icon { get; set; }
        public String rules { get; set; }
        public Boolean battletag { get; set; }
        public Boolean steam { get; set; }
    }
}
