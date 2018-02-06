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
        public String Name { get; set; }
        public Int32 Volume { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public String Image { get; set; }
        public Double ReservationCost { get; set; }
        public Boolean IsActiveReservation { get; set; }
        public Boolean IsActiveCatering { get; set; }
        public Boolean IsActiveFeedback { get; set; }
        public Boolean IsPrivate { get; set; }
        public String FeedbackLink { get; set; }
        public String District { get; set; }
        public String Street { get; set; }
        public String Housenumber { get; set; }
        public String Postcode { get; set; }
        public String City { get; set; }
        public BackendEventTypeViewModelItem EventType { get; set; }

        public BackendEventViewModelItem()
        {
            EventType = new BackendEventTypeViewModelItem();
        }

        public static Dictionary<string, InputInformation> GetForm()
        {
            Dictionary<string, InputInformation> result = new Dictionary<string, InputInformation>();

            result.Add("ID", new InputInformation() { Type = InputInformationType.integer, Readonly = true });
            result.Add("Name", new InputInformation() { Type = InputInformationType.@string, Readonly = true });
            result.Add("Volume", new InputInformation() { Type = InputInformationType.integer, Required = true });
            result.Add("Start", new InputInformation() { Type = InputInformationType.datetime, Required = true });
            result.Add("End", new InputInformation() { Type = InputInformationType.datetime, Required = true });
            result.Add("Image", new InputInformation() { Type = InputInformationType.image });
            result.Add("ReservationCost", new InputInformation() { Type = InputInformationType.integer });
            result.Add("IsActiveReservation", new InputInformation() { Type = InputInformationType.boolean });
            result.Add("IsActiveCatering", new InputInformation() { Type = InputInformationType.boolean });
            result.Add("IsActiveFeedback", new InputInformation() { Type = InputInformationType.boolean });
            result.Add("IsPrivate", new InputInformation() { Type = InputInformationType.boolean });
            result.Add("FeedbackLink", new InputInformation() { Type = InputInformationType.@string });

            result.Add("District", new InputInformation() { Type = InputInformationType.@string, Required = true });
            result.Add("Street", new InputInformation() { Type = InputInformationType.@string, Required = true });
            result.Add("Housenumber", new InputInformation() { Type = InputInformationType.@string, Required = true });
            result.Add("Postcode", new InputInformation() { Type = InputInformationType.@string, Required = true });
            result.Add("City", new InputInformation() { Type = InputInformationType.@string, Required = true });

            result.Add("EventType", new InputInformation() { Type = InputInformationType.reference, Reference = "EventType", ReferenceForm = Form.GetReferenceForm(BackendEventTypeViewModelItem.GetForm()) });

            return result;
        }
    }
}