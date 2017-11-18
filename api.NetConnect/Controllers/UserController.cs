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
using static api.NetConnect.Helper.PasswordHelper;
using api.NetConnect.data.ViewModel.User.Backend;
using api.NetConnect.data.ViewModel;

namespace api.NetConnect.Controllers
{
    using BackendProfileListArgs = ListArgsRequest<BackendProfileFilter>;
    using BackendProfileListViewModel = ListArgsViewModel<BackendUserViewModelItem, BackendProfileFilter>;
    public class UserController : ApiController
    {
        #region Frontend
        [HttpGet]
        public IHttpActionResult Detail(Int32 id)
        {
            ProfileViewModel viewmodel = new ProfileViewModel();

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

        [HttpPut]
        public IHttpActionResult Detail_Update(Int32 id, ProfileViewModelItem request)
        {
            ProfileViewModel viewmodel = new ProfileViewModel();

            try
            {
                var updateModel = UserDataController.GetItem(id);
                updateModel.FromViewModel(request);

                if (request.OldPassword != null && request.NewPassword1 != null && request.NewPassword2 != null)
                    updateModel.Password = PasswordHelper.ChangePassword(id, request.OldPassword, request.NewPassword1, request.NewPassword2);

                updateModel = UserDataController.Update(updateModel);
                viewmodel.Data.FromModel(updateModel);
            }
            catch (WrongPasswordException ex)
            {
                viewmodel.Success = false;
                viewmodel.AddWarningAlert(ExceptionHelper.FullException(ex));
            }
            catch (PasswordsNotEqualException ex)
            {
                viewmodel.Success = false;
                viewmodel.AddWarningAlert(ExceptionHelper.FullException(ex));
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
        public IHttpActionResult BackendDetail_Update(Int32 id, ProfileViewModelItem request)
        {
            BackendUserViewModel viewmodel = new BackendUserViewModel();

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
