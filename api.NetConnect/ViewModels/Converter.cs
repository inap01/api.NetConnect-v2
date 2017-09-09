using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace api.NetConnect.ViewModels
{
    public class Converter
    {
        protected static String _connectionString => ConfigurationManager.ConnectionStrings["ConnectionStringDev"].ConnectionString;
    }
}