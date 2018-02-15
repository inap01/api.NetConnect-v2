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


    public enum CateringStatusFilterEnum { Alle, Offen, Abgeschlossen }

    public class CateringStatusFilter
    {
        public static IEnumerable<CateringStatusFilterEnum> getOptions()
        {
            return (IEnumerable<CateringStatusFilterEnum>)(Enum.GetValues(typeof(CateringStatusFilterEnum)));
        }
    }


    public enum SeatingStatusFilterEnum { Ungefiltert, Frei, Vorgemerkt, Reserviert, NetConnect }

    public class SeatingStatusFilter
    {
        public static IEnumerable<SeatingStatusFilterEnum> getOptions()
        {
            return (IEnumerable<SeatingStatusFilterEnum>)(Enum.GetValues(typeof(SeatingStatusFilterEnum)));
        }
    }
}
