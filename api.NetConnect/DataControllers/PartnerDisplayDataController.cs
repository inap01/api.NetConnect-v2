using api.NetConnect.data.Entity;
using System;
using System.Data.Entity;
using System.Linq;

namespace api.NetConnect.DataControllers
{
    public class PartnerDisplayDataController : BaseDataController, IDataController<PartnerDisplay>
    {
        public PartnerDisplayDataController() : base()
        {

        }

        #region Basic Functions
        public PartnerDisplay GetItem(int ID)
        {
            var qry = db.PartnerDisplay.AsQueryable();
            qry.Include(x => x.PartnerDisplayRelation);

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<PartnerDisplay> GetItems()
        {
            var qry = db.PartnerDisplay.AsQueryable();
            qry.Include(x => x.PartnerDisplayRelation);

            return qry;
        }

        public PartnerDisplay Insert(PartnerDisplay item)
        {
            var result = db.PartnerDisplay.Add(item);
            db.SaveChanges();

            return result;
        }

        public PartnerDisplay Update(PartnerDisplay item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            db.PartnerDisplay.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion
    }
}