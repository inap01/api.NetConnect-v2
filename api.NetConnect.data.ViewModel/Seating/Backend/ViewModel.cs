using api.NetConnect.data.ViewModel.Event.Backend;
using api.NetConnect.data.ViewModel.User.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Seating.Backend
{
    public class BackendSeatingListViewModel : ListArgsViewModel<BackendSeatingViewModelItem, BackendSeatingFilter>
    {

        public BackendSeatingListViewModel() : base()
        {

        }
    }

    public class BackendSeatingViewModel : BackendBaseViewModel
    {
        public BackendSeatingViewModelItem Data { get; set; }
        public List<BackendUserViewModelItem> UserOptions { get; set; }
        public List<BackendSeatingStatusOption> StatusOptions { get; set; }

        public BackendSeatingViewModel()
        {
            Data = new BackendSeatingViewModelItem();
            UserOptions = new List<BackendUserViewModelItem>();
            StatusOptions = new List<BackendSeatingStatusOption>()
            {
                new BackendSeatingStatusOption(0),
                new BackendSeatingStatusOption(1),
                new BackendSeatingStatusOption(2),
                new BackendSeatingStatusOption(3),
                new BackendSeatingStatusOption(-1)
            };

            Form = GetForm();
        }

        public override Dictionary<string, InputInformation> GetForm()
        {
            return BackendSeatingViewModelItem.GetForm();
        }
    }

    public class BackendSeatingViewModelItem : BaseViewModelItem
    {
        public Int32 SeatNumber { get; set; }
        public BackendSeatingStatusOption ReservationState { get; set; }
        public DateTime ReservationDate { get; set; }
        public String Description { get; set; }
        public Boolean IsPayed { get; set; }
        public BackendUserViewModelItem User { get; set; }
        public BackendUserViewModelItem TransferUser { get; set; }
        public BackendEventViewModelItem Event { get; set; }

        public BackendSeatingViewModelItem()
        {
            User = new BackendUserViewModelItem();
            Event = new BackendEventViewModelItem();
        }

        public static Dictionary<string, InputInformation> GetForm()
        {
            Dictionary<string, InputInformation> result = new Dictionary<string, InputInformation>();

            result.Add("ID", new InputInformation() { Type = InputInformationType.integer, Readonly = true });
            result.Add("ReservationDate", new InputInformation() { Type = InputInformationType.datetime, Required = true });
            result.Add("ReservationState", new InputInformation() { Type = InputInformationType.choice, Required = true });
            result.Add("Description", new InputInformation() { Type = InputInformationType.@string });
            result.Add("IsPayed", new InputInformation() { Type = InputInformationType.boolean });

            result.Add("User", new InputInformation() { Type = InputInformationType.reference, Reference = "User", ReferenceForm = Form.GetReferenceForm(BackendUserViewModelItem.GetForm()), Required = true });
            result.Add("TransferUser", new InputInformation() { Type = InputInformationType.reference, Reference = "TransferUser", ReferenceForm = Form.GetReferenceForm(BackendUserViewModelItem.GetForm()) });
            result.Add("Event", new InputInformation() { Type = InputInformationType.reference, Reference = "Event", ReferenceForm = Form.GetReferenceForm(BackendEventViewModelItem.GetForm()), Required = true, Readonly = true });

            return result;
        }
    }

    public class BackendSeatingStatusOption
    {
        public Int32 Key { get; set; }
        public String Text { get; set; }

        public BackendSeatingStatusOption(Int32 Status)
        {
            Key = Status;
            switch (Status)
            {
                case 0:
                    Text = "Frei";
                    break;
                case 1:
                    Text = "Vorgemerkt";
                    break;
                case 2:
                    Text = "Reserviert";
                    break;
                case 3:
                    Text = "NetConnect";
                    break;
                case -1:
                    Text = "Gesperrt";
                    break;
            }
        }
    }
}