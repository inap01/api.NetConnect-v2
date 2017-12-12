using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using api.NetConnect.data.ViewModel.Partner;
using api.NetConnect.data.ViewModel.Partner.Backend;
using api.NetConnect.DataControllers;
using api.NetConnect.data.ViewModel;

namespace api.NetConnect.Converters
{
    public static partial class ConverterExtensions
    {
        #region Frontend
        public static PartnerViewModelItem FromModel(this PartnerViewModelItem viewModel, Partner model)
        {
            viewModel.Name = model.Name;
            viewModel.Description = model.Content;
            viewModel.Image = Properties.Settings.Default.imageAbsolutePath + model.ImageContainer.OriginalPath + "/image.png";
            viewModel.Link = model.Link;
            viewModel.RefLink = model.Link;

            viewModel.PartnerType.FromModel(model.PartnerPack);

            // TODO Nachfolgendes
            string[] displays = { "Header", "Footer" };
            foreach (var display in displays)
                viewModel.Display.Add(new data.ViewModel.Partner.PartnerDisplay() { ID = 1, Name = display, Value = true });

            return viewModel;
        }

        public static PartnerType FromModel(this PartnerType viewModel, PartnerPack model)
        {
            viewModel.ID = model.ID;
            viewModel.Name = model.Name;

            return viewModel;
        }
        #endregion
        #region Backend
        public static BackendPartnerViewModelItem FromModel(this BackendPartnerViewModelItem viewModel, Partner model)
        {
            viewModel.ID = model.ID;
            viewModel.Name = model.Name;
            viewModel.Description = model.Content;
            viewModel.Link = model.Link;
            viewModel.RefLink = model.RefLink;
            viewModel.OriginalImage = "http://lan-netconnect.de/_api/images/partner/lioncast/image.png";
            viewModel.PassiveImage = "http://lan-netconnect.de/_api/images/partner/lioncast/image.png";
            viewModel.IsActive = model.IsActive;

            viewModel.PartnerTypeSelected = new BackendPartnerType()
            {
                ID = model.PartnerPack.ID,
                Name = model.PartnerPack.Name
            };
            
            foreach (var display in PartnerDisplayDataController.GetItems())
            {
                if(model.PartnerDisplayRelation.FirstOrDefault(x => x.PartnerID == model.ID && x.PartnerDisplayID == display.ID) != null)
                    viewModel.Display.Add(new data.ViewModel.Partner.PartnerDisplay()
                    {
                        ID = display.ID,
                        Name = display.Name,
                        Value = true
                    });
                else
                    viewModel.Display.Add(new data.ViewModel.Partner.PartnerDisplay()
                    {
                        ID = display.ID,
                        Name = display.Name,
                        Value = false
                    });
            }

            return viewModel;
        }

        public static Partner ToModel(this BackendPartnerViewModelItem viewModel)
        {
            Partner model = new Partner();
            if (viewModel.ID != 0) 
                model = PartnerDataController.GetItem(viewModel.ID);
            
            model.Name = viewModel.Name;
            model.Content = viewModel.Description;
            model.Link = viewModel.Link;
            model.RefLink = viewModel.RefLink;
            //model.Image = viewModel.Image;
            model.IsActive = viewModel.IsActive;

            model.PartnerPack = PartnerPackDataController.GetItem(viewModel.PartnerTypeSelected.ID);

            /*
            foreach (var display in viewModel.Display.Where(x => x.Value))
                model.PartnerDisplayRelation.Add(new PartnerDisplayRelation()
                {
                    PartnerID = viewModel.ID,
                    Partner = PartnerDataController.GetItem(viewModel.ID),
                    PartnerDisplayID = display.ID,
                    PartnerDisplay = PartnerDisplayDataController.GetItem(display.ID)
                });
            */

            return model;
        }

        public static Partner ToModel(this BackendPartnerPositionViewModelItem viewModel)
        {
            Partner model = new Partner();
            if (viewModel.ID != 0)
                model = PartnerDataController.GetItem(viewModel.ID);

            model.Position = viewModel.Position;

            return model;
        }
        #endregion
    }

    public class PartnerConverter
    {
        public static List<BackendPartnerViewModelItem> FilterList(ListArgsRequest<BackendPartnerFilter> args, out Int32 TotalCount)
        {
            List<BackendPartnerViewModelItem> result = new List<BackendPartnerViewModelItem>();

            var partner = PartnerDataController.GetItems();
            List<Partner> items;

            if (args.Filter.StatusSelected != data.ViewModel.StatusFilterEnum.Alle)
            {
                if (args.Filter.StatusSelected == data.ViewModel.StatusFilterEnum.Aktiv)
                    partner = partner.Where(x => x.IsActive).ToList();
                else
                    partner = partner.Where(x => !x.IsActive).ToList();
            }

            if (args.Filter.PartnerTypeSelected != "Alle")
                partner = partner.Where(x => x.PartnerPack.Name == args.Filter.PartnerTypeSelected).ToList();

            items = partner.Where(x => x.Name.ToLower().Contains(args.Filter.Name.ToLower())).ToList();

            TotalCount = items.Count;

            items = items.Skip(args.Pagination.ItemsPerPageSelected * (args.Pagination.Page - 1))
                 .Take(args.Pagination.ItemsPerPageSelected)
                 .ToList();

            foreach (var model in items)
            {
                BackendPartnerViewModelItem item = new BackendPartnerViewModelItem();
                item.FromModel(model);
                result.Add(item);
            }

            return result;
        }
    }
}