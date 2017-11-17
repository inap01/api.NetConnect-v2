using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Tournament.Backend
{
    public class BackendTournamentViewModel : BackendBaseViewModel
    {
        public BackendTournamentViewModelItem Data { get; set; }

        public BackendTournamentViewModel()
        {
            Data = new BackendTournamentViewModelItem();

            Form = GetForm();
        }

        public override Dictionary<string, InputInformation> GetForm()
        {
            return BackendTournamentViewModelItem.GetForm();
        }
    }

    public class BackendTournamentViewModelItem : BackendBaseViewModelItem
    {
        public String ChallongeLink { get; set; }
        public String Mode { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public Int32 ParticipantCount { get; set; }
        public Int32 TeamSize { get; set; }
        public BackendTournamentGameViewModelItem Game { get; set; }
        public List<BackendTournamentParticipantViewModelItem> Player { get; set; }
        public List<BackendTournamentTeamViewModelItem> Teams { get; set; }
        public BackendTournamentPartner Partner { get; set; }

        public BackendTournamentViewModelItem()
        {
            Game = new BackendTournamentGameViewModelItem();
            Player = new List<BackendTournamentParticipantViewModelItem>();
            Teams = new List<BackendTournamentTeamViewModelItem>();
        }

        public class BackendTournamentPartner : BaseViewModelItem
        {
            public String Name { get; set; }
            public String ImagePath { get; set; }
        }

        public static Dictionary<string, InputInformation> GetForm()
        {
            Dictionary<string, InputInformation> result = new Dictionary<string, InputInformation>();

            result.Add("ID", new InputInformation() { Type = InputInformationType.integer, Readonly = true });
            result.Add("ChallongeLink", new InputInformation() { Type = InputInformationType.@string });
            result.Add("Mode", new InputInformation() { Type = InputInformationType.@string });
            result.Add("Start", new InputInformation() { Type = InputInformationType.datetime });
            result.Add("End", new InputInformation() { Type = InputInformationType.datetime });

            result.Add("Game", new InputInformation() { Type = InputInformationType.reference, Reference = "Game", ReferenceForm = BackendTournamentGameViewModelItem.GetForm() });
            result.Add("Player", new InputInformation() { Type = InputInformationType.reference, Reference = "Player", ReferenceForm = BackendTournamentParticipantViewModelItem.GetForm() });
            result.Add("Teams", new InputInformation() { Type = InputInformationType.reference, Reference = "Team", ReferenceForm = BackendTournamentTeamViewModelItem.GetForm() });

            return result;
        }
    }

    public class BackendTournamentGameViewModelItem : BackendBaseViewModelItem
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

    public class BackendTournamentTeamViewModelItem : BackendBaseViewModelItem
    {
        public String Name { get; set; }
        public Boolean HasPassword { get; set; }
        public List<BackendTournamentParticipantViewModelItem> Player { get; set; }

        public BackendTournamentTeamViewModelItem()
        {
            Player = new List<BackendTournamentParticipantViewModelItem>();
        }

        public static Dictionary<string, InputInformation> GetForm()
        {
            Dictionary<string, InputInformation> result = new Dictionary<string, InputInformation>();

            result.Add("ID", new InputInformation() { Type = InputInformationType.integer, Readonly = true });
            result.Add("Name", new InputInformation() { Type = InputInformationType.@string, Readonly = true });
            result.Add("HasPassword", new InputInformation() { Type = InputInformationType.boolean, Readonly = true });

            result.Add("Player", new InputInformation() { Type = InputInformationType.reference, Reference = "Player", ReferenceForm = BackendTournamentParticipantViewModelItem.GetForm() });

            return result;
        }
    }

    public class BackendTournamentParticipantViewModelItem : BackendBaseViewModelItem
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }

        public static Dictionary<string, InputInformation> GetForm()
        {
            Dictionary<string, InputInformation> result = new Dictionary<string, InputInformation>();

            result.Add("ID", new InputInformation() { Type = InputInformationType.integer, Readonly = true });
            result.Add("FirstName", new InputInformation() { Type = InputInformationType.@string, Readonly = true });
            result.Add("LastName", new InputInformation() { Type = InputInformationType.@string, Readonly = true });
            result.Add("Nickname", new InputInformation() { Type = InputInformationType.@string, Readonly = true });

            return result;
        }
    }
}