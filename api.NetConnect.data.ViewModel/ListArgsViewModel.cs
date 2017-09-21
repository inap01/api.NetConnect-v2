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

    class ListArgsViewModel2<T> : BaseViewModel
    {
        public List<IBaseViewModelItem> Data { get; set; }
        public IBaseFilter<T> Filter { get; set; }
        public IBaseSortSetting<T> SortSettings { get; set; }
        public IBasePagination<T> Pagination { get; set; }

        public ListArgsViewModel2(IBaseFilter<T> f, IBaseSortSetting<T> s, IBasePagination<T> p)
        {
            Filter = f;
            SortSettings = s;
            Pagination = p;
        }
    }

    public interface IBasePagination<T>
    {
    }

    public interface IBaseSortSetting<T>
    {
    }

    public interface IBaseFilter<T>
    {
    }

    public class TournamentFilter : IBaseFilter<Tournament.TournamentViewModelItem>
    {

    }
}
