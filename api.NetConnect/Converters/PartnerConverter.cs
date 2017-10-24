using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using api.NetConnect.data.ViewModel.Partner;
using api.NetConnect.data.ViewModel.Partner.Backend;

namespace api.NetConnect.Converters
{
    public static partial class ConverterExtensions
    {
        #region Frontend
        public static void FromModel(this PartnerViewModelItem viewModel, Partner model)
        {
            viewModel.Name = model.Name;
            viewModel.Description = model.Content;
            viewModel.Image = model.ImageContainer.OriginalPath;
            viewModel.Link = model.Link;
            viewModel.RefLink = model.Link;

            viewModel.PartnerType = new PartnerType()
            {
                Name = model.PartnerPack.Name
            };

            // TODO Nachfolgendes
            string[] displays = { "Header", "Footer" };
            foreach (var display in displays)
                viewModel.Display.Add(display, true);
        }

        public static void FromViewModel(this Partner model, PartnerViewModelItem viewModel)
        {

        }
        #endregion
        #region Backend
        public static void FromModel(this BackendPartnerViewModelItem viewModel, Partner model)
        {
            viewModel.Name = model.Name;
            viewModel.Description = model.Content;
            viewModel.Link = model.Link;
            viewModel.RefLink = model.Link;

            viewModel.PartnerType = new PartnerType()
            {
                Name = model.PartnerPack.Name
            };

            // TODO Nachfolgendes
            string[] displays = { "Header", "Footer" };
            foreach (var display in displays)
                viewModel.Display.Add(display, true);
        }

        public static void FromViewModel(this Partner model, BackendPartnerViewModelItem viewModel)
        {

        }
        #endregion
    }
}