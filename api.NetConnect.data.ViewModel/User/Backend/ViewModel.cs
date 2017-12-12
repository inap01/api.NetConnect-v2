using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.User.Backend
{
    public class BackendUserViewModel : BackendBaseViewModel
    {
        public BackendUserViewModelItem Data { get; set; }

        public BackendUserViewModel()
        {
            Data = new BackendUserViewModelItem();

            Form = GetForm();
        }

        public override Dictionary<string, InputInformation> GetForm()
        {
            return BackendUserViewModelItem.GetForm();
        }
    }

    public class BackendUserViewModelItem : BaseViewModelItem
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }
        public String Image { get; set; }
        public String Email { get; set; }
        public String SteamID { get; set; }
        public String BattleTag { get; set; }
        public Boolean Newsletter { get; set; }

        public static Dictionary<string, InputInformation> GetForm()
        {
            Dictionary<string, InputInformation> result = new Dictionary<string, InputInformation>();

            result.Add("ID", new InputInformation() { Type = InputInformationType.integer, Readonly = true });
            result.Add("FirstName", new InputInformation() { Type = InputInformationType.@string, Required = true });
            result.Add("LastName", new InputInformation() { Type = InputInformationType.@string, Required = true });
            result.Add("Nickname", new InputInformation() { Type = InputInformationType.@string, Required = true });
            result.Add("Image", new InputInformation() { Type = InputInformationType.image });
            result.Add("Email", new InputInformation() { Type = InputInformationType.@string, Required = true });
            result.Add("SteamID", new InputInformation() { Type = InputInformationType.@string });
            result.Add("BattleTag", new InputInformation() { Type = InputInformationType.@string });
            result.Add("Newsletter", new InputInformation() { Type = InputInformationType.boolean });

            return result;
        }
    }
}