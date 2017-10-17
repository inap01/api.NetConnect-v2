using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.NetConnect.Controllers
{
    using NewsListViewModel = ListViewModel<NewsViewModelItem>;

    public class NewsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            NewsListViewModel model = new NewsListViewModel();
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
            model.Data.Add(i);
            i.Comments.Add(c);
            model.Data.Add(i);


            return Ok(model);
        }
    }
}
