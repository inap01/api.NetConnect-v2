using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data
{
    public class _Product : BaseModel
    {
        public String description { get; set; }
        public String image { get; set; }
        public Decimal price { get; set; }
        public String attributes { get; set; }
        public Boolean single_choice { get; set; }
    }
}
