using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel
{
    public class Pagination
    {
        public Int32 Page { get; set; }
        public Int32 ItemsPerPageSelected { get; set; }
        public Int32 TotalItemsCount { get; set; }

        public Pagination()
        {
            Page = 1;
            ItemsPerPageSelected = 10;
            TotalItemsCount = 0;
        }
    }
}
