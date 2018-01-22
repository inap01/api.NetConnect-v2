using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Auth
{
    public class RegisterRequest
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }
        public String Email { get; set; }
        public String Password1 { get; set; }
        public String Password2 { get; set; }
        public Boolean Newsletter { get; set; }
    }
}
