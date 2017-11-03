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
        public static List<GalleryItem> GetItems(int eventID)
        {
            List<GalleryItem> items = new List<GalleryItem>();
            var serverBasePath = HttpContext.Current.Server.MapPath("~");            
            var imageDirectoryPath = Path.Combine(serverBasePath, "images", "gallery");

            var targetDir = Directory.GetDirectories(imageDirectoryPath).FirstOrDefault(x => x.Last().ToString() == eventID.ToString());
            if (targetDir == null)
                throw new DirectoryNotFoundException();

            int id = 0;
            //TODO abklären wie Thumbnails generiert werden
            foreach(var file in Directory.GetFiles(targetDir))
            {
                items.Add(new GalleryItem()
                {
                    ID = id++,
                    EventID = eventID,
                    RelativeURL = file.Remove(0, serverBasePath.Length).Replace("\\", "/").Remove(0, 1)
                });
            }
            return items;
        }
    }

    public class GalleryItem
    {
        public int ID { get; set; }
        public int EventID { get; set; }
        public string RelativeURL { get; set; }
    }
}