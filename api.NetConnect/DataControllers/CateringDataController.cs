using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Catering.Backend;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class CateringDataController : BaseDataController, IDataController<CateringProduct>
    {
        public CateringDataController() : base()
        {

        }

        #region Basic Functions
        public CateringProduct GetItem(int ID)
        {
            var qry = db.CateringProduct.AsQueryable();
            qry.Include(x => x.CateringProductAttributeRelation);

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<CateringProduct> GetItems()
        {
            var qry = db.CateringProduct.AsQueryable();
            qry.Include(x => x.CateringProductAttributeRelation);

            return qry;
        }

        public CateringProduct Insert(CateringProduct item)
        {
            var result = db.CateringProduct.Add(item);
            db.SaveChanges();

            return result;
        }

        public CateringProduct Update(CateringProduct item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            db.CateringProduct.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion
    }
}