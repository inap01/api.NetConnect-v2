using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.Converters
{
    public static partial class ConverterExtensions
    {
        private static DateTime ByteArrayToDateTime(Byte[] input)
        {
            long longVar = BitConverter.ToInt64(input, 0);
            return new DateTime(1980, 1, 1).AddMilliseconds(longVar);
        }
    }
}