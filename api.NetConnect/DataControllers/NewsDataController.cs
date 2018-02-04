using api.NetConnect.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class NewsDataController
    {
        #region Frontend
        public static List<FbNews> GetItems(Int32 ItemAmount = 7)
        {
            List<FbNews> modelList = new List<FbNews>();

            return modelList;
        }
        #endregion
    }
}