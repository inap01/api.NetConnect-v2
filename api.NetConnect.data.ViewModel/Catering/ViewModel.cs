using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Catering
{
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
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool SingleChoice { get; set; }
        public bool IsActive { get; set; }
        public List<ProductAttributeViewModelItem> ProductAttribute { get; set;}        
    }
    public class ProductAttributeViewModelItem : BaseViewModelItem
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
