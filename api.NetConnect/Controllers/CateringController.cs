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

                // TODO: 
                //Int32 eventID = EventDataController.GetItems().FirstOrDefault(x => x.Start <= DateTime.Now && x.End >= DateTime.Now).ID;
                Int32 eventID = 10;
                foreach (var model in SeatDataController.GetCurrentUserSeats(eventID))
                {
                    CateringSeat item = new CateringSeat();

                    item.FromModel(model);
                    viewmodel.Seats.Add(item);
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
                // TODO: 
                //Int32 eventID = EventDataController.GetItems().FirstOrDefault(x => x.Start <= DateTime.Now && x.End >= DateTime.Now).ID;
                Int32 eventID = 10;

                CateringOrderDataController.Insert(request.ToModel(eventID));
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