using api.NetConnect.data.Entity;
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
        public static GalleryViewModelListItem FromModel(this GalleryViewModelListItem viewmodel, Event model, Int32 ImgCount, String Thumbnail)
        {
            viewmodel.ID = model.ID;
            viewmodel.Title = $"{model.EventType.Name} Vol.{model.Volume}";
            viewmodel.ImageCount = ImgCount;
            viewmodel.Thumbnail = Properties.Settings.Default.imageAbsolutePath + Thumbnail;

            return viewmodel;
        }

        public static GalleryViewModel FromModel(this GalleryViewModel viewmodel, Event model, List<GalleryItem> images)
        {
            viewmodel.Data.ID = model.ID;
            viewmodel.Data.ImageCount = images.Count;
            viewmodel.Data.Title = $"{model.EventType.Name} Vol.{model.Volume}";
            viewmodel.Data.Thumbnail = Properties.Settings.Default.imageAbsolutePath + GalleryDataController.GetGalleryThumbnail(model.ID).ImageUrl;

            viewmodel.Data.Images = images.ConvertAll(x =>
            {
                return new GalleryViewModelImageItem().FromModel(x);
            });

            return viewmodel;
        }

        public static GalleryViewModelImageItem FromModel(this GalleryViewModelImageItem viewmodel, GalleryItem model)
        {
            viewmodel.ImageUrl = Properties.Settings.Default.imageAbsolutePath + model.ImageUrl;
            viewmodel.ThumbnailUrl = Properties.Settings.Default.imageAbsolutePath + model.ThumbnailUrl;

            return viewmodel;
        }
    }
}