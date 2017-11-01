using api.NetConnect.DataControllers;
using api.NetConnect.data.ViewModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api.NetConnect.Converters;
using api.NetConnect.Helper;
using static api.NetConnect.Helper.PasswordHelper;
using api.NetConnect.data.ViewModel.Profile.Backend;
using api.NetConnect.data.ViewModel;

namespace api.NetConnect.Controllers
{
    using BackendProfileListViewModel = ListViewModel<BackendProfileViewModelItem>;
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
        public IHttpActionResult Backend_Get([FromUri] BackendProfileFilter filter)
        {
            BackendProfileListViewModel viewmodel = new BackendProfileListViewModel();

            // TODO

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Backend_Detail(Int32 id)
        {
            BackendProfileViewModel viewmodel = new BackendProfileViewModel();

            // TODO

            return Ok(viewmodel);
        }

        [HttpPost]
        public IHttpActionResult BackendDetail_Insert(ProfileViewModelItem request)
        {
            BackendProfileViewModel viewmodel = new BackendProfileViewModel();

            // TODO

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult BackendDetail_Update(Int32 id, ProfileViewModelItem request)
        {
            BackendProfileViewModel viewmodel = new BackendProfileViewModel();

            // TODO

            return Ok(viewmodel);
        }

        [HttpDelete]
        public IHttpActionResult Backend_Delete(BackendProfileDeleteRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();

            return Ok(viewmodel);
        }
        #endregion
    }
}
