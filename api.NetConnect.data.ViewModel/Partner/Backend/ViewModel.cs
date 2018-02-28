using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Partner.Backend
{
    public class BackendPartnerListArgs : ListArgsRequest<BackendPartnerFilter>
    {

    }

    public class BackendPartnerListViewModel : ListArgsViewModel<BackendPartnerViewModelItem, BackendPartnerFilter>
    {

    }

    public class BackendPartnerViewModel : BackendBaseViewModel
    {
        public BackendPartnerViewModelItem Data { get; set; }
        public List<BackendPartnerType> PartnerTypeOptions { get; set; }

        public BackendPartnerViewModel()
        {
            Data = new BackendPartnerViewModelItem();
            PartnerTypeOptions = new List<BackendPartnerType>();

            Form = GetForm();
        }

        public override Dictionary<string, InputInformation> GetForm()
        {
            return BackendPartnerViewModelItem.GetForm();
        }
    }

    public class BackendPartnerViewModelItem : BackendBaseViewModelItem
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public String OriginalImage { get; set; }
        public String PassiveImage { get; set; }
        public String Link { get; set; }
        public String RefLink { get; set; }
        public BackendPartnerType PartnerTypeSelected { get; set; }
        public List<PartnerDisplay> Display { get; set; }
        public Boolean IsActive { get; set; }

        public BackendPartnerViewModelItem()
        {
            PartnerTypeSelected = new BackendPartnerType();
            Display = new List<PartnerDisplay>();
        }

        public static Dictionary<string, InputInformation> GetForm()
        {
            Dictionary<string, InputInformation> result = new Dictionary<string, InputInformation>();

            result.Add("ID", new InputInformation() { Type = InputInformationType.integer, Required = true, Readonly = true });
            result.Add("Name", new InputInformation() { Type = InputInformationType.@string, Required = true, });
            result.Add("Description", new InputInformation() { Type = InputInformationType.text });
            result.Add("OriginalImage", new InputInformation() { Type = InputInformationType.@string, Required = true });
            result.Add("PassiveImage", new InputInformation() { Type = InputInformationType.@string, Required = true });
            result.Add("Link", new InputInformation() { Type = InputInformationType.@string, Required = true });
            result.Add("RefLink", new InputInformation() { Type = InputInformationType.@string });
            result.Add("IsActive", new InputInformation() { Type = InputInformationType.boolean });

            result.Add("PartnerType", new InputInformation() { Type = InputInformationType.reference, Required = true, Reference = "PartnerType", ReferenceForm = BackendPartnerType.GetForm() });

            return result;
        }
    }

    public class BackendPartnerType : BackendBaseViewModelItem
    {
        public String Name { get; set; }

        public static Dictionary<string, InputInformation> GetForm()
        {
            Dictionary<string, InputInformation> result = new Dictionary<string, InputInformation>();

            result.Add("ID", new InputInformation() { Type = InputInformationType.integer, Required = true, Readonly = true });
            result.Add("Name", new InputInformation() { Type = InputInformationType.@string, Required = true, Readonly = true });

            return result;
        }
    }

    public class BackendPartnerPositionViewModel : BackendBaseViewModel
    {
        public List<BackendPartnerPositionViewModelItem> Data { get; set; }
        public List<String> PartnerTypeOptions { get; set; }

        public BackendPartnerPositionViewModel()
        {
            Data = new List<BackendPartnerPositionViewModelItem>();
        }

        public override Dictionary<string, InputInformation> GetForm()
        {
            throw new NotImplementedException();
        }
    }

    public class BackendPartnerPositionViewModelItem : BaseViewModelItem
    {
        public String Name { get; set; }
        public Int32 Position { get; set; }
    }
}