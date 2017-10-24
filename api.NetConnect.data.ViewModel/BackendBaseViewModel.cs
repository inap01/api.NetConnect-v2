using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel
{
    public class BackendBaseViewModel : BaseViewModel
    {
        public Dictionary<String, InputInformation> Form { get; set; }

        public BackendBaseViewModel() : base()
        {
            Form = new Dictionary<String, InputInformation>();
        }

        public class InputInformation
        {
            public String Type { get; set; }
            public Boolean Readonly { get; set; }
            public Boolean Required { get; set; }
            public String Reference { get; set; }
            public Dictionary<String, InputInformation> ReferenceForm { get; set; }

            public InputInformation()
            {
                Readonly = false;
                Required = false;
            }
        }
    }
}
