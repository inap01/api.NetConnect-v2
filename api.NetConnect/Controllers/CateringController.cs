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
using api.NetConnect.data.ViewModel.User.Backend;

namespace api.NetConnect.Controllers
{
    public class CateringController : BaseController
    {
        #region Frontend
        [HttpGet]
        public IHttpActionResult Get()
        {
            CateringListViewModel viewmodel = new CateringListViewModel();
            CateringDataController dataCtrl = new CateringDataController();
            SeatDataController seatDataCtrl = new SeatDataController();
            EventDataController eventDataCtrl = new EventDataController();

            try
            {
                foreach (var model in dataCtrl.GetItems())
                {
                    ProductViewModelItem item = new ProductViewModelItem();

                    item.FromModel(model);
                    viewmodel.Data.Add(item);
                }
                
                var e = eventDataCtrl.GetItems().FirstOrDefault(x => x.Start <= DateTime.Now && x.End >= DateTime.Now);
                if(e == null)
                {
                    return Warning(viewmodel, "Keine passende Veranstaltung gefunden.");
                }
                else if(!e.IsActiveCatering)
                {
                    return Warning(viewmodel, "Das Catering ist derzeit deaktiviert.");
                }

                int eventID = e.ID;
                foreach (var model in seatDataCtrl.GetCurrentUserSeats(eventID))
                {
                    CateringSeat item = new CateringSeat();

                    item.FromModel(model);
                    viewmodel.Seats.Add(item);
                }
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Detail(Int32 ID)
        {
            CateringViewModel viewmodel = new CateringViewModel();
            CateringDataController dataCtrl = new CateringDataController();

            try
            {
                viewmodel.Data.FromModel(dataCtrl.GetItem(ID));
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [Authorize()]
        [HttpPost]
        public IHttpActionResult Insert(OrderRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();
            CateringOrderDataController dataCtrl = new CateringOrderDataController();
            EventDataController eventDataCtrl = new EventDataController();

            try
            {
                var e = eventDataCtrl.GetItems().FirstOrDefault(x => x.Start <= DateTime.Now && x.End >= DateTime.Now);
                if (e == null)
                {
                    return Warning(viewmodel, "Die Bestellung konnte keiner Veranstaltung zugeordnet werden.");
                }

                int eventID = e.ID;
                dataCtrl.Insert(request.ToModel(eventID));
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Bestellung ist eingegangen.");
        }
        #endregion
        #region Backend
        [Authorize(Roles = "Admin,Team")]
        [HttpGet]
        public IHttpActionResult Backend_Get()
        {
            BackendCateringListViewModel viewmodel = new BackendCateringListViewModel();
            BackendCateringListArgs args = new BackendCateringListArgs();
            EventDataController eventDataCtrl = new EventDataController();
            CateringOrderDataController orderDataCtrl = new CateringOrderDataController();
            CateringDataController cateringDataCtrl = new CateringDataController();
            UserDataController userDataCtrl = new UserDataController();

            try
            {
                var events = eventDataCtrl.GetItems().OrderByDescending(x => x.ID).ToList();
                viewmodel.Filter.EventOptions = events.ConvertAll(x =>
                {
                    return new BackendCateringFilter.CateringFilterEvent()
                    {
                        ID = x.ID,
                        Name = $"{x.EventType.Name} Vol.{x.Volume}"
                    };
                });

                var products = cateringDataCtrl.GetItems().ToList();
                viewmodel.ProductOptions = products.ConvertAll(x =>
                {
                    return new BackendCateringProductItem().FromModel(x);
                });

                viewmodel.Filter.EventSelected = viewmodel.Filter.EventOptions[0];
                args.Filter.EventSelected = viewmodel.Filter.EventOptions[0];

                Int32 TotalItemsCount = 0;
                viewmodel.FromModel(orderDataCtrl.FilterList(args, out TotalItemsCount));
                viewmodel.Pagination.TotalItemsCount = TotalItemsCount;
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpPut]
        public IHttpActionResult Backend_FilterList(BackendCateringListArgs args)
        {
            BackendCateringListViewModel viewmodel = new BackendCateringListViewModel();
            EventDataController eventDataCtrl = new EventDataController();
            CateringOrderDataController orderDataCtrl = new CateringOrderDataController();
            CateringDataController cateringDataCtrl = new CateringDataController();
            UserDataController userDataCtrl = new UserDataController();

            try
            {
                var events = eventDataCtrl.GetItems().OrderByDescending(x => x.ID).ToList();
                viewmodel.Filter.EventOptions = events.ConvertAll(x =>
                {
                    return new BackendCateringFilter.CateringFilterEvent()
                    {
                        ID = x.ID,
                        Name = $"{x.EventType.Name} Vol.{x.Volume}"
                    };
                });

                var products = cateringDataCtrl.GetItems().ToList();
                viewmodel.ProductOptions = products.ConvertAll(x =>
                {
                    return new BackendCateringProductItem().FromModel(x);
                });

                viewmodel.Filter.Name = args.Filter.Name;
                viewmodel.Filter.SeatNumber = args.Filter.SeatNumber;
                viewmodel.Filter.EventSelected = args.Filter.EventSelected;
                viewmodel.Filter.StatusSelected = args.Filter.StatusSelected;
                viewmodel.Pagination = args.Pagination;
                
                Int32 TotalItemsCount = 0;
                viewmodel.FromModel(orderDataCtrl.FilterList(args, out TotalItemsCount));
                viewmodel.Pagination.TotalItemsCount = TotalItemsCount;
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpGet]
        public IHttpActionResult Backend_Detail(Int32 id)
        {
            BackendCateringViewModel viewmodel = new BackendCateringViewModel();
            CateringOrderDataController dataCtrl = new CateringOrderDataController();
            EventDataController eventDataCtrl = new EventDataController();
            UserDataController userDataCtrl = new UserDataController();

            try
            {
                viewmodel.EventOptions = eventDataCtrl.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendEventViewModelItem().FromModel(x);
                }).OrderByDescending(x => x.ID).ToList();
                viewmodel.UserOptions = userDataCtrl.GetItems().OrderBy(x => x.FirstName).ToList().ConvertAll(x =>
                {
                    return new BackendUserViewModelItem().FromModel(x);
                });


                viewmodel.Data.FromModel(dataCtrl.GetItem(id));
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpGet]
        public IHttpActionResult Backend_Detail_New()
        {
            BackendCateringViewModel viewmodel = new BackendCateringViewModel();

            try
            {
                // TODO
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpPost]
        public IHttpActionResult Backend_Detail_Insert(BackendNewOrderRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();
            CateringOrderDataController dataCtrl = new CateringOrderDataController();

            try
            {
                dataCtrl.Insert(request.ToModel());
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Bestellung wurde aufgenommen.");
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpPut]
        public IHttpActionResult Backend_Detail_Update(Int32 id, BackendCateringViewModelItem request)
        {
            BackendCateringViewModel viewmodel = new BackendCateringViewModel();
            CateringOrderDataController dataCtrl = new CateringOrderDataController();

            try
            {
                dataCtrl.Update(request.ToModel());
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Eintrag wurde gespeichert.");
        }
        #endregion
    }
}