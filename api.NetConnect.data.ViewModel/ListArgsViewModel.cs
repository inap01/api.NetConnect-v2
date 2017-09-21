using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel
{
    class ListArgsViewModel<T, F, S, P> : BaseViewModel
        where F : new()
        where S : new()
        where P : new()
    {
        public List<T> Data { get; set; }
        public F Filter { get; set; }
        public S SortSettings { get; set; }
        public P Pagination { get; set; }

        public ListArgsViewModel()
        {
            Data = new List<T>();
            Filter = new F();
            SortSettings = new S();
            Pagination = new P();
        }

        public ListArgsViewModel(F f, S s, P p)
        {
            Filter = f;
            SortSettings = s;
            Pagination = p;
        }
    }
}
