using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Catering.Backend
{
    public class BackendCateringFilter
    {
        public String Name { get; set; }
        public String SeatNumber { get; set; }
        public CateringFilterEvent EventSelected { get; set; }
        public List<CateringFilterEvent> EventOptions { get; set; }
        public CateringStatusFilterEnum StatusSelected { get; set; }
        private List<CateringStatusFilterEnum> _statusOptions { get; set; }
        public List<CateringStatusFilterEnum> StatusOptions
        {
            get { return _statusOptions; }
            set
            {
                var union = _statusOptions.Union(value).Distinct();
                _statusOptions = union.ToList();
            }
        }

        public BackendCateringFilter()
        {
            Name = "";
            SeatNumber = "";
            StatusSelected = CateringStatusFilterEnum.Offen;
            _statusOptions = new List<CateringStatusFilterEnum>();
            StatusOptions = new List<CateringStatusFilterEnum>(CateringStatusFilter.getOptions());
        }

        public class CateringFilterEvent
        {
            public Int32 ID { get; set; }
            public String Name { get; set; }
        }
    }

    public class BackendProductFilter
    {
        public String Name { get; set; }
    }

    public class BackendProductAttributeFilter
    {
        public String Name { get; set; }
    }
}
