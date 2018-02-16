using api.NetConnect.Converters;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Event;
using api.NetConnect.data.ViewModel.News;
using api.NetConnect.data.ViewModel.News.Backend;
using api.NetConnect.DataControllers;
using api.NetConnect.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.NetConnect.Controllers
{
    public class NewsController : BaseController
    {
        #region Frontend
        [HttpGet]
        public IHttpActionResult Get()
        {
            FbNewsListViewModel viewmodel = new FbNewsListViewModel();
            EventDataController dataCtrl = new EventDataController();

            var nextEvent = dataCtrl.GetItems().OrderByDescending(x => x.Start).FirstOrDefault(x => x.Start > DateTime.Now);
            if(nextEvent != null)
                viewmodel.NextEvent = new EventViewModelItem().FromModel(nextEvent);
            viewmodel.Data = NewsDataController.GetItems().data;

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Detail(Int32 id)
        {
            NewsViewModel viewmodel = new NewsViewModel();

            return Ok(viewmodel);
        }
        #endregion

        #region Backend
        [HttpGet]
        public IHttpActionResult Backend_Get()
        {
            BackendNewsListViewModel viewmodel = new BackendNewsListViewModel();
            BackendNewsViewModelItem i = new BackendNewsViewModelItem()
            {
                ID = 1,
                Title = "News 1",
                Image = "image 1",
                Link = "http://www.google.de",
                Date = DateTime.Now,
                Text = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut."
            };
            viewmodel.Data.Add(i);
            viewmodel.Data.Add(i);


            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Backend_Detail(Int32 id)
        {
            BackendNewsViewModel viewmodel = new BackendNewsViewModel();
            BackendNewsViewModelItem i = new BackendNewsViewModelItem()
            {
                ID = 1,
                Title = "News 1",
                Image = "image 1",
                Link = "http://www.google.de",
                Date = DateTime.Now,
                Text = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut."
            };
            viewmodel.Data = i;

            return Ok(viewmodel);
        }
        #endregion
    }
}
