using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel.Catering;
using api.NetConnect.data.ViewModel.Catering.Backend;
using api.NetConnect.DataControllers;
using api.NetConnect.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.Converters
{
    public static partial class ConverterExtensions
    {
        public static ProductViewModelItem FromModel(this ProductViewModelItem viewmodel, CateringProduct model)
        {
            viewmodel.ID = model.ID;
            viewmodel.Name = model.Name;
            viewmodel.Price = model.Price;
            viewmodel.SingleChoice = model.SingleChoice;
            viewmodel.Image = Properties.Settings.Default.imageAbsolutePath + model.Image;
            
            List<ProductAttributeViewModelItem> atts = new List<ProductAttributeViewModelItem>();
            foreach (var at in model.CateringProductAttributeRelation)
                atts.Add(new ProductAttributeViewModelItem().FromModel(at.CateringProductAttribute));

            viewmodel.ProductAttributes = atts;

            return viewmodel;
        }

        public static ProductAttributeViewModelItem FromModel(this ProductAttributeViewModelItem viewmodel, CateringProductAttribute model)
        {
            viewmodel.ID = model.ID;
            viewmodel.Name = model.Name;

            return viewmodel;
        }

        public static CateringSeat FromModel(this CateringSeat viewmodel, Seat model)
        {
            viewmodel.ID = model.ID;
            viewmodel.SeatNumber = model.SeatNumber;

            return viewmodel;
        }

        public static CateringOrder ToModel(this OrderRequest viewmodel, Int32 EventID)
        {
            CateringOrder model = new CateringOrder();
            
            model.UserID = UserHelper.CurrentUserID;
            model.SeatID = viewmodel.SeatID;
            model.EventID = EventID;
            model.Registered = DateTime.Now;

            model.CateringOrderDetail = viewmodel.Items.ConvertAll(x =>
            {
                return x.ToModel();
            });

            return model;
        }

        public static CateringOrderDetail ToModel(this OrderRequest.OrderRequestItem viewmodel)
        {
            CateringOrderDetail model = new CateringOrderDetail();

            model.CateringProductID = viewmodel.ID;
            model.Attributes = JsonConvert.SerializeObject(viewmodel.Attributes);
            model.Amount = viewmodel.Amount;

            return model;
        }

        public static BackendCateringListViewModel FromModel(this BackendCateringListViewModel viewmodel, IEnumerable<CateringOrder> modelList)
        {
            viewmodel.Data = new List<BackendCateringViewModelItem>().FromModel(modelList);

            return viewmodel;
        }
        public static List<BackendCateringViewModelItem> FromModel(this List<BackendCateringViewModelItem> viewmodel, IEnumerable<CateringOrder> modelList)
        {
            foreach (var model in modelList)
                viewmodel.Add(new BackendCateringViewModelItem().FromModel(model));

            return viewmodel;
        }
        public static BackendCateringViewModelItem FromModel(this BackendCateringViewModelItem viewmodel, CateringOrder model)
        {
            viewmodel.ID = model.ID;
            viewmodel.Event.FromModel(model.Event);
            viewmodel.User.FromModel(model.User);
            viewmodel.SeatNumber = model.Seat.SeatNumber;
            viewmodel.Order = model.CateringOrderDetail.ToList().ConvertAll(x =>
            {
                return new BackendCateringOrderItem().FromModel(x);
            });
            viewmodel.Status = new BackendCateringStatusOption(model.OrderState);
            viewmodel.Note = model.Note;

            return viewmodel;
        }
        public static BackendCateringSeat FromModel(this BackendCateringSeat viewmodel, User user, Seat seat)
        {
            viewmodel.Name = $"{user.FirstName} {user.LastName}";
            viewmodel.SeatNumber = seat.SeatNumber;

            return viewmodel;
        }
        public static BackendCateringOrderItem FromModel(this BackendCateringOrderItem viewmodel, CateringOrderDetail model)
        {
            var attr = JsonConvert.DeserializeObject<String[]>(model.Attributes);

            viewmodel.ID = model.ID;
            viewmodel.Name = model.CateringProduct.Name;
            viewmodel.Attributes = attr.ToList();
            viewmodel.Amount = model.Amount;
            viewmodel.Price = model.CateringProduct.Price;

            return viewmodel;
        }
        public static BackendCateringProductItem FromModel(this BackendCateringProductItem viewmodel, CateringProduct model)
        {
            viewmodel.ID = model.ID;
            viewmodel.Name = model.Name;
            viewmodel.Price = model.Price;
            viewmodel.SingleChoice = model.SingleChoice;

            List<ProductAttributeViewModelItem> atts = new List<ProductAttributeViewModelItem>();
            foreach (var at in model.CateringProductAttributeRelation)
                atts.Add(new ProductAttributeViewModelItem().FromModel(at.CateringProductAttribute));

            viewmodel.Attributes = atts;

            return viewmodel;
        }
        public static CateringOrder ToModel(this BackendNewOrderRequest viewmodel)
        {
            UserDataController userDataCtrl = new UserDataController();

            CateringOrder model = new CateringOrder();

            model.EventID = viewmodel.Event.ID;
            model.OrderState = 0;
            model.Registered = DateTime.Now;
            model.UserID = userDataCtrl.GetItems().Single(x => x.Email == "bestellung.theke@lan-netconnect.de").ID;
            model.SeatID = 1; // TODO
            model.Note = viewmodel.Note;

            model.CateringOrderDetail = viewmodel.Data.ConvertAll(x =>
            {
                return x.ToModel();
            });

            return model;
        }

        public static CateringOrderDetail ToModel(this BackendNewOrderRequestItem viewmodel)
        {
            CateringOrderDetail model = new CateringOrderDetail();

            model.CateringProductID = viewmodel.Product.ID;
            model.Attributes = JsonConvert.SerializeObject(viewmodel.Attributes);
            model.Amount = viewmodel.Amount;

            return model;
        }

        public static CateringOrder ToModel(this BackendCateringViewModelItem viewmodel)
        {
            CateringOrder model = new CateringOrder();

            model.ID = viewmodel.ID;
            model.OrderState = viewmodel.Status.Key;

            return model;
        }
    }
}