using api.NetConnect.data.ViewModel.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static api.NetConnect.data.ViewModel.Tournament.TournamentViewModelItem;

namespace api.NetConnect.data.ViewModel.Account
{
    #region Reservation
    public class AccountReservationViewModel : BaseViewModel
    {
        public AccountReservationViewModelItem Data { get; set; }

        public AccountReservationViewModel()
        {
            Data = new AccountReservationViewModelItem();
        }
    }

    public class AccountReservationViewModelItem
    {
        public String Name { get; set; }
        public String Image { get; set; }
        public List<AccountReservationEventViewModelItem> Events { get; set; }
        public List<AccountReservationSeatTransferLogViewModelItem> TransferLog { get; set; }
        public BackAccountData BankAccountData { get; set; }

        public AccountReservationViewModelItem()
        {
            Events = new List<AccountReservationEventViewModelItem>();
            TransferLog = new List<AccountReservationSeatTransferLogViewModelItem>();
            BankAccountData = new BackAccountData();
        }
    }

    public class AccountReservationEventViewModelItem : BaseViewModelItem
    {
        public String Name { get; set; }
        public List<AccountReservationSeatViewModelItem> Seats { get; set; }

        public AccountReservationEventViewModelItem()
        {
            Seats = new List<AccountReservationSeatViewModelItem>();
        }
    }
    
    public class AccountReservationSeatViewModelItem : BaseViewModelItem
    {
        public Int32 SeatNumber { get; set; }
        public Int32 State { get; set; }
    }

    public class AccountReservationSeatTransferLogViewModelItem
    {
        public DateTime Date { get; set; }
        public String Text { get; set; }
    }

    public class BackAccountData
    {
        public String BankAccountOwner { get; set; }
        public String IBAN { get; set; }
        public String BLZ { get; set; }
        public String BankAccountNumber { get; set; }
        public String BIC { get; set; }
    }
    #endregion

    #region Tournament
    public class AccountTournamentViewModel : BaseViewModel
    {
        public AccountTournamentViewModelItem Data { get; set; }

        public AccountTournamentViewModel()
        {
            Data = new AccountTournamentViewModelItem();
        }
    }

    public class AccountTournamentViewModelItem
    {
        public String Name { get; set; }
        public String Image { get; set; }
        public List<AccountTournamentParticipantViewModelItem> TournamentParticipation { get; set; }

        public AccountTournamentViewModelItem()
        {
            TournamentParticipation = new List<AccountTournamentParticipantViewModelItem>();
        }
    }

    public class AccountTournamentParticipantViewModelItem : BaseViewModelItem
    {
        public String Mode { get; set; }
        public DateTime Start { get; set; }
        public String GameTitle { get; set; }
        public EventViewModelItem Event { get; set; }

        public AccountTournamentParticipantViewModelItem()
        {
            Event = new EventViewModelItem();
        }
    }
    #endregion

    #region Edit
    public class AccountEditViewModel : BaseViewModel
    {
        public AccountEditViewModelItem Data { get; set; }

        public AccountEditViewModel()
        {
            Data = new AccountEditViewModelItem();
        }
    }

    public class AccountEditViewModelItem : BaseViewModelItem
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }
        public String Email { get; set; }
        public String Image { get; set; }
        public String SteamID { get; set; }
        public String BattleTag { get; set; }
        public Boolean Newsletter { get; set; }
        public String OldPassword { get; set; }
        public String NewPassword1 { get; set; }
        public String NewPassword2 { get; set; }

        public AccountEditViewModelItem()
        {
        }
    }
    #endregion
}