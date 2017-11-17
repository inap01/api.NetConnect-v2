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
                viewModel.Display.Add(new data.ViewModel.Partner.PartnerDisplay() { ID = 1, Name = display, Value = true });
        }

        public static void FromViewModel(this Partner model, PartnerViewModelItem viewModel)
        {
            viewModel.ID = model.ID;
            viewModel.Name = model.Name;
            viewModel.Description = model.Content;
            viewModel.Link = model.Link;
            viewModel.RefLink = model.RefLink;
            viewModel.Image = "";

            viewModel.PartnerType = new PartnerType()
            {
                ID = model.PartnerPack.ID,
                Name = model.PartnerPack.Name
            };

            foreach (var displayRelation in model.PartnerDisplayRelation)
            {
                viewModel.Display.Add(new data.ViewModel.Partner.PartnerDisplay()
                {
                    ID = displayRelation.PartnerDisplay.ID,
                    Name = displayRelation.PartnerDisplay.Name,
                    Value = true
                });
            }
        }
        #endregion
        #region Backend
        public static void FromModel(this BackendPartnerViewModelItem viewModel, Partner model)
        {
            viewModel.ID = model.ID;
            viewModel.Name = model.Name;
            viewModel.Description = model.Content;
            viewModel.Link = model.Link;
            viewModel.RefLink = model.RefLink;
            viewModel.Image = "";
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

            model.PartnerPackID = viewModel.PartnerTypeSelected.ID;

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

            var items = PartnerDataController.GetItems();

            if (args.Filter.StatusSelected != data.ViewModel.StatusFilterEnum.Alle)
            {
                if (args.Filter.StatusSelected == data.ViewModel.StatusFilterEnum.Aktiv)
                    items = items.Where(x => x.IsActive).ToList();
                else
                    items = items.Where(x => !x.IsActive).ToList();
            }

            if (args.Filter.PartnerTypeSelected != "Alle")
                items = items.Where(x => x.PartnerPack.Name == args.Filter.PartnerTypeSelected).ToList();

            items = items.Where(x => x.Name.ToLower().Contains(args.Filter.Name.ToLower())).ToList();

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