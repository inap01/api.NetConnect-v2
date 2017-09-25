using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using api.NetConnect.data.ViewModel.ChangeSet;

namespace api.NetConnect.Converters
{
    public static partial class ConverterExtensions
    {
        public static void FromModel(this ChangeSetViewModelItem viewModel, ChangeSet model)
        {
            viewModel.CateringOrder = model.CateringOrder;
            viewModel.CateringProduct = model.CateringOrderDetail;
            viewModel.CateringOrderDetail = model.CateringProduct;
            viewModel.Chat = model.Chat;
            viewModel.Logs = model.Logs;
            viewModel.Partner = model.Partner;
            viewModel.PartnerPack = model.PartnerPack;
            viewModel.Seat = model.Seat;
            viewModel.Settings = model.Settings;
            viewModel.Tournament = model.Tournament;
            viewModel.TournamentGame = model.TournamentGame;
            viewModel.TournamentParticipant = model.TournamentParticipant;
            viewModel.TournamentTeam = model.TournamentTeam;
            viewModel.User = model.User;
        }
    }
}