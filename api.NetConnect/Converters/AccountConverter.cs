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
        public static AccountReservationViewModelItem FromModel(this AccountReservationViewModelItem viewmodel, User model)
        {
            EventDataController eventDataCtrl = new EventDataController();
            SeatTransferLogDataController transferDataCtrl = new SeatTransferLogDataController();

            viewmodel.Name = $"{model.FirstName} {model.LastName}";
            viewmodel.Image = Properties.Settings.Default.imageAbsolutePath + "team/no_image.png"; // TODO
            viewmodel.Events = eventDataCtrl.GetItems().Where(x => x.End > DateTime.Now).ToList().ConvertAll(x => {
                var vm = new AccountReservationEventViewModelItem();
                vm.FromModel(x, model);
                return vm;
            });
            viewmodel.BankAccountData.FromProperties();

            viewmodel.TransferLog.Add(new AccountReservationSeatTransferLogViewModelItem() { Date = DateTime.Now.AddDays(-3), Text = "asdfghjkl" });
            viewmodel.TransferLog.Add(new AccountReservationSeatTransferLogViewModelItem() { Date = DateTime.Now.AddDays(-2), Text = "asdfghjkl" });
            viewmodel.TransferLog.Add(new AccountReservationSeatTransferLogViewModelItem() { Date = DateTime.Now.AddDays(-1), Text = "asdfghjkl" });

            viewmodel.TransferLog.AddRange(transferDataCtrl.GetItems().Where(x => x.DestinationUser.ID == model.ID).ToList().ConvertAll(x =>
            {
                return SendTicketString(x);
            }));
            viewmodel.TransferLog.AddRange(transferDataCtrl.GetItems().Where(x => x.SourceUser.ID == model.ID).ToList().ConvertAll(x =>
            {
                return RecivedTicketString(x);
            }));
            viewmodel.TransferLog = viewmodel.TransferLog.OrderByDescending(x => x.Date).ToList();

            return viewmodel;
        }

        public static AccountReservationEventViewModelItem FromModel(this AccountReservationEventViewModelItem viewmodel, Event model, User user)
        {
            SeatDataController seatDataCtrl = new SeatDataController();

            viewmodel.ID = model.ID;
            viewmodel.Name = model.EventType.Name + " Vol." + model.Volume;
            viewmodel.Seats = seatDataCtrl.GetItems().Where(x => x.EventID == model.ID && x.UserID == user.ID).ToList().ConvertAll(x => {
                var vm = new AccountReservationSeatViewModelItem();
                vm.FromModel(x);
                return vm;
            });
            viewmodel.TransferedSeats = seatDataCtrl.GetItems().Where(x => x.EventID == model.ID && x.TransferUserID == user.ID).ToList().ConvertAll(x => {
                var vm = new AccountReservationTransferedSeatViewModelItem();
                vm.FromModel(x);
                return vm;
            });

            return viewmodel;
        }

        public static AccountReservationSeatViewModelItem FromModel(this AccountReservationSeatViewModelItem viewmodel, Seat model)
        {
            viewmodel.ID = model.ID;
            viewmodel.SeatNumber = model.SeatNumber;
            viewmodel.State = model.State;
            if (model.TransferUser != null)
                viewmodel.TransferUser = new UserViewModelItem().FromModel(model.TransferUser);

            return viewmodel;
        }

        public static AccountReservationTransferedSeatViewModelItem FromModel(this AccountReservationTransferedSeatViewModelItem viewmodel, Seat model)
        {
            viewmodel.ID = model.ID;
            viewmodel.SeatNumber = model.SeatNumber;
            viewmodel.State = model.State;
            if (model.TransferUser != null)
                viewmodel.From = new UserViewModelItem().FromModel(model.User);

            return viewmodel;
        }

        public static BankAccountData FromProperties(this BankAccountData viewmodel)
        {
            viewmodel.BankAccountOwner = Properties.Settings.Default.BankAccountOwner;
            viewmodel.IBAN = Properties.Settings.Default.IBAN;
            viewmodel.BLZ = Properties.Settings.Default.BLZ;
            viewmodel.BankAccountNumber = Properties.Settings.Default.BankAccountNumber;
            viewmodel.BIC = Properties.Settings.Default.BIC;

            return viewmodel;
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
        public static AccountTournamentViewModelItem FromModel(this AccountTournamentViewModelItem viewmodel, User model)
        {
            TournamentParticipantDataController participantDataCtrl = new TournamentParticipantDataController();

            viewmodel.Name = $"{model.FirstName} {model.LastName}";
            viewmodel.Image = Properties.Settings.Default.imageAbsolutePath + "team/no_image.png"; // TODO

            viewmodel.TournamentParticipation.AddRange(participantDataCtrl.GetItems().Where(x => x.User.ID == model.ID && x.Tournament.Event.End > DateTime.Now).ToList().ConvertAll(x =>
            {
                return new AccountTournamentParticipantViewModelItem().FromModel(x.Tournament);
            }));

            return viewmodel;
        }

        public static AccountTournamentParticipantViewModelItem FromModel(this AccountTournamentParticipantViewModelItem viewmodel, Tournament model)
        {
            viewmodel.ID = model.ID;
            viewmodel.Mode = model.Mode;
            viewmodel.Start = model.Start;
            viewmodel.GameTitle = model.TournamentGame.Name;
            viewmodel.Event.FromModel(model.Event);

            return viewmodel;
        }
        #endregion

        #region Edit
        public static AccountEditViewModelItem FromModel(this AccountEditViewModelItem viewmodel, User model)
        {
            viewmodel.ID = model.ID;
            viewmodel.FirstName = model.FirstName;
            viewmodel.LastName = model.LastName;
            viewmodel.Nickname = model.Nickname;
            viewmodel.Image = Properties.Settings.Default.imageAbsolutePath + "team/no_image.png"; // TODO
            viewmodel.Email = model.Email;
            viewmodel.SteamID = model.SteamID;
            viewmodel.BattleTag = model.BattleTag;
            viewmodel.Newsletter = model.Newsletter;
            viewmodel.OldPassword = null;
            viewmodel.NewPassword1 = null;
            viewmodel.NewPassword2 = null;

            return viewmodel;
        }

        public static User ToModel(this User model, AccountEditViewModelItem viewmodel)
        {
            model.FirstName = viewmodel.FirstName;
            model.LastName = viewmodel.LastName;
            model.Nickname = viewmodel.Nickname;
            model.Email = viewmodel.Email;
            model.SteamID = viewmodel.SteamID;
            model.BattleTag = viewmodel.BattleTag;

            return model;
        }
        #endregion
    }
}