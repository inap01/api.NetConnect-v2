using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.EventType.Backend
{
    public class BackendEventTypeListArgs : ListArgsRequest<BackendEventTypeFilter>
    {

    }

    public class BackendEventTypeListViewModel : ListArgsViewModel<BackendEventTypeViewModelItem, BackendEventTypeFilter>
    {

    }

    public class BackendEventTypeViewModel : BackendBaseViewModel
    {
        public BackendEventTypeViewModelItem Data { get; set; }

        public BackendEventTypeViewModel()
        {
            Data = new BackendEventTypeViewModelItem();

            Form = GetForm();
        }

        public override Dictionary<string, InputInformation> GetForm()
        {
            return BackendEventTypeViewModelItem.GetForm();
        }
    }

    public class BackendEventTypeViewModelItem : BaseViewModelItem
    {
        public String Name { get; set; }
        public String Description { get; set; }

        public static Dictionary<string, InputInformation> GetForm()
        {
            Dictionary<string, InputInformation> result = new Dictionary<string, InputInformation>();

            result.Add("ID", new InputInformation() { Type = InputInformationType.integer, Readonly = true });
            result.Add("Name", new InputInformation() { Type = InputInformationType.@string, Required = true });
            result.Add("Description", new InputInformation() { Type = InputInformationType.text });

            return result;
        }
    }
}