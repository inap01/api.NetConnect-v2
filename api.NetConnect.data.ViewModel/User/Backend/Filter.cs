using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.User.Backend
{
    public class BackendProfileFilter
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }

        public BackendProfileFilter()
        {
            FirstName = "";
            LastName = "";
            Nickname = "";
        }
    }
}
