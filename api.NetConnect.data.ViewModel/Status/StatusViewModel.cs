using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Status
{
    public class StatusViewModel : BaseViewModel
    {
        public StatusViewModelItem Data { get; set; }

        public StatusViewModel(String Titel, String Text)
        {
            Data = new StatusViewModelItem(Titel, Text);
        }
    }

    public class StatusViewModelItem
    {
        public String Titel { get; set; }
        public String Text { get; set; }

        public StatusViewModelItem(String Titel, String Text)
        {
            this.Titel = Titel;
            this.Text = Text;
        }
    }
}
