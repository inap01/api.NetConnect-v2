using api.NetConnect.data.ViewModel.Event.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Game.Backend
{
    public class BackendGameViewModel : BackendBaseViewModel
    {
        public BackendGameViewModelItem Data { get; set; }

        public BackendGameViewModel()
        {
            Data = new BackendGameViewModelItem();

            Form = GetForm();
        }

        public override Dictionary<string, InputInformation> GetForm()
        {
            return BackendGameViewModelItem.GetForm();
        }
    }

    public class BackendGameViewModelItem : BackendBaseViewModelItem
    {
        public String Name { get; set; }
        public String ImagePath { get; set; }
        public String RulesPath { get; set; }
        public Boolean RequireBattleTag { get; set; }
        public Boolean RequireSteamID { get; set; }

        public static Dictionary<string, InputInformation> GetForm()
        {
            Dictionary<string, InputInformation> result = new Dictionary<string, InputInformation>();

            result.Add("ID", new InputInformation() { Type = InputInformationType.integer, Readonly = true });
            result.Add("Name", new InputInformation() { Type = InputInformationType.@string, Readonly = true });
            result.Add("ImagePath", new InputInformation() { Type = InputInformationType.@string, Readonly = true });
            result.Add("RulesPath", new InputInformation() { Type = InputInformationType.@string, Readonly = true });
            result.Add("RequireBattleTag", new InputInformation() { Type = InputInformationType.boolean, Readonly = true });
            result.Add("RequireSteamID", new InputInformation() { Type = InputInformationType.boolean, Readonly = true });

            return result;
        }
    }
}