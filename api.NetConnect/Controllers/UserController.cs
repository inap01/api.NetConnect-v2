using api.NetConnect.DataControllers;
using api.NetConnect.data.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api.NetConnect.Converters;
using api.NetConnect.Helper;
using api.NetConnect.data.ViewModel.User.Backend;
using api.NetConnect.data.ViewModel;

namespace api.NetConnect.Controllers
{
    public class UserController : BaseController
    {
        #region Frontend
        [HttpGet]
        public IHttpActionResult Detail(Int32 id)
        {
            UserViewModel viewmodel = new UserViewModel();
            UserDataController dataCtrl = new UserDataController();

            try
            {
                viewmodel.Data.FromModel(dataCtrl.GetItem(id));
            }
            catch(Exception ex)
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
            BackendProfileListViewModel viewmodel = new BackendProfileListViewModel();
            BackendProfileListArgs args = new BackendProfileListArgs();
            UserDataController dataCtrl = new UserDataController();

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
        public IHttpActionResult Backend_FilterList(BackendProfileListArgs args)
        {
            BackendProfileListViewModel viewmodel = new BackendProfileListViewModel();
            UserDataController dataCtrl = new UserDataController();

            try
            {
                viewmodel.Filter.FirstName = args.Filter.FirstName;
                viewmodel.Filter.LastName = args.Filter.LastName;
                viewmodel.Filter.Nickname = args.Filter.Nickname;
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
            BackendUserViewModel viewmodel = new BackendUserViewModel();
            UserDataController dataCtrl = new UserDataController();

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

        [HttpPut]
        public IHttpActionResult Backend_Detail_Update(Int32 id, BackendUserViewModelItem request)
        {
            BackendUserViewModel viewmodel = new BackendUserViewModel();
            UserDataController dataCtrl = new UserDataController();

            try
            {
                viewmodel.Data.FromModel(dataCtrl.Update(request.ToModel()));
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Benutzer wurde erfolgreich aktualisiert.");
        }
        #endregion
    }
}
