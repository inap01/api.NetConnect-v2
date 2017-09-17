using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data
{
    public class _Partner : BaseModel
    {
        public String name { get; set; }
        public String link { get; set; }
        public String content { get; set; }
        public String image { get; set; }
        public String image_alt { get; set; }
        public Int32 status { get; set; }
        public Boolean active { get; set; }
        public Int32 position { get; set; }
        public Int32 click_count { get; set; }
    }
}
