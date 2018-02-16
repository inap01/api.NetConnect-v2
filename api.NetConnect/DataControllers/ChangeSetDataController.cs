using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class ChangeSetDataController : BaseDataController, IDataController<ChangeSet>
    {
        public ChangeSetDataController() : base()
        {

        }

        public ChangeSet GetItem(int ID)
        {
            var qry = db.ChangeSet.AsQueryable();

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<ChangeSet> GetItems()
        {
            var qry = db.ChangeSet.AsQueryable();

            return qry;
        }

        public ChangeSet Insert(ChangeSet item)
        {
            throw new NotImplementedException();
        }

        public ChangeSet Update(ChangeSet item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            throw new NotImplementedException();
        }
    }
}