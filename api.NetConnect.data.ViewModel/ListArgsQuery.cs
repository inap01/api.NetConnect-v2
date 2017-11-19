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
        public virtual T this[int key]
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

    public class ListArgsViewModel<T, F> : ListViewModel<T>
        where F : new()
    {
        public F Filter { get; set; }
        public Pagination Pagination { get; set; }

        public ListArgsViewModel()
        {
            Data = new List<T>();
            Filter = new F();
            Pagination = new Pagination();
        }

        public ListArgsViewModel(F f, Pagination p)
        {
            Data = new List<T>();
            Filter = f;
            Pagination = p;
        }

        public ListArgsViewModel(ListArgsRequest<F> request)
        {
            Data = new List<T>();
            Filter = request.Filter;
            Pagination = request.Pagination;
        }
    }

    public class ListArgsRequest<F>
        where F : new()
    {
        public F Filter { get; set; }
        public Pagination Pagination { get; set; }
        public ListArgsRequest()
        {
            Filter = new F();
            Pagination = new Pagination();
        }

        public ListArgsRequest(F f, Pagination p)
        {
            Filter = f;
            Pagination = p;
        }
    }
}
