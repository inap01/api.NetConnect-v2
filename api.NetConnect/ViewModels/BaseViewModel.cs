using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.ViewModels
{
    public class BaseViewModel
    {
        public Boolean Success { get; set; }
        public List<AlertMessage> AlertMessages { get; set; }

        public BaseViewModel()
        {
            Success = true;
            AlertMessages = new List<AlertMessage>();
        }

        public void AddInfoAlert(String message)
        {
            AlertMessages.Add(new AlertMessage()
            {
                State = AlertStates.info,
                Message = message
            });
        }

        public void AddSuccessAlert(String message)
        {
            AlertMessages.Add(new AlertMessage()
            {
                State = AlertStates.success,
                Message = message
            });
        }

        public void AddWarningAlert(String message)
        {
            AlertMessages.Add(new AlertMessage()
            {
                State = AlertStates.warning,
                Message = message
            });
        }

        public void AddDangerAlert(String message)
        {
            AlertMessages.Add(new AlertMessage()
            {
                State = AlertStates.danger,
                Message = message
            });
        }
    }

    public enum AlertStates { info, success, warning, danger }

    public class AlertMessage
    {
        public AlertStates State { get; set; }
        public String Message { get; set; }
    }
}