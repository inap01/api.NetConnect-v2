using api.NetConnect.data.Entity;
using System;
using System.Data.Entity;
using System.Linq;

namespace api.NetConnect.DataControllers
{
    public class PartnerDisplayRelationDataController : BaseDataController, IDataController<PartnerDisplayRelation>
    {
        public PartnerDisplayRelationDataController() : base()
        {

        }

        #region Basic Functions
        public PartnerDisplayRelation GetItem(int ID)
        {
            var qry = db.PartnerDisplayRelation.AsQueryable();
            qry.Include(x => x.Partner);
            qry.Include(x => x.PartnerDisplay);

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<PartnerDisplayRelation> GetItems()
        {
            var qry = db.PartnerDisplayRelation.AsQueryable();
            qry.Include(x => x.Partner);
            qry.Include(x => x.PartnerDisplay);

            return qry;
        }

        public PartnerDisplayRelation Insert(PartnerDisplayRelation item)
        {
            var result = db.PartnerDisplayRelation.Add(item);
            db.SaveChanges();

            return result;
        }

        public PartnerDisplayRelation Update(PartnerDisplayRelation item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            db.PartnerDisplayRelation.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion
    }
}