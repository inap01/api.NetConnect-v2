using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel
{
    public class Form
    {
        public static Dictionary<string, InputInformation> GetReferenceForm(Dictionary<string, InputInformation> dict)
        {
            for(int i = 0; i < dict.Count; i++)
            {
                dict.ElementAt(i).Value.Readonly = true;
                dict.ElementAt(i).Value.Required = false;
            }

            return dict;
        }
    }
}
