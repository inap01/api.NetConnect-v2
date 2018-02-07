using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Catering;
using api.NetConnect.data.ViewModel.Catering.Backend;
using api.NetConnect.Converters;
using api.NetConnect.DataControllers;
using api.NetConnect.Helper;
using api.NetConnect.data.ViewModel.Event.Backend;

namespace api.NetConnect.Controllers
{
    public class CateringController : ApiController
    {
        #region Frontend
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

        [Authorize()]
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
        #endregion
        #region Backend
        [HttpGet]
        public IHttpActionResult Backend_Get()
        {
            BackendCateringListViewModel viewmodel = new BackendCateringListViewModel();
            BackendCateringListArgs args = new BackendCateringListArgs();

            try
            {
                var events = EventDataController.GetItems().OrderByDescending(x => x.ID).ToList();
                viewmodel.Filter.EventOptions = events.ConvertAll(x =>
                {
                    return new BackendCateringFilter.CateringFilterEvent()
                    {
                        ID = x.ID,
                        Name = $"{x.EventType.Name} Vol.{x.Volume}"
                    };
                });
                viewmodel.Filter.EventSelected = viewmodel.Filter.EventOptions[0];
                args.Filter.EventSelected = viewmodel.Filter.EventOptions[0];

                Int32 TotalItemsCount = 0;
                viewmodel.FromModel(CateringOrderDataController.FilterList(args, out TotalItemsCount));
                viewmodel.Pagination.TotalItemsCount = TotalItemsCount;
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult Backend_FilterList(BackendCateringListArgs args)
        {
            BackendCateringListViewModel viewmodel = new BackendCateringListViewModel();

            try
            {
                var events = EventDataController.GetItems().OrderByDescending(x => x.ID).ToList();
                viewmodel.Filter.EventOptions = events.ConvertAll(x =>
                {
                    return new BackendCateringFilter.CateringFilterEvent()
                    {
                        ID = x.ID,
                        Name = $"{x.EventType.Name} Vol.{x.Volume}"
                    };
                });

                viewmodel.Filter.Name = args.Filter.Name;
                viewmodel.Filter.SeatNumber = args.Filter.SeatNumber;
                viewmodel.Filter.EventSelected = args.Filter.EventSelected;
                viewmodel.Filter.StatusSelected = args.Filter.StatusSelected;
                viewmodel.Pagination = args.Pagination;
                
                Int32 TotalItemsCount = 0;
                viewmodel.FromModel(CateringOrderDataController.FilterList(args, out TotalItemsCount));
                viewmodel.Pagination.TotalItemsCount = TotalItemsCount;
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Backend_Detail(Int32 id)
        {
            BackendCateringViewModel viewmodel = new BackendCateringViewModel();

            try
            {
                viewmodel.EventOptions = EventDataController.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendEventViewModelItem().FromModel(x);
                }).OrderByDescending(x => x.ID).ToList();

                viewmodel.Data.FromModel(CateringOrderDataController.GetItem(id));
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPost]
        public IHttpActionResult Backend_Detail_Insert(BackendCateringViewModelItem request)
        {
            BackendCateringViewModel viewmodel = new BackendCateringViewModel();

            try
            {
                // TODO
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult Backend_Detail_Update(Int32 id, BackendCateringViewModelItem request)
        {
            BackendCateringViewModel viewmodel = new BackendCateringViewModel();

            try
            {
                // TODO
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }
        #endregion
    }
}