using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel
{
    public enum StatusFilterEnum { Alle, Aktiv, Inaktiv }

    public class StatusFilter
    {
        public static IEnumerable<StatusFilterEnum> getOptions()
        {
            return (IEnumerable<StatusFilterEnum>)(Enum.GetValues(typeof(StatusFilterEnum)));
        }
    }
}
