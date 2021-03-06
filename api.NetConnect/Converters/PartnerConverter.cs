﻿using api.NetConnect.data.Entity;
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
        public static PartnerViewModelItem FromModel(this PartnerViewModelItem viewmodel, Partner model)
        {
            viewmodel.Name = model.Name;
            viewmodel.Description = model.Content;
            viewmodel.Image = Properties.Settings.Default.imageAbsolutePath + model.ImageOriginal;
            viewmodel.Link = model.Link;
            viewmodel.RefLink = model.Link;

            viewmodel.PartnerType.FromModel(model.PartnerPack);

            // TODO Nachfolgendes
            string[] displays = { "Header", "Footer" };
            foreach (var display in displays)
                viewmodel.Display.Add(new data.ViewModel.Partner.PartnerDisplay() { ID = 1, Name = display, Value = true });

            return viewmodel;
        }

        public static PartnerType FromModel(this PartnerType viewmodel, PartnerPack model)
        {
            viewmodel.ID = model.ID;
            viewmodel.Name = model.Name;

            return viewmodel;
        }
        #endregion
        #region Backend
        public static List<BackendPartnerViewModelItem> FromModel(this List<BackendPartnerViewModelItem> viewmodel, List<Partner> modelList)
        {
            foreach (var model in modelList)
                viewmodel.Add(new BackendPartnerViewModelItem().FromModel(model));

            return viewmodel;
        }

        public static BackendPartnerViewModelItem FromModel(this BackendPartnerViewModelItem viewmodel, Partner model)
        {
            PartnerDisplayDataController displayDataCtrl = new PartnerDisplayDataController();

            viewmodel.ID = model.ID;
            viewmodel.Name = model.Name;
            viewmodel.Description = model.Content;
            viewmodel.Link = model.Link;
            viewmodel.RefLink = model.RefLink;
            viewmodel.OriginalImage = model.ImageOriginal;
            viewmodel.PassiveImage = model.ImagePassive;
            viewmodel.IsActive = model.IsActive;

            viewmodel.PartnerTypeSelected = new BackendPartnerType()
            {
                ID = model.PartnerPack.ID,
                Name = model.PartnerPack.Name
            };
            
            foreach (var display in displayDataCtrl.GetItems())
            {
                if(model.PartnerDisplayRelation.FirstOrDefault(x => x.PartnerID == model.ID && x.PartnerDisplayID == display.ID) != null)
                    viewmodel.Display.Add(new data.ViewModel.Partner.PartnerDisplay()
                    {
                        ID = display.ID,
                        Name = display.Name,
                        Value = true
                    });
                else
                    viewmodel.Display.Add(new data.ViewModel.Partner.PartnerDisplay()
                    {
                        ID = display.ID,
                        Name = display.Name,
                        Value = false
                    });
            }

            return viewmodel;
        }

        public static Partner ToModel(this BackendPartnerViewModelItem viewmodel)
        {
            Partner model = new Partner();

            model.ID = viewmodel.ID;
            model.Name = viewmodel.Name;
            model.Content = viewmodel.Description;
            model.Link = viewmodel.Link;
            model.RefLink = viewmodel.RefLink;
            model.PartnerPackID = viewmodel.PartnerTypeSelected.ID;
            model.ImageOriginal = viewmodel.OriginalImage;
            model.ImagePassive = viewmodel.PassiveImage;
            model.IsActive = viewmodel.IsActive;

            return model;
        }

        public static BackendPartnerPositionViewModelItem FromModel(this BackendPartnerPositionViewModelItem viewmodel, Partner model, Int32 Position)
        {
            viewmodel.ID = model.ID;
            viewmodel.Name = model.Name;
            viewmodel.Position = Position;

            return viewmodel;
        }

        public static Partner ToModel(this BackendPartnerPositionViewModelItem viewmodel)
        {
            Partner model = new Partner();

            model.ID = viewmodel.ID;
            model.Position = viewmodel.Position;

            return model;
        }
        #endregion
    }
}