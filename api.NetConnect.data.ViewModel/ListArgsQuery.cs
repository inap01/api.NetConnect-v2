using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel
{
    public class ListArgsQuery<T, F, S> : BaseViewModel
        where F : new()
        where S : new()
    {
        public List<T> Data { get; set; }
        public F Filter { get; set; }
        public S SortSettings { get; set; }
        public Pagination Pagination { get; set; }

        public ListArgsQuery()
        {
            Data = new List<T>();
            Filter = new F();
            SortSettings = new S();
            Pagination = new Pagination();
        }

        public ListArgsQuery(F f, S s)
        {
            Filter = f;
            SortSettings = s;
        }
    }
}
