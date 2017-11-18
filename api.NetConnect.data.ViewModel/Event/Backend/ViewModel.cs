using api.NetConnect.data.ViewModel.EventType.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Event.Backend
{
    public class BackendEventViewModel : BackendBaseViewModel
    {
        public BackendEventViewModelItem Data { get; set; }
        public List<BackendEventTypeViewModelItem> EventTypeOptions { get; set; }

        public BackendEventViewModel()
        {
            Data = new BackendEventViewModelItem();
            EventTypeOptions = new List<BackendEventTypeViewModelItem>();

            Form = GetForm();
        }

        public override Dictionary<string, InputInformation> GetForm()
        {
            return BackendEventViewModelItem.GetForm();
        }
    }

    public class BackendEventViewModelItem : BaseViewModelItem
    {
        public Int32 Volume { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public String Image { get; set; }
        public BackendEventTypeViewModelItem EventType { get; set; }

        public BackendEventViewModelItem()
        {
            EventType = new BackendEventTypeViewModelItem();
        }

        public static Dictionary<string, InputInformation> GetForm()
        {
            Dictionary<string, InputInformation> result = new Dictionary<string, InputInformation>();

            result.Add("ID", new InputInformation() { Type = InputInformationType.integer, Readonly = true });
            result.Add("Volume", new InputInformation() { Type = InputInformationType.integer, Required = true });
            result.Add("Start", new InputInformation() { Type = InputInformationType.datetime, Required = true });
            result.Add("End", new InputInformation() { Type = InputInformationType.datetime, Required = true });
            result.Add("Image", new InputInformation() { Type = InputInformationType.image });

            result.Add("EventType", new InputInformation() { Type = InputInformationType.reference, Reference = "EventType", ReferenceForm = Form.GetReferenceForm(BackendEventTypeViewModelItem.GetForm()) });

            return result;
        }
    }
}