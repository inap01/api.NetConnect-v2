using api.NetConnect.DataControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api.NetConnect.Converters;
using api.NetConnect.Helper;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.EventType.Backend;

namespace api.NetConnect.Controllers
{
    public class EventTypeController : BaseController
    {
        #region Backend
        [Authorize(Roles = "Admin,Team")]
        [HttpGet]
        public IHttpActionResult Backend_Get()
        {
            BackendEventTypeListViewModel viewmodel = new BackendEventTypeListViewModel();
            BackendEventTypeListArgs args = new BackendEventTypeListArgs();
            EventTypeDataController dataCtrl = new EventTypeDataController();

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

        [Authorize(Roles = "Admin,Team")]
        [HttpPut]
        public IHttpActionResult Backend_FilterList(BackendEventTypeListArgs args)
        {
            BackendEventTypeListViewModel viewmodel = new BackendEventTypeListViewModel();
            EventTypeDataController dataCtrl = new EventTypeDataController();

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

        [Authorize(Roles = "Admin,Team")]
        [HttpGet]
        public IHttpActionResult Backend_Detail(Int32 id)
        {
            BackendEventTypeViewModel viewmodel = new BackendEventTypeViewModel();
            EventTypeDataController dataCtrl = new EventTypeDataController();

            try
            {
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
            BackendEventTypeViewModel viewmodel = new BackendEventTypeViewModel();

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
        public IHttpActionResult Backend_Detail_Insert(BackendEventTypeViewModelItem request)
        {
            BackendEventTypeViewModel viewmodel = new BackendEventTypeViewModel();

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
        [HttpPut]
        public IHttpActionResult Backend_Detail_Update(Int32 id, BackendEventTypeViewModelItem request)
        {
            BackendEventTypeViewModel viewmodel = new BackendEventTypeViewModel();
            EventTypeDataController dataCtrl = new EventTypeDataController();

            try
            {
                var result = dataCtrl.Update(request.ToModel());
                viewmodel.Data.FromModel(result);
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Der Eintrag wurde gespeichert.");
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpDelete]
        public IHttpActionResult Backend_Delete(BackendEventTypeDeleteRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();

            // TODO

            return Ok(viewmodel);
        }
        #endregion
    }
}
