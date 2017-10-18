using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel
{
    public class BackendBaseViewModel : BaseViewModel
    {
        public List<InputInformation> Form { get; set; }

        public BackendBaseViewModel() : base()
        {
            Form = new List<InputInformation>();
        }

        public class InputInformation
        {
            public String Key { get; set; }
            public String Type { get; set; }
            public Boolean Readonly { get; set; }
        }
    }
}
