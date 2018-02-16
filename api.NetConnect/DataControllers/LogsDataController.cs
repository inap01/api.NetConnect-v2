using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class LogsDataController : BaseDataController, IDataController<Logs>
    {
        public LogsDataController() : base()
        {

        }

        #region Basic Functions
        public Logs GetItem(int ID)
        {
            var qry = db.Logs.AsQueryable();
            qry.Include(x => x.User);

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<Logs> GetItems()
        {
            var qry = db.Logs.AsQueryable();
            qry.Include(x => x.User);

            return qry;
        }

        public Logs Insert(Logs item)
        {
            var result = db.Logs.Add(item);
            db.SaveChanges();

            return result;
        }

        public Logs Update(Logs item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            db.Logs.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion
    }
}