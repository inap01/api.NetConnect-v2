using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class GenericDataController<T> where T : class
    {
        protected static DataContext db = null;

        protected static DataContext InitDB()
        {
            if (db != null)
                return db;
            db = new DataContext(ConfigurationManager.ConnectionStrings["Entities"].ConnectionString);
            return db;
        }

        public static T GetItem(Int32 condition)
        {
            InitDB();
            var list = GetPropertyMatchingTypeT(typeof(T));
            return GetPropertyFromSetMatchingCondition<Int32>(list, condition, "ID");
        }

        #region GETITEM
        public static T GetItem<ConditionPropType>(ConditionPropType condition, String ParameterName)
        {
            InitDB();
            var list = GetPropertyMatchingTypeT(typeof(T));
            
            return GetPropertyFromSetMatchingCondition<ConditionPropType>(list, condition, ParameterName);
        }
        
        private static T GetPropertyFromSetMatchingCondition<Z>(DbSet<T> value, Z condition, String paramName)
        {
            foreach(var x in value)
            {
                var props = x.GetType().GetProperty(paramName).GetValue(x);
                if (props.GetHashCode() == condition.GetHashCode())
                    return x;
            }
            string msg = $"There was no Matching Property of Type {typeof(Z).Name} found in {typeof(T).Name}\n with Condition {paramName} == {condition}";
            System.Diagnostics.Debug.WriteLine(msg);
            throw new NoMatchingPropertyFoundException(msg);
        }

        private static DbSet<T> GetPropertyMatchingTypeT(Type typeOfT)
        {
            var u = typeof(DataContext).GetProperties().SingleOrDefault(x => x.PropertyType.GetGenericArguments().Any(t => t.AssemblyQualifiedName == typeOfT.AssemblyQualifiedName));
            return (System.Data.Entity.DbSet<T>)u.GetValue(InitDB());            
        }
        #endregion

        public static void Delete(T item)
        {
            InitDB();
            var list = GetPropertyMatchingTypeT(typeof(T));
            list.Remove(item);
        }
    }

    public class NoMatchingPropertyFoundException : Exception
    {
        public NoMatchingPropertyFoundException(String ErrorText)
            :base(ErrorText)
        {

        }
    }
}