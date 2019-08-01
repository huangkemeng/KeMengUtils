using System;
using System.Collections.Generic;
using System.Text;

namespace KeMengUtils.ThrowEx
{
    public class ExpressionIsNotTureException : ApplicationException
    {

        public ExpressionIsNotTureException(string msg) : base(msg)
        {

        }
        public ExpressionIsNotTureException(string msg, Exception innerExceptiion) : base(msg, innerExceptiion)
        {

        }
        public ExpressionIsNotTureException(Type type) : base($"该表达式不是{type.Name}表达式")
        {

        }

        public ExpressionIsNotTureException(Type type, Exception innerExceptiion) : base($"该表达式不是{type.Name}表达式", innerExceptiion)
        {

        }

    }
}
