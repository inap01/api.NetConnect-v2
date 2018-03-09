using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel.Event.Backend;
using api.NetConnect.data.ViewModel.User.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Catering.Backend
{
    #region Catering
    public class BackendCateringListArgs : ListArgsRequest<BackendCateringFilter>
    {
        public BackendCateringListArgs() : base()
        {

        }
    }

    public class BackendCateringListViewModel : ListArgsViewModel<BackendCateringViewModelItem, BackendCateringFilter>
    {
        public List<BackendCateringProductItem> ProductOptions { get; set; }

        public BackendCateringListViewModel() : base()
        {
            ProductOptions = new List<BackendCateringProductItem>();
        }
    }

    public class BackendCateringViewModel : BackendBaseViewModel
    {
        public BackendCateringViewModelItem Data { get; set; }
        public List<BackendEventViewModelItem> EventOptions { get; set; }
        public List<BackendCateringStatusOption> StatusOptions { get; set; }
        public List<BackendUserViewModelItem> UserOptions { get; set; }

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
            UserOptions = new List<BackendUserViewModelItem>();

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
        public BackendUserViewModelItem User { get; set; }
        public List<BackendCateringOrderItem> Order { get; set; }
        public DateTime Registered { get; set; }
        public Int32 SeatNumber { get; set; }
        public BackendCateringStatusOption Status { get; set; }
        public String Note { get; set; }

        public BackendCateringViewModelItem()
        {
            Event = new BackendEventViewModelItem();
            User = new BackendUserViewModelItem();
            Order = new List<BackendCateringOrderItem>();
            Status = new BackendCateringStatusOption(1);
        }

        public static Dictionary<string, InputInformation> GetForm()
        {
            Dictionary<string, InputInformation> result = new Dictionary<string, InputInformation>();

            result.Add("ID", new InputInformation() { Type = InputInformationType.integer, Readonly = true });
            result.Add("Status", new InputInformation() { Type = InputInformationType.choice, Required = true });
            result.Add("SeatNumber", new InputInformation() { Type = InputInformationType.integer, Required = true, Readonly = true });
            result.Add("Event", new InputInformation() { Type = InputInformationType.reference, Reference = "Event", ReferenceForm = Form.GetReferenceForm(BackendEventViewModelItem.GetForm()), Required = true, Readonly = true });
            result.Add("User", new InputInformation() { Type = InputInformationType.reference, Reference = "User", ReferenceForm = Form.GetReferenceForm(BackendUserViewModelItem.GetForm()), Required = true, Readonly = true });
            result.Add("Note", new InputInformation() { Type = InputInformationType.text });
            result.Add("Registered", new InputInformation() { Type = InputInformationType.datetime, Readonly = true });

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

    public class BackendCateringProductItem : BackendBaseViewModelItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<ProductAttributeViewModelItem> Attributes { get; set; }
        public bool SingleChoice { get; set; }
    }
    #endregion
    #region Product
    public class BackendProductListArgs : ListArgsRequest<BackendProductFilter>
    {
        public BackendProductListArgs() : base()
        {

        }
    }

    public class BackendProductListViewModel : ListArgsViewModel<BackendProductViewModelItem, BackendProductFilter>
    {
        public BackendProductListViewModel() : base()
        {

        }
    }

    public class BackendProductViewModel : BackendBaseViewModel
    {
        public BackendProductViewModelItem Data { get; set; }
        public List<BackendProductAttributeViewModelItem> AttributeOptions { get; set; }

        public BackendProductViewModel()
        {
            Data = new BackendProductViewModelItem();
            AttributeOptions = new List<BackendProductAttributeViewModelItem>();

            Form = GetForm();
        }

        public override Dictionary<string, InputInformation> GetForm()
        {
            return BackendProductViewModelItem.GetForm();
        }
    }

    public class BackendProductViewModelItem : BackendBaseViewModelItem
    {
        public String Name { get; set; }
        public String Image { get; set; }
        public Decimal Price { get; set; }
        public Boolean SingleChoice { get; set; }
        public Boolean IsActive { get; set; }
        public List<BackendProductAttributeViewModelItem> Attributes { get; set; }

        public BackendProductViewModelItem()
        {
            Attributes = new List<BackendProductAttributeViewModelItem>();
        }

        public static Dictionary<string, InputInformation> GetForm()
        {
            Dictionary<string, InputInformation> result = new Dictionary<string, InputInformation>();

            result.Add("ID", new InputInformation() { Type = InputInformationType.integer, Readonly = true });
            result.Add("Name", new InputInformation() { Type = InputInformationType.@string, Required = true });
            result.Add("Image", new InputInformation() { Type = InputInformationType.@string, Required = true });
            result.Add("Price", new InputInformation() { Type = InputInformationType.@decimal, Required = true });
            result.Add("SingleChoice", new InputInformation() { Type = InputInformationType.boolean, Required = true });
            result.Add("IsActive", new InputInformation() { Type = InputInformationType.boolean, Required = true });

            return result;
        }
    }
    #endregion
    #region Attributes
    public class BackendProductAttributeListArgs : ListArgsRequest<BackendProductAttributeFilter>
    {
        public BackendProductAttributeListArgs() : base()
        {

        }
    }

    public class BackendProductAttributeListViewModel : ListArgsViewModel<BackendProductAttributeViewModelItem, BackendProductAttributeFilter>
    {
        public BackendProductAttributeListViewModel() : base()
        {

        }
    }

    public class BackendProductAttributeViewModel : BackendBaseViewModel
    {
        public BackendProductAttributeViewModelItem Data { get; set; }

        public BackendProductAttributeViewModel()
        {
            Data = new BackendProductAttributeViewModelItem();

            Form = GetForm();
        }

        public override Dictionary<string, InputInformation> GetForm()
        {
            return BackendProductAttributeViewModelItem.GetForm();
        }
    }
    public class BackendProductAttributeViewModelItem : BaseViewModelItem
    {
        public String Name { get; set; }

        public static Dictionary<string, InputInformation> GetForm()
        {
            Dictionary<string, InputInformation> result = new Dictionary<string, InputInformation>();

            result.Add("ID", new InputInformation() { Type = InputInformationType.integer, Readonly = true });
            result.Add("Name", new InputInformation() { Type = InputInformationType.@string, Required = true });

            return result;
        }
    }
    #endregion
}