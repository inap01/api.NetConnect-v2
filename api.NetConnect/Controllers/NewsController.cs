using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.News;
using api.NetConnect.data.ViewModel.News.Backend;
using api.NetConnect.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.NetConnect.Controllers
{
    using NewsListViewModel = ListViewModel<NewsViewModelItem>;
    using BackendNewsListViewModel = ListViewModel<BackendNewsViewModelItem>;

    public class NewsController : ApiController
    {
        #region Frontend
        [HttpGet]
        public IHttpActionResult Get()
        {
            NewsListViewModel viewmodel = new NewsListViewModel();
            viewmodel.Authenticated = UserHelper.Authenticated;
            FbBaseComment bc = new FbBaseComment()
            {
                Name = "Marius Hartmann",
                Link = "http://www.facebook.com",
                Date = DateTime.Now,
                Text = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut."
            };
            FbComment c = new FbComment()
            {
                Name = "Marius Hartmann",
                Link = "http://www.facebook.com",
                Date = DateTime.Now,
                Text = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut."
            };
            c.Comments.Add(bc);
            c.Comments.Add(bc);
            c.Comments.Add(bc);

            NewsViewModelItem i = new NewsViewModelItem()
            {
                ID = 1,
                Title = "News 1",
                Image = "image 1",
                Link = "http://www.google.de",
                Date = DateTime.Now,
                Text = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut."
            };
            i.Comments.Add(c);
            viewmodel.Data.Add(i);
            i.Comments.Add(c);
            viewmodel.Data.Add(i);


            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Detail(Int32 id)
        {
            NewsViewModel viewmodel = new NewsViewModel();
            viewmodel.Authenticated = UserHelper.Authenticated;
            FbBaseComment bc = new FbBaseComment()
            {
                Name = "Marius Hartmann",
                Link = "http://www.facebook.com",
                Date = DateTime.Now,
                Text = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut."
            };
            FbComment c = new FbComment()
            {
                Name = "Marius Hartmann",
                Link = "http://www.facebook.com",
                Date = DateTime.Now,
                Text = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut."
            };
            c.Comments.Add(bc);
            c.Comments.Add(bc);
            c.Comments.Add(bc);
            NewsViewModelItem i = new NewsViewModelItem()
            {
                ID = 1,
                Title = "News 1",
                Image = "image 1",
                Link = "http://www.google.de",
                Date = DateTime.Now,
                Text = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut."
            };
            i.Comments.Add(c);
            viewmodel.Data = i;

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
