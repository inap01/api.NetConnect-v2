using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Partner;
using api.NetConnect.data.ViewModel.Partner.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.NetConnect.Controllers
{
    using PartnerListViewModel = ListViewModel<PartnerViewModelItem>;
    using BackendPartnerListViewModel = ListViewModel<BackendPartnerViewModelItem>;

    public class PartnerController : ApiController
    {
        #region Frontend
        [HttpGet]
        public IHttpActionResult Get()
        {
            PartnerListViewModel viewmodel = new PartnerListViewModel();
            for (int i = 0; i < 10; i++)
            {
                PartnerViewModelItem item = new PartnerViewModelItem()
                {
                    ID = 1,
                    Name = "Partner " + (i + 1),
                    Description = "",
                    PartnerType = new PartnerType() { Name = "Grandmaster" },
                    Image = "",
                    Link = "http://google.de",
                    RefLink = "http://google.de"
                };
                item.Display.Add("Header", false);
                item.Display.Add("Footer", true);
                viewmodel.Data.Add(item);
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Detail(Int32 id)
        {
            PartnerViewModel viewmodel = new PartnerViewModel();
            viewmodel.Data = new PartnerViewModelItem()
            {
                ID = 1,
                Name = "Partner 1",
                Description = "",
                PartnerType = new PartnerType() { Name = "Grandmaster" },
                Image = "",
                Link = "http://google.de",
                RefLink = "http://google.de"
            };
            viewmodel.Data.Display.Add("Header", false);
            viewmodel.Data.Display.Add("Footer", true);

            return Ok(viewmodel);
        }
        #endregion

        #region Backend
        [HttpGet]
        public IHttpActionResult Backend_Get([FromUri] BackendPartnerFilter filter)
        {
            BackendPartnerListViewModel viewmodel = new BackendPartnerListViewModel();
            for (int i = 0; i < 10; i++)
            {
                BackendPartnerViewModelItem item = new BackendPartnerViewModelItem()
                {
                    ID = 1,
                    Name = "Partner " + (i + 1),
                    Description = "",
                    PartnerType = new PartnerType() { Name = "Grandmaster" },
                    Image = "",
                    Link = "http://google.de",
                    RefLink = "http://google.de"
                };
                item.Display.Add("Header", false);
                item.Display.Add("Footer", true);
                viewmodel.Data.Add(item);
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Backend_Detail(Int32 id)
        {
            BackendPartnerViewModel viewmodel = new BackendPartnerViewModel();
            viewmodel.Data = new BackendPartnerViewModelItem()
            {
                ID = 1,
                Name = "Partner 1",
                Description = "",
                PartnerType = new PartnerType() { ID = 1, Name = "Grandmaster" },
                Image = "",
                Link = "http://google.de",
                RefLink = "http://google.de"
            };
            viewmodel.Data.Display.Add("Header", false);
            viewmodel.Data.Display.Add("Footer", true);

            viewmodel.Form.Add("ID", new BackendBaseViewModel.InputInformation() { Type = "integer", Readonly = true });
            viewmodel.Form.Add("Name", new BackendBaseViewModel.InputInformation() { Type = "string" });
            viewmodel.Form.Add("Description", new BackendBaseViewModel.InputInformation() { Type = "text" });
            var refForm = new Dictionary<string, BackendBaseViewModel.InputInformation>();
            refForm.Add("ID", new BackendBaseViewModel.InputInformation() { Type = "integer", Readonly = true });
            refForm.Add("Name", new BackendBaseViewModel.InputInformation() { Type = "string", Readonly = true });
            viewmodel.Form.Add("PartnerType", new BackendBaseViewModel.InputInformation() { Type = "reference", Reference = "PartnerType", ReferenceForm = refForm });
            viewmodel.Form.Add("Image", new BackendBaseViewModel.InputInformation() { Type = "image" });
            viewmodel.Form.Add("Link", new BackendBaseViewModel.InputInformation() { Type = "string" });
            viewmodel.Form.Add("RefLink", new BackendBaseViewModel.InputInformation() { Type = "string" });

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Backend_Detail_New()
        {
            BackendPartnerViewModel viewmodel = new BackendPartnerViewModel();

            viewmodel.Form.Add("ID", new BackendBaseViewModel.InputInformation() { Type = "integer", Readonly = true });
            viewmodel.Form.Add("Name", new BackendBaseViewModel.InputInformation() { Type = "string" });
            viewmodel.Form.Add("Description", new BackendBaseViewModel.InputInformation() { Type = "text" });
            var refForm = new Dictionary<string, BackendBaseViewModel.InputInformation>();
            refForm.Add("ID", new BackendBaseViewModel.InputInformation() { Type = "integer", Readonly = true });
            refForm.Add("Name", new BackendBaseViewModel.InputInformation() { Type = "string", Readonly = true });
            viewmodel.Form.Add("PartnerType", new BackendBaseViewModel.InputInformation() { Type = "reference", Reference = "PartnerType", ReferenceForm = refForm });
            viewmodel.Form.Add("Image", new BackendBaseViewModel.InputInformation() { Type = "image" });
            viewmodel.Form.Add("Link", new BackendBaseViewModel.InputInformation() { Type = "string" });
            viewmodel.Form.Add("RefLink", new BackendBaseViewModel.InputInformation() { Type = "string" });

            return Ok(viewmodel);
        }

        [HttpPost]
        public IHttpActionResult Backend_Detail_Insert(BackendPartnerViewModelItem request)
        {
            BackendPartnerViewModel viewmodel = new BackendPartnerViewModel();

            // TODO

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult Backend_Detail_Update(Int32 id, BackendPartnerViewModelItem request)
        {
            BackendPartnerViewModel viewmodel = new BackendPartnerViewModel();

            // TODO

            return Ok(viewmodel);
        }

        [HttpDelete]
        public IHttpActionResult Backend_Delete(BackendPartnerDeleteRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();

            // TODO

            return Ok(viewmodel);
        }
        #endregion
    }
}
