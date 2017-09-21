using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel
{
    public class Pagination
    {
        public Int32 CurrentPage { get; set; }
        public Int32 ItemsPerPage { get; set; }
        public Int32 TotalItemsCount { get; set; }

        public Pagination()
        {
            CurrentPage = 1;
            ItemsPerPage = 10;
            TotalItemsCount = 0;
        }
    }
}
