using api.NetConnect.data.Entity;
using System;
using System.Data.Entity;
using System.Linq;

namespace api.NetConnect.DataControllers
{
    public class PartnerPackDataController : BaseDataController, IDataController<PartnerPack>
    {
        public PartnerPackDataController() : base()
        {

        }

        #region Basic Functions
        public PartnerPack GetItem(int ID)
        {
            var qry = db.PartnerPack.AsQueryable();
            qry.Include(x => x.Partner);

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<PartnerPack> GetItems()
        {
            var qry = db.PartnerPack.AsQueryable();
            qry.Include(x => x.Partner);

            return qry;
        }

        public PartnerPack Insert(PartnerPack item)
        {
            var result = db.PartnerPack.Add(item);
            db.SaveChanges();

            return result;
        }

        public PartnerPack Update(PartnerPack item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            db.PartnerPack.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion
    }
}