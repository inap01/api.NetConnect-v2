using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class GalleryDataController 
    {
        private static string GetTargetDir(int eventID)
        {
            var serverBasePath = HttpContext.Current.Server.MapPath("~");
            var imageDirectoryPath = Path.Combine(serverBasePath, "images", "gallery");

            var targetDir = Directory.GetDirectories(imageDirectoryPath).FirstOrDefault(x => x.EndsWith(eventID.ToString()));
            if (targetDir == null)
                throw new DirectoryNotFoundException();
            return targetDir;
        }
        public static GalleryItem GetGalleryThumbnail(int eventID)
        {
            var serverBasePath = HttpContext.Current.Server.MapPath("~");
            var res = Directory.GetFiles(GetTargetDir(eventID)).FirstOrDefault(x => x.Contains("__preview"));
            if (res != null)
                return new GalleryItem()
                {
                    ImageUrl = res.Remove(0, (serverBasePath + "/images/").Length).Replace("\\", "/")
                };
            return null;
        }
        public static Int32 Count(int eventID)
        {
            List<GalleryItem> items = new List<GalleryItem>();
            var serverBasePath = HttpContext.Current.Server.MapPath("~");
            string targetDir = GetTargetDir(eventID);

            return Directory.GetFiles(targetDir).Where(x => !x.Contains("_preview")).Count();
        }
        public static List<GalleryItem> GetItems(int eventID)
        {
            List<GalleryItem> items = new List<GalleryItem>();
            var serverBasePath = HttpContext.Current.Server.MapPath("~");
            string targetDir = GetTargetDir(eventID);

            foreach (var file in Directory.GetFiles(targetDir).Where(x => !x.Contains("_preview")))
            {
                String _thumbnail = GetImageThumbnail(file);
                items.Add(new GalleryItem()
                {
                    ImageUrl = file.Remove(0, (serverBasePath + "/images/").Length).Replace("\\", "/"),
                    ThumbnailUrl = _thumbnail.Remove(0, (serverBasePath + "/images/").Length).Replace("\\", "/")
                });
            }
            return items;
        }
        private static String GetImageThumbnail(String originalPath)
        {
            Int32 index = originalPath.LastIndexOf("\\");
            String thumbnailPath = originalPath.Insert(index, "\\thumbnails");
            String thumbnailDir = thumbnailPath.Substring(0, thumbnailPath.LastIndexOf('\\'));

            if (!Directory.Exists(thumbnailDir))
                Directory.CreateDirectory(thumbnailDir);
            if (!File.Exists(thumbnailPath))
            {
                Image image = Image.FromFile(originalPath);
                Image thumb = image.GetThumbnailImage(Properties.Settings.Default.galleryThumbnailWidth, Properties.Settings.Default.galleryThumbnailHeight, () => false, IntPtr.Zero);
                thumb.Save(thumbnailPath);
            }

            return thumbnailPath;
        }
    }

    public class GalleryItem
    {
        public string ImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}