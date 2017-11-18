using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Event.Backend
{
    public class BackendEventViewModel : BackendBaseViewModel
    {
        public BackendEventViewModelItem Data { get; set; }

        public BackendEventViewModel()
        {
            Data = new BackendEventViewModelItem();

            Form = GetForm();
        }

        public override Dictionary<string, InputInformation> GetForm()
        {
            Dictionary<string, InputInformation> result = new Dictionary<string, InputInformation>();

            return result;
        }
    }

    public class BackendEventViewModelItem : BaseViewModelItem
    {
        public String Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public String Image { get; set; }
        public String Text { get; set; }

        public static Dictionary<string, InputInformation> GetForm()
        {
            Dictionary<string, InputInformation> result = new Dictionary<string, InputInformation>();

            result.Add("ID", new InputInformation() { Type = InputInformationType.integer, Readonly = true });
            result.Add("Title", new InputInformation() { Type = InputInformationType.@string });
            result.Add("Start", new InputInformation() { Type = InputInformationType.datetime });
            result.Add("End", new InputInformation() { Type = InputInformationType.datetime });
            result.Add("Image", new InputInformation() { Type = InputInformationType.image });
            result.Add("Text", new InputInformation() { Type = InputInformationType.text });

            return result;
        }
    }
}