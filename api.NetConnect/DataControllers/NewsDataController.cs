using api.NetConnect.data.ViewModel.News;
using api.NetConnect.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class NewsDataController
    {
        //private static String request = "https://graph.facebook.com/netconnectev/posts?limit=10&fields=message,comments{comments{from,message,permalink_url,created_time},message,from,permalink_url,created_time},likes,created_time,story,full_picture,permalink_url&locale=de_DE&access_token=637939956366699|RgfPZ04x6S8T0Vii1wWEGFHky8Y";
        private static String endpoint = "posts";
        private static String accessToken = "637939956366699|RgfPZ04x6S8T0Vii1wWEGFHky8Y";
        private static String args = "limit=10&fields=message,comments{comments{from,message,permalink_url,created_time},message,from,permalink_url,created_time},likes,created_time,story,full_picture,permalink_url&locale=de_DE";

        #region Frontend
        public static FbNews GetItems()
        {
            FbNews modelList = new FbNews();

            FacebookClient graph = new FacebookClient();
            var task = graph.GetAsync<FbNews>(accessToken, endpoint, args);
            Task.WaitAll(task);
            modelList = task.Result;

            return modelList;
        }
        #endregion
    }
}