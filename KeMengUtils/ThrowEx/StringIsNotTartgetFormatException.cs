using System;
using System.Collections.Generic;
using System.Text;

namespace KeMengUtils.ThrowEx
{
    public class StringIsNotTartgetFormatException : ApplicationException
    {
        public StringIsNotTartgetFormatException(string msg) : base(msg)
        {

        }
        public StringIsNotTartgetFormatException(string msg, Exception innerException) : base(msg, innerException)
        {

        }
        public StringIsNotTartgetFormatException(Type type) : base($"字符串不符合{type.Name}类型的格式！")
        {

        }
        public StringIsNotTartgetFormatException(Type type, Exception innerException) : base($"字符串不符合{type.Name}类型的格式！", innerException)
        {

        }
    }
}
