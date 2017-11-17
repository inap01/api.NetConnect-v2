﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel
{
    public partial class BaseViewModel
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
                Type = AlertState.info,
                Message = message
            });
        }

        public void AddSuccessAlert(String message)
        {
            AlertMessages.Add(new AlertMessage()
            {
                Type = AlertState.success,
                Message = message
            });
        }

        public void AddWarningAlert(String message)
        {
            AlertMessages.Add(new AlertMessage()
            {
                Type = AlertState.warning,
                Message = message
            });
        }

        public void AddDangerAlert(String message)
        {
            AlertMessages.Add(new AlertMessage()
            {
                Type = AlertState.danger,
                Message = message
            });
        }
    }

    public class BaseViewModelItem
    {
        public Int32 ID { get; set; }
    }

    public enum AlertState { info, success, warning, danger }

    public class AlertMessage
    {
        public AlertState Type { get; set; }
        public String Message { get; set; }
    }
}