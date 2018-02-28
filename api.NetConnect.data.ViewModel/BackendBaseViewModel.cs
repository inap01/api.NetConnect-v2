using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel
{
    public abstract class BackendBaseViewModel : BaseViewModel
    {
        public Dictionary<String, InputInformation> Form { get; set; }

        public BackendBaseViewModel() : base()
        {
            Form = new Dictionary<String, InputInformation>();
        }

        public abstract Dictionary<String, InputInformation> GetForm();
    }

    public abstract class BackendBaseViewModelItem : BaseViewModelItem
    {

    }

    public class InputInformation
    {
        public InputInformationType Type { get; set; }
        public Boolean Readonly { get; set; }
        public Boolean Required { get; set; }
        public String Reference { get; set; }
        public Dictionary<String, InputInformation> ReferenceForm { get; set; }

        public InputInformation()
        {
            Readonly = false;
            Required = false;
        }
    }

    public enum InputInformationType { @string, text, boolean, @integer, date, datetime, reference, referenceButton, choice }
}
