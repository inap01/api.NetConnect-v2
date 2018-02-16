using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using api.NetConnect.data.ViewModel.Account;
using api.NetConnect.data.ViewModel;
using api.NetConnect.DataControllers;
using api.NetConnect.data.ViewModel.User;

namespace api.NetConnect.Converters
{
    public static partial class ConverterExtensions
    {
        #region Reservation
        public static AccountReservationViewModelItem FromModel(this AccountReservationViewModelItem viewModel, User model)
        {
            EventDataController eventDataCtrl = new EventDataController();
            SeatTransferLogDataController transferDataCtrl = new SeatTransferLogDataController();

            viewModel.Name = $"{model.FirstName} {model.LastName}";
            viewModel.Image = "http://lan-netconnect.de/_api/images/team/no_image.png"; // TODO
            viewModel.Events = eventDataCtrl.GetItems().Where(x => x.End > DateTime.Now).ToList().ConvertAll(x => {
                var vm = new AccountReservationEventViewModelItem();
                vm.FromModel(x, model);
                return vm;
            });
            viewModel.BankAccountData.FromProperties();

            viewModel.TransferLog.Add(new AccountReservationSeatTransferLogViewModelItem() { Date = DateTime.Now.AddDays(-3), Text = "asdfghjkl" });
            viewModel.TransferLog.Add(new AccountReservationSeatTransferLogViewModelItem() { Date = DateTime.Now.AddDays(-2), Text = "asdfghjkl" });
            viewModel.TransferLog.Add(new AccountReservationSeatTransferLogViewModelItem() { Date = DateTime.Now.AddDays(-1), Text = "asdfghjkl" });

            viewModel.TransferLog.AddRange(transferDataCtrl.GetItems().Where(x => x.DestinationUser.ID == model.ID).ToList().ConvertAll(x =>
            {
                return SendTicketString(x);
            }));
            viewModel.TransferLog.AddRange(transferDataCtrl.GetItems().Where(x => x.SourceUser.ID == model.ID).ToList().ConvertAll(x =>
            {
                return RecivedTicketString(x);
            }));
            viewModel.TransferLog = viewModel.TransferLog.OrderByDescending(x => x.Date).ToList();

            return viewModel;
        }

        public static AccountReservationEventViewModelItem FromModel(this AccountReservationEventViewModelItem viewModel, Event model, User user)
        {
            SeatDataController seatDataCtrl = new SeatDataController();

            viewModel.ID = model.ID;
            viewModel.Name = model.EventType.Name + " Vol." + model.Volume;
            viewModel.Seats = seatDataCtrl.GetItems().Where(x => x.EventID == model.ID && x.UserID == user.ID).ToList().ConvertAll(x => {
                var vm = new AccountReservationSeatViewModelItem();
                vm.FromModel(x);
                return vm;
            });
            viewModel.TransferedSeats = seatDataCtrl.GetItems().Where(x => x.EventID == model.ID && x.TransferUserID == user.ID).ToList().ConvertAll(x => {
                var vm = new AccountReservationTransferedSeatViewModelItem();
                vm.FromModel(x);
                return vm;
            });

            return viewModel;
        }

        public static AccountReservationSeatViewModelItem FromModel(this AccountReservationSeatViewModelItem viewModel, Seat model)
        {
            viewModel.ID = model.ID;
            viewModel.SeatNumber = model.SeatNumber;
            viewModel.State = model.State;
            if (model.TransferUser != null)
                viewModel.TransferUser = new UserViewModelItem().FromModel(model.TransferUser);

            return viewModel;
        }

        public static AccountReservationTransferedSeatViewModelItem FromModel(this AccountReservationTransferedSeatViewModelItem viewModel, Seat model)
        {
            viewModel.ID = model.ID;
            viewModel.SeatNumber = model.SeatNumber;
            viewModel.State = model.State;
            if (model.TransferUser != null)
                viewModel.From = new UserViewModelItem().FromModel(model.User);

            return viewModel;
        }

        public static BankAccountData FromProperties(this BankAccountData viewModel)
        {
            viewModel.BankAccountOwner = Properties.Settings.Default.BankAccountOwner;
            viewModel.IBAN = Properties.Settings.Default.IBAN;
            viewModel.BLZ = Properties.Settings.Default.BLZ;
            viewModel.BankAccountNumber = Properties.Settings.Default.BankAccountNumber;
            viewModel.BIC = Properties.Settings.Default.BIC;

            return viewModel;
        }

        private static AccountReservationSeatTransferLogViewModelItem SendTicketString(SeatTransferLog LogEntry)
        {
            AccountReservationSeatTransferLogViewModelItem item = new AccountReservationSeatTransferLogViewModelItem();

            item.Date = LogEntry.TransferDate;
            item.Text = $"Du hast Platz #{LogEntry.SeatID} an {LogEntry.DestinationUser.FirstName} {LogEntry.DestinationUser.LastName} transferiert.";

            return item;
        }

        private static AccountReservationSeatTransferLogViewModelItem RecivedTicketString(SeatTransferLog LogEntry)
        {
            AccountReservationSeatTransferLogViewModelItem item = new AccountReservationSeatTransferLogViewModelItem();

            item.Date = LogEntry.TransferDate;
            item.Text = $"Du hast Platz #{LogEntry.SeatID} von {LogEntry.SourceUser.FirstName} {LogEntry.SourceUser.LastName} erhalten.";

            return item;
        }
        #endregion

        #region Tournament
        public static AccountTournamentViewModelItem FromModel(this AccountTournamentViewModelItem viewModel, User model)
        {
            TournamentParticipantDataController participantDataCtrl = new TournamentParticipantDataController();

            viewModel.Name = $"{model.FirstName} {model.LastName}";
            viewModel.Image = "http://lan-netconnect.de/_api/images/team/no_image.png"; // TODO
            
            viewModel.TournamentParticipation.AddRange(participantDataCtrl.GetItems().Where(x => x.User.ID == model.ID && x.Tournament.Event.End > DateTime.Now).ToList().ConvertAll(x =>
            {
                return new AccountTournamentParticipantViewModelItem().FromModel(x.Tournament);
            }));

            return viewModel;
        }

        public static AccountTournamentParticipantViewModelItem FromModel(this AccountTournamentParticipantViewModelItem viewModel, Tournament model)
        {
            viewModel.ID = model.ID;
            viewModel.Mode = model.Mode;
            viewModel.Start = model.Start;
            viewModel.GameTitle = model.TournamentGame.Name;
            viewModel.Event.FromModel(model.Event);

            return viewModel;
        }
        #endregion

        #region Edit
        public static AccountEditViewModelItem FromModel(this AccountEditViewModelItem viewModel, User model)
        {
            viewModel.ID = model.ID;
            viewModel.FirstName = model.FirstName;
            viewModel.LastName = model.LastName;
            viewModel.Nickname = model.Nickname;
            viewModel.Image = "http://lan-netconnect.de/_api/images/team/no_image.png";
            viewModel.Email = model.Email;
            viewModel.SteamID = model.SteamID;
            viewModel.BattleTag = model.BattleTag;
            viewModel.OldPassword = null;
            viewModel.NewPassword1 = null;
            viewModel.NewPassword2 = null;

            return viewModel;
        }

        public static User ToModel(this User model, AccountEditViewModelItem viewModel)
        {
            model.FirstName = viewModel.FirstName;
            model.LastName = viewModel.LastName;
            model.Nickname = viewModel.Nickname;
            model.Email = viewModel.Email;
            model.SteamID = viewModel.SteamID;
            model.BattleTag = viewModel.BattleTag;

            return model;
        }
        #endregion
    }
}