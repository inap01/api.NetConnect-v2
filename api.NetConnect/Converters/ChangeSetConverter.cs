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
        public static void FromModel(this ChangeSetViewModelItem viewmodel, ChangeSet model)
        {
            viewmodel.CateringOrder = model.CateringOrder;
            viewmodel.CateringProduct = model.CateringOrderDetail;
            viewmodel.CateringOrderDetail = model.CateringProduct;
            viewmodel.Partner = model.Partner;
            viewmodel.PartnerPack = model.PartnerPack;
            viewmodel.Seat = model.Seat;
            viewmodel.Tournament = model.Tournament;
            viewmodel.TournamentGame = model.TournamentGame;
            viewmodel.TournamentParticipant = model.TournamentParticipant;
            viewmodel.TournamentTeam = model.TournamentTeam;
            viewmodel.User = model.User;
        }
    }
}