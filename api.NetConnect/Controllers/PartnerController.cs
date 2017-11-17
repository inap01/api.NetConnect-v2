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
    using BackendPartnerListArgs = ListArgsRequest<BackendPartnerFilter>;
    using BackendPartnerListViewModel = ListArgsViewModel<BackendPartnerViewModelItem, BackendPartnerFilter>;
    using data.Entity;
    using DataControllers;
    using Converters;
    using api.NetConnect.Helper;

    public class PartnerController : ApiController
    {
        #region Frontend
        [HttpGet]
        public IHttpActionResult Get()
        {
            PartnerListViewModel viewmodel = new PartnerListViewModel();

            foreach(var model in PartnerDataController.GetItems())
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

            viewmodel.Data.FromModel(PartnerDataController.GetItem(id));

            return Ok(viewmodel);
        }
        #endregion

        #region Backend
        [HttpGet]
        public IHttpActionResult Backend_Get()
        {
            BackendPartnerListViewModel viewmodel = new BackendPartnerListViewModel();
            BackendPartnerListArgs args = new BackendPartnerListArgs();

            try
            {
                viewmodel.Filter.PartnerTypeOptions = PartnerPackDataController.GetItems().Select(x => x.Name).OrderBy(x => x).ToList();

                Int32 TotalItemsCount;
                viewmodel.Data = PartnerConverter.FilterList(args, out TotalItemsCount);

                viewmodel.Pagination.TotalItemsCount = TotalItemsCount;
            }
            catch(Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult Backend_FilterList(BackendPartnerListArgs args)
        {
            BackendPartnerListViewModel viewmodel = new BackendPartnerListViewModel();

            try
            {
                viewmodel.Filter.Name = args.Filter.Name;
                viewmodel.Filter.StatusSelected = args.Filter.StatusSelected;
                viewmodel.Filter.PartnerTypeSelected = args.Filter.PartnerTypeSelected;
                viewmodel.Pagination = args.Pagination;
                viewmodel.Filter.PartnerTypeOptions = PartnerPackDataController.GetItems().Select(x => x.Name).OrderBy(x => x).ToList();

                Int32 TotalItemsCount;
                viewmodel.Data = PartnerConverter.FilterList(args, out TotalItemsCount);

                viewmodel.Pagination.TotalItemsCount = TotalItemsCount;
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Backend_Detail(Int32 id)
        {
            BackendPartnerViewModel viewmodel = new BackendPartnerViewModel();

            try
            {
                viewmodel.Data.FromModel(PartnerDataController.GetItem(id));
                viewmodel.PartnerTypeOptions = PartnerPackDataController.GetItems().ConvertAll(x => 
                {
                    return new PartnerType() { ID = x.ID, Name = x.Name };
                }).OrderBy(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Backend_Detail_New()
        {
            BackendPartnerViewModel viewmodel = new BackendPartnerViewModel();

            try
            {
                viewmodel.PartnerTypeOptions = PartnerPackDataController.GetItems().ConvertAll(x =>
                {
                    return new PartnerType() { ID = x.ID, Name = x.Name };
                }).OrderBy(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPost]
        public IHttpActionResult Backend_Detail_Insert(BackendPartnerViewModelItem request)
        {
            BackendPartnerViewModel viewmodel = new BackendPartnerViewModel();

            try
            {
                viewmodel.Data.FromModel(PartnerDataController.Create(request.ToModel()));

                viewmodel.AddSuccessAlert("Partner wurde erstellt.");
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult Backend_Detail_Update(Int32 id, BackendPartnerViewModelItem request)
        {
            BackendPartnerViewModel viewmodel = new BackendPartnerViewModel();

            try
            {
                viewmodel.Data.FromModel(PartnerDataController.Update(request.ToModel()));

                viewmodel.AddSuccessAlert("Partner wurde erfolgreich aktualisiert.");
            }
            catch(Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpDelete]
        public IHttpActionResult Backend_Delete(BackendPartnerDeleteRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();

            // TODO

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult Backend_Position(PositionPartnerTypeRequest args)
        {
            BackendPartnerPositionViewModel viewmodel = new BackendPartnerPositionViewModel();

            if (args.PartnerType == null)
                args.PartnerType = PartnerPackDataController.GetItems().First().Name;

            int position = 1;
            viewmodel.Data = PartnerDataController.GetItems()
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

            viewmodel.PartnerTypeOptions = PartnerPackDataController.GetItems()
                .ConvertAll(x =>
            {
                return x.Name;
            });

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult Backend_Position_Update(PositionPartnerUpdateRequest request)
        {
            BackendPartnerPositionViewModel viewmodel = new BackendPartnerPositionViewModel();

            try
            {
                request.Partner.ForEach(x =>
                {
                    PartnerDataController.Update(x.ToModel());
                });

                viewmodel.AddSuccessAlert("Sortierung wurde aktualisiert.");
            }
            catch(Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }
        #endregion
    }
}
