using api.NetConnect.data.ViewModel.Gallery;
using api.NetConnect.DataControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.Converters
{
    public static partial class ConverterExtensions
    {
        public static void FromModel(this GalleryViewModelImageItem viewModel, GalleryItem model)
        {
            viewModel.EventID = model.EventID;
            viewModel.ID = model.ID;
            viewModel.RelativeURL = model.RelativeURL;
            viewModel.RelativeURLThumbnail = "";
        }
    }
}