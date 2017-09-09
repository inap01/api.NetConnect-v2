using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data
{
    public class Settings : BaseModel
    {
        public Int32 volume { get; set; }
        public Decimal vorkasse { get; set; }
        public Decimal abendkasse { get; set; }
        public DateTime start { get; set; }
        public DateTime ende { get; set; }
        public Boolean active_reservierung { get; set; }
        public DateTime kontocheck { get; set; }
        public Int32 tage_reservierung { get; set; }
        public Boolean catering { get; set; }
        public Boolean feedback { get; set; }
        public String feedback_link { get; set; }
        public Boolean chat { get; set; }
        public String kontoinhaber { get; set; }
        public String iban { get; set; }
        public String blz { get; set; }
        public String kontonummer { get; set; }
        public String bic { get; set; }
    }
}
