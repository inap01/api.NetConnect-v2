using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Catering
{
    public class CateringListViewModel: ListViewModel<ProductViewModelItem>
    {
        public List<CateringSeat> Seats { get; set; }

        public CateringListViewModel()
        {
            Seats = new List<CateringSeat>();
        }
    }
    public class CateringViewModel : BaseViewModel
    {
        public ProductViewModelItem Data { get; set; }

        public CateringViewModel()
        {
            Data = new ProductViewModelItem();
        }
    }
    public class ProductViewModelItem : BaseViewModelItem
    {
        public String Name { get; set; }
        public Decimal Price { get; set; }
        public Boolean SingleChoice { get; set; }
        public String Image { get; set; }
        public List<ProductAttributeViewModelItem> ProductAttributes { get; set; }
    }
    public class ProductAttributeViewModelItem : BaseViewModelItem
    {
        public String Name { get; set; }
    }
    public class CateringSeat : BaseViewModelItem
    {
        public Int32 SeatNumber { get; set; }
    }
}
