using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel.Catering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.Converters
{
    public static partial class ConverterExtensions
    {
        public static void FromModel(this ProductViewModelItem viewModel, CateringProduct model)
        {
            viewModel.ID = model.ID;
            viewModel.Name = model.Name;
            viewModel.IsActive = model.IsActive;
            viewModel.Price = model.Price;
            viewModel.SingleChoice = model.SingleChoice;

            var vm = new ProductAttributeViewModelItem();
            List<ProductAttributeViewModelItem> atts = new List<ProductAttributeViewModelItem>();
            foreach (var at in model.CateringProductAttributeRelation)
                atts.Add(new ProductAttributeViewModelItem().FromModel(at.CateringProductAttribute));

            viewModel.ProductAttribute = atts;
        }
        public static ProductAttributeViewModelItem FromModel(this ProductAttributeViewModelItem viewModel, CateringProductAttribute model)
        {
            viewModel.ID = model.ID;
            viewModel.IsActive = model.IsActive;
            viewModel.Name = model.Name;

            return viewModel;
        }
    }
}