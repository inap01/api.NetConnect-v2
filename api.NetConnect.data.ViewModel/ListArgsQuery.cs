using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel
{
    public class ListViewModel<T> : BaseViewModel
    {
        public List<T> Data { get; set; }

        public ListViewModel()
        {
            Data = new List<T>();
        }
        public T this[int key]
        {
            get
            {
                return Data[key];
            }
            set
            {
                Data[key] = value;
            }
        }
    }

    public class ListArgsViewModel<T, F, S> : ListViewModel<T>
        where F : new()
        where S : new()
    {
        public F Filter { get; set; }
        public S SortSettings { get; set; }
        public Pagination Pagination { get; set; }

        public ListArgsViewModel()
        {
            Data = new List<T>();
            Filter = new F();
            SortSettings = new S();
            Pagination = new Pagination();
        }

        public ListArgsViewModel(F f, S s, Pagination p)
        {
            Data = new List<T>();
            Filter = f;
            SortSettings = s;
            Pagination = p;
        }

        public ListArgsViewModel(ListArgsRequest<F, S> request)
        {
            Data = new List<T>();
            Filter = request.Filter;
            SortSettings = request.SortSettings;
            Pagination = request.Pagination;
        }
    }

    public class ListArgsRequest<F, S>
        where F : new()
        where S : new()
    {
        public F Filter { get; set; }
        public S SortSettings { get; set; }
        public Pagination Pagination { get; set; }
        public ListArgsRequest()
        {
            Filter = new F();
            SortSettings = new S();
            Pagination = new Pagination();
        }

        public ListArgsRequest(F f, S s, Pagination p)
        {
            Filter = f;
            SortSettings = s;
            Pagination = p;
        }
    }
}
