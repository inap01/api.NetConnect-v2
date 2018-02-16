using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public interface IDataController<T> where T : class
    {
        T GetItem(Int32 ID);
        IQueryable<T> GetItems();
        T Insert(T item);
        T Update(T item);
        void Delete(Int32 ID);
    }
}