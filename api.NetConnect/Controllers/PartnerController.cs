using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Partner;
using api.NetConnect.data.ViewModel.Partner.Backend;
using api.NetConnect.DataControllers;
using api.NetConnect.Converters;
using api.NetConnect.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.NetConnect.Controllers
{
    public class PartnerController : BaseController
    {
        #region Frontend
        [HttpGet]
        public IHttpActionResult Get()
        {
            PartnerListViewModel viewmodel = new PartnerListViewModel();
            PartnerDataController dataCtrl = new PartnerDataController();

            foreach (var model in dataCtrl.GetItems().Where(x => x.IsActive).OrderBy(x => x.PartnerPackID).ThenBy(x => x.Position))
            {
                PartnerViewModelItem item = new PartnerViewModelItem();
                
                item.FromModel(model);
                viewmodel.Data.Add(item);
            }

            return Ok(viewmodel);
        }
        //TODO Fertig implementieren
        [HttpGet]
        public IHttpActionResult Detail(Int32 id)
        {
            PartnerViewModel viewmodel = new PartnerViewModel();
            PartnerDataController dataCtrl = new PartnerDataController();

            viewmodel.Data.FromModel(dataCtrl.GetItem(id));

            return Ok(viewmodel);
        }
        #endregion

        #region Backend
        [Authorize(Roles = "Admin,Team")]
        [HttpGet]
        public IHttpActionResult Backend_Get()
        {
            BackendPartnerListViewModel viewmodel = new BackendPartnerListViewModel();
            BackendPartnerListArgs args = new BackendPartnerListArgs();
            PartnerDataController dataCtrl = new PartnerDataController();
            PartnerPackDataController partnerPackdataCtrl = new PartnerPackDataController();

            try
            {
                viewmodel.Filter.PartnerTypeOptions = partnerPackdataCtrl.GetItems().Select(x => x.Name).OrderBy(x => x).ToList();

                Int32 TotalItemsCount;
                viewmodel.Data.FromModel(dataCtrl.FilterList(args, out TotalItemsCount));

                viewmodel.Pagination.TotalItemsCount = TotalItemsCount;
            }
            catch(Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpPut]
        public IHttpActionResult Backend_FilterList(BackendPartnerListArgs args)
        {
            BackendPartnerListViewModel viewmodel = new BackendPartnerListViewModel();
            PartnerDataController dataCtrl = new PartnerDataController();
            PartnerPackDataController partnerPackdataCtrl = new PartnerPackDataController();

            try
            {
                viewmodel.Filter.Name = args.Filter.Name;
                viewmodel.Filter.StatusSelected = args.Filter.StatusSelected;
                viewmodel.Filter.PartnerTypeSelected = args.Filter.PartnerTypeSelected;
                viewmodel.Pagination = args.Pagination;
                viewmodel.Filter.PartnerTypeOptions = partnerPackdataCtrl.GetItems().Select(x => x.Name).OrderBy(x => x).ToList();

                Int32 TotalItemsCount;
                viewmodel.Data.FromModel(dataCtrl.FilterList(args, out TotalItemsCount));

                viewmodel.Pagination.TotalItemsCount = TotalItemsCount;
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpGet]
        public IHttpActionResult Backend_Detail(Int32 id)
        {
            BackendPartnerViewModel viewmodel = new BackendPartnerViewModel();
            PartnerDataController dataCtrl = new PartnerDataController();
            PartnerPackDataController partnerPackdataCtrl = new PartnerPackDataController();

            try
            {
                viewmodel.Data.FromModel(dataCtrl.GetItem(id));
                viewmodel.PartnerTypeOptions = partnerPackdataCtrl.GetItems().ToList().ConvertAll(x => 
                {
                    return new BackendPartnerType() { ID = x.ID, Name = x.Name };
                }).OrderBy(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpGet]
        public IHttpActionResult Backend_Detail_New()
        {
            BackendPartnerViewModel viewmodel = new BackendPartnerViewModel();
            PartnerDataController dataCtrl = new PartnerDataController();
            PartnerPackDataController partnerPackdataCtrl = new PartnerPackDataController();
            PartnerDisplayDataController partnerDisplayDataCtrl = new PartnerDisplayDataController();

            try
            {
                viewmodel.PartnerTypeOptions = partnerPackdataCtrl.GetItems().ToList().ConvertAll(x =>
                {
                    return new BackendPartnerType() { ID = x.ID, Name = x.Name };
                }).OrderBy(x => x.Name).ToList();
                foreach (var display in partnerDisplayDataCtrl.GetItems())
                        viewmodel.Data.Display.Add(new data.ViewModel.Partner.PartnerDisplay()
                        {
                            ID = display.ID,
                            Name = display.Name,
                            Value = false
                        });
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpPost]
        public IHttpActionResult Backend_Detail_Insert(BackendPartnerViewModelItem request)
        {
            BackendPartnerViewModel viewmodel = new BackendPartnerViewModel();
            PartnerDataController dataCtrl = new PartnerDataController();

            try
            {
                viewmodel.Data.FromModel(dataCtrl.Insert(request.ToModel()));
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Partner wurde erstellt.");
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpPut]
        public IHttpActionResult Backend_Detail_Update(Int32 id, BackendPartnerViewModelItem request)
        {
            BackendPartnerViewModel viewmodel = new BackendPartnerViewModel();
            PartnerDataController dataCtrl = new PartnerDataController();

            try
            {
                viewmodel.Data.FromModel(dataCtrl.Update(request.ToModel()));
            }
            catch(Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Partner wurde erfolgreich aktualisiert.");
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpDelete]
        public IHttpActionResult Backend_Delete(BackendPartnerDeleteRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();

            // TODO

            return Ok(viewmodel);
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpPost]
        public IHttpActionResult Backend_Position(PositionPartnerTypeRequest args)
        {
            BackendPartnerPositionViewModel viewmodel = new BackendPartnerPositionViewModel();
            PartnerDataController dataCtrl = new PartnerDataController();
            PartnerPackDataController partnerPackDataCtrl = new PartnerPackDataController();

            if (args.PartnerType == null)
                args.PartnerType = partnerPackDataCtrl.GetItems().First().Name;

            int position = 1;
            viewmodel.Data = dataCtrl.GetItems()
                .Where(x => x.PartnerPack.Name == args.PartnerType && x.IsActive)
                .OrderBy(x => x.Position).ToList()
                .ConvertAll(x =>
            {
                return new BackendPartnerPositionViewModelItem()
                {
                    ID = x.ID,
                    Name = x.Name,
                    Position = position++
                };
            }).ToList();

            viewmodel.PartnerTypeOptions = partnerPackDataCtrl.GetItems().ToList().ConvertAll(x =>
            {
                return x.Name;
            });

            return Ok(viewmodel);
        }

        [Authorize(Roles = "Admin,Team")]
        [HttpPut]
        public IHttpActionResult Backend_Position_Update(PositionPartnerUpdateRequest request)
        {
            BackendPartnerPositionViewModel viewmodel = new BackendPartnerPositionViewModel();
            PartnerDataController dataCtrl = new PartnerDataController();

            try
            {
                request.Partner.ForEach(x =>
                {
                    dataCtrl.Update(x.ToModel());
                });
            }
            catch(Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Sortierung wurde aktualisiert.");
        }
        #endregion
    }
}
