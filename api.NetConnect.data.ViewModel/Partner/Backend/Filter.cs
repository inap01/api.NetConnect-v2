using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Partner.Backend
{
    public class BackendPartnerFilter
    {
        private string all = "Alle";
        public String Name { get; set; }
        public StatusFilterEnum StatusSelected { get; set; }
        private List<StatusFilterEnum> _statusOptions { get; set; }
        public List<StatusFilterEnum> StatusOptions
        {
            get { return _statusOptions; }
            set
            {
                var union = _statusOptions.Union(value).Distinct();
                _statusOptions = union.ToList();
            }
        }
        public String PartnerTypeSelected { get; set; }
        private List<String> _partnerTypeOptions;
        public List<String> PartnerTypeOptions
        {
            get { return _partnerTypeOptions; }
            set
            {
                if (!_partnerTypeOptions.Contains(all))
                    _partnerTypeOptions.Add(all);
                var union = _partnerTypeOptions.Union(value).Distinct();
                _partnerTypeOptions = union.ToList();
            }
        }

        public BackendPartnerFilter()
        {
            Name = "";
            StatusSelected = StatusFilterEnum.Alle;
            _statusOptions = new List<StatusFilterEnum>();
            StatusOptions = new List<StatusFilterEnum>(StatusFilter.getOptions());
            PartnerTypeSelected = all;
            _partnerTypeOptions = new List<string>();
        }
    }
}
