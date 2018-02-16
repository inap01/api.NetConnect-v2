using api.NetConnect.data.ViewModel;
using api.NetConnect.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace api.NetConnect.Controllers
{
    public class BaseController : ApiController
    {
        protected OkNegotiatedContentResult<T> Ok<T>(T content, String successMessage = null) where T : BaseViewModel
        {
            content.Authenticated = UserHelper.Authenticated;
            content.Success = true;
            if (successMessage != null)
                content.AddSuccessMessage(successMessage);

            return base.Ok<T>(content);
        }

        protected OkNegotiatedContentResult<T> Info<T>(T content, String successMessage = null) where T : BaseViewModel
        {
            content.Authenticated = UserHelper.Authenticated;
            content.Success = false;
            if (successMessage != null)
                content.AddInfoMessage(successMessage);

            return base.Ok<T>(content);
        }

        protected OkNegotiatedContentResult<T> Warning<T>(T content, String warningMessage = null) where T : BaseViewModel
        {
            content.Authenticated = UserHelper.Authenticated;
            content.Success = false;
            if (warningMessage != null)
                content.AddWarningMessage(warningMessage);

            return base.Ok<T>(content);
        }

        protected OkNegotiatedContentResult<T> Error<T>(T content, String errorMessage = null) where T : BaseViewModel
        {
            content.Authenticated = UserHelper.Authenticated;
            content.Success = false;
            if (errorMessage != null)
                content.AddErrorMessage(errorMessage);

            return base.Ok<T>(content);
        }

        protected OkNegotiatedContentResult<T> Error<T>(T content, Exception ex, String errorMessage = null) where T : BaseViewModel
        {
            content.Authenticated = UserHelper.Authenticated;

            if(Properties.Settings.Default.ClientDebugMode)
                errorMessage = exceptionHandling(ex, errorMessage);

            return Error(content, errorMessage);
        }

        private String exceptionHandling(Exception ex, String message = "{0}")
        {
            if (message == null)
                message = String.Empty;

            var innerEx = ex;
            while (innerEx.InnerException != null)
                innerEx = innerEx.InnerException;

            if (!message.Contains("{0}"))
                message += " {0}";

            if (message != null)
                message = String.Format(message, innerEx.Message);
            return message;
        }
    }
}
