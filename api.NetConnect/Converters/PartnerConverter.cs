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
            viewModel.Image = Properties.Settings.Default.imageAbsolutePath + model.ImageOriginal + "/image.png";
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
        public static List<BackendPartnerViewModelItem> FromModel(this List<BackendPartnerViewModelItem> viewModel, List<Partner> model)
        {
            viewModel = model.ConvertAll(x =>
            {
                return new BackendPartnerViewModelItem().FromModel(x);
            });

            return viewModel;
        }

        public static BackendPartnerViewModelItem FromModel(this BackendPartnerViewModelItem viewModel, Partner model)
        {
            PartnerDisplayDataController displayDataCtrl = new PartnerDisplayDataController();

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
            
            foreach (var display in displayDataCtrl.GetItems())
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
            PartnerDataController dataCtrl = new PartnerDataController();
            PartnerPackDataController partnerPackDataCtrl = new PartnerPackDataController();

            Partner model = new Partner();
            if (viewModel.ID != 0) 
                model = dataCtrl.GetItem(viewModel.ID);
            
            model.Name = viewModel.Name;
            model.Content = viewModel.Description;
            model.Link = viewModel.Link;
            model.RefLink = viewModel.RefLink;
            //model.Image = viewModel.Image;
            model.IsActive = viewModel.IsActive;

            model.PartnerPack = partnerPackDataCtrl.GetItem(viewModel.PartnerTypeSelected.ID);

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
            PartnerDataController dataCtrl = new PartnerDataController();

            Partner model = new Partner();
            if (viewModel.ID != 0)
                model = dataCtrl.GetItem(viewModel.ID);

            model.Position = viewModel.Position;

            return model;
        }
        #endregion
    }
}