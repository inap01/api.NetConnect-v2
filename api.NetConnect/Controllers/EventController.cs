using api.NetConnect.DataControllers;
using api.NetConnect.data.ViewModel.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api.NetConnect.Converters;
using api.NetConnect.Helper;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Event.Backend;
using api.NetConnect.data.ViewModel.EventType.Backend;

namespace api.NetConnect.Controllers
{
    public class EventController : BaseController
    {
        #region Frontend
        [HttpGet]
        public IHttpActionResult Get()
        {
            EventListViewModel viewmodel = new EventListViewModel();
            EventDataController dataCtrl = new EventDataController();

            try
            {
                var events = dataCtrl.GetItems().Where(x => x.End >= DateTime.Now).OrderBy(y => y.Start);
                foreach(var e in events)
                {
                    viewmodel.Data.Add(new EventViewModelItem().FromModel(e));
                }
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Detail(Int32 id)
        {
            EventViewModel viewmodel = new EventViewModel();
            EventDataController dataCtrl = new EventDataController();

            try
            {
                viewmodel.Data = new EventViewModelItem().FromModel(dataCtrl.GetItem(id));
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }
        #endregion

        #region Backend
        [HttpGet]
        public IHttpActionResult Backend_Get()
        {
            BackendEventListViewModel viewmodel = new BackendEventListViewModel();
            BackendEventListArgs args = new BackendEventListArgs();
            EventDataController dataCtrl = new EventDataController();

            try
            {
                Int32 TotalItemsCount;
                viewmodel.Data.FromModel(dataCtrl.FilterList(args, out TotalItemsCount));

                viewmodel.Pagination.TotalItemsCount = TotalItemsCount;
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult Backend_FilterList(BackendEventListArgs args)
        {
            BackendEventListViewModel viewmodel = new BackendEventListViewModel();
            EventDataController dataCtrl = new EventDataController();

            try
            {
                viewmodel.Filter.Name = args.Filter.Name;
                viewmodel.Pagination = args.Pagination;

                Int32 TotalItemsCount;
                viewmodel.Data.FromModel(dataCtrl.FilterList(args, out TotalItemsCount));

                viewmodel.Pagination.TotalItemsCount = TotalItemsCount;
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Backend_Detail(Int32 id)
        {
            BackendEventViewModel viewmodel = new BackendEventViewModel();
            EventDataController dataCtrl = new EventDataController();
            EventTypeDataController eventTypeDataCtrl = new EventTypeDataController();

            try
            {
                viewmodel.EventTypeOptions = eventTypeDataCtrl.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendEventTypeViewModelItem() { ID = x.ID, Name = x.Name };
                }).OrderByDescending(x => x.ID).ToList();

                viewmodel.Data.FromModel(dataCtrl.GetItem(id));
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Backend_Detail_New()
        {
            BackendEventViewModel viewmodel = new BackendEventViewModel();

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

        [HttpPost]
        public IHttpActionResult Backend_Detail_Insert(BackendEventViewModelItem request)
        {
            BackendEventViewModel viewmodel = new BackendEventViewModel();

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

        [HttpPut]
        public IHttpActionResult Backend_Detail_Update(Int32 id, BackendEventViewModelItem request)
        {
            BackendEventViewModel viewmodel = new BackendEventViewModel();
            EventDataController dataCtrl = new EventDataController();

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

        [HttpDelete]
        public IHttpActionResult Backend_Delete(BackendEventDeleteRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();

            // TODO

            return Ok(viewmodel);
        }
        #endregion
    }
}
