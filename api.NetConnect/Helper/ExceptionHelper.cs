using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace api.NetConnect.Helper
{
    public class ExceptionHelper
    {
        public static String FullException(Exception ex)
        {
            StringBuilder sb = new StringBuilder();

            if (Properties.Settings.Default.APIStatus_IsActive)
                sb.Append("- " + ex.Message + "\n");
            else
                sb.Append($"- {ex.Message}\n{ex.StackTrace}\n");
            ex = ex.InnerException;

            while(ex != null)
            {
                if(Properties.Settings.Default.APIStatus_IsActive)
                    sb.Append("- " + ex.Message + "\n");
                else
                    sb.Append($"- {ex.Message}\n{ex.StackTrace}\n");

                ex = ex.InnerException;
            }

            return sb.ToString();
        }
    }
}