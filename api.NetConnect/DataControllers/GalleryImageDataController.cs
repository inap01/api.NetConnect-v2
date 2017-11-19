using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class GalleryImageDataController 
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
        public static List<GalleryItem> GetItems(int eventID)
        {
            List<GalleryItem> items = new List<GalleryItem>();
            var serverBasePath = HttpContext.Current.Server.MapPath("~");
            string targetDir = GetTargetDir(eventID);
            int id = 0;
            //TODO abklären wie Thumbnails generiert werden
            foreach(var file in Directory.GetFiles(targetDir))
            {
                items.Add(new GalleryItem()
                {
                    ID = id++,
                    EventID = eventID,
                    RelativeURL = file.Remove(0, serverBasePath.Length).Replace("\\", "/")
                });
            }
            return items;
        }
        public static GalleryItem GetThumbnail(int eventID)
        {
            var serverBasePath = HttpContext.Current.Server.MapPath("~");
            var res = Directory.GetFiles(GetTargetDir(eventID)).FirstOrDefault(x => x.Contains("__preview"));
            if (res != null)
                return new GalleryItem() { EventID = eventID, ID = 0, RelativeURL = res.Remove(0, serverBasePath.Length).Replace("\\", "/") };
            return null;
        }
    }

    public class GalleryItem
    {
        public int ID { get; set; }
        public int EventID { get; set; }
        public string RelativeURL { get; set; }
    }
}