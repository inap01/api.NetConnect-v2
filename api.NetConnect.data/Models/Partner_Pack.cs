using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data
{
    public class _Partner_Pack : BaseModel
    {
        public String name { get; set; }
        public Boolean show_partner { get; set; }
        public Boolean show_frontsite { get; set; }
        public Boolean show_trikot { get; set; }
    }
}
