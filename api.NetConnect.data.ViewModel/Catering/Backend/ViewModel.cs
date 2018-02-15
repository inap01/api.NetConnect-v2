using api.NetConnect.data.ViewModel.Event.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Catering.Backend
{
    public class BackendCateringListArgs : ListArgsRequest<BackendCateringFilter>
    {
        public BackendCateringListArgs() : base()
        {

        }
    }

    public class BackendCateringListViewModel : ListArgsViewModel<BackendCateringViewModelItem, BackendCateringFilter>
    {
        public BackendCateringListViewModel() : base()
        {

        }
    }

    public class BackendCateringViewModel : BackendBaseViewModel
    {
        public BackendCateringViewModelItem Data { get; set; }
        public List<BackendEventViewModelItem> EventOptions { get; set; }
        public List<BackendCateringStatusOption> StatusOptions { get; set; }

        public BackendCateringViewModel()
        {
            Data = new BackendCateringViewModelItem();
            EventOptions = new List<BackendEventViewModelItem>();
            StatusOptions = new List<BackendCateringStatusOption>()
            {
                new BackendCateringStatusOption(0),
                new BackendCateringStatusOption(1),
                new BackendCateringStatusOption(2),
                new BackendCateringStatusOption(-1)
            };

            Form = GetForm();
        }

        public override Dictionary<string, InputInformation> GetForm()
        {
            return BackendCateringViewModelItem.GetForm();
        }
    }

    public class BackendCateringViewModelItem : BackendBaseViewModelItem
    {
        public BackendEventViewModelItem Event { get; set; }
        public BackendCateringSeat User { get; set; }
        public List<BackendCateringOrderItem> Order { get; set; }
        public BackendCateringStatusOption Status { get; set; }

        public BackendCateringViewModelItem()
        {
            Event = new BackendEventViewModelItem();
            User = new BackendCateringSeat();
            Order = new List<BackendCateringOrderItem>();
            Status = new BackendCateringStatusOption(1);
        }

        public static Dictionary<string, InputInformation> GetForm()
        {
            Dictionary<string, InputInformation> result = new Dictionary<string, InputInformation>();

            result.Add("ID", new InputInformation() { Type = InputInformationType.integer, Readonly = true });
            result.Add("Status", new InputInformation() { Type = InputInformationType.choice, Required = true });
            result.Add("Event", new InputInformation() { Type = InputInformationType.reference, Reference = "Event", ReferenceForm = Form.GetReferenceForm(BackendEventViewModelItem.GetForm()), Readonly = true });

            return result;
        }
    }

    public class BackendCateringStatusOption
    {
        public Int32 Key { get; set; }
        public String Text { get; set; }

        public BackendCateringStatusOption(Int32 Status)
        {
            Key = Status;
            switch(Status)
            {
                case 0:
                    Text = "Neu";
                    break;
                case 1:
                    Text = "Bezahlt";
                    break;
                case 2:
                    Text = "Fertig";
                    break;
                case -1:
                    Text = "Storniert";
                    break;
            }
        }
    }

    public class BackendCateringSeat
    {
        public String Name { get; set; }
        public Int32 SeatNumber { get; set; }
    }

    public class BackendCateringOrderItem : BackendBaseViewModelItem
    {
        public String Name { get; set; }
        public List<String> Attributes { get; set; }
        public Int32 Amount { get; set; }
        public Decimal Price { get; set; }
    }
}