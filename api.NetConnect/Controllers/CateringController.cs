using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Catering;
using api.NetConnect.DataControllers;

namespace api.NetConnect.Controllers
{
    using api.NetConnect.Helper;
    using Converters;
    using CateringListViewModel = ListViewModel<ProductViewModelItem>;

    public class CateringController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            CateringListViewModel viewmodel = new CateringListViewModel();
            viewmodel.Authenticated = UserHelper.Authenticated;
            
            try
            {
                foreach (var model in CateringDataController.GetItems())
                {
                    ProductViewModelItem item = new ProductViewModelItem();

                    item.FromModel(model);
                    viewmodel.Data.Add(item);
                }
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Detail(Int32 ID)
        {
            CateringViewModel viewmodel = new CateringViewModel();
            viewmodel.Authenticated = UserHelper.Authenticated;
            
            try
            {
                viewmodel.Data.FromModel(CateringDataController.GetItem(ID));
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPost]
        public IHttpActionResult Insert(OrderRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();

            try
            {
                CateringOrderDataController.Insert(request.ToModel());
                viewmodel.AddSuccessAlert("Bestellung ist eingegangen.");
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }
    }
}