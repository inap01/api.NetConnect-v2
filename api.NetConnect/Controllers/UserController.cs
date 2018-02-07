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
    public class UserController : ApiController
    {
        #region Frontend
        [HttpGet]
        public IHttpActionResult Detail(Int32 id)
        {
            UserViewModel viewmodel = new UserViewModel();
            viewmodel.Authenticated = UserHelper.Authenticated;

            try
            {
                viewmodel.Data.FromModel(UserDataController.GetItem(id));
            }
            catch(Exception ex)
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
            BackendProfileListViewModel viewmodel = new BackendProfileListViewModel();
            BackendProfileListArgs args = new BackendProfileListArgs();

            try
            {
                Int32 TotalItemsCount;
                viewmodel.Data = UserConverter.FilterList(args, out TotalItemsCount);

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
        public IHttpActionResult Backend_FilterList(BackendProfileListArgs args)
        {
            BackendProfileListViewModel viewmodel = new BackendProfileListViewModel();

            try
            {
                viewmodel.Filter.FirstName = args.Filter.FirstName;
                viewmodel.Filter.LastName = args.Filter.LastName;
                viewmodel.Filter.Nickname = args.Filter.Nickname;
                viewmodel.Pagination = args.Pagination;

                Int32 TotalItemsCount;
                viewmodel.Data = UserConverter.FilterList(args, out TotalItemsCount);

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
            BackendUserViewModel viewmodel = new BackendUserViewModel();

            try
            {
                viewmodel.Data.FromModel(UserDataController.GetItem(id));
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
        public IHttpActionResult Backend_Detail_Update(Int32 id, BackendUserViewModelItem request)
        {
            BackendUserViewModel viewmodel = new BackendUserViewModel();

            try
            {
                viewmodel.Data.FromModel(UserDataController.Update(request.ToModel()));

                viewmodel.AddSuccessAlert("Benutzer wurde erfolgreich aktualisiert.");
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
