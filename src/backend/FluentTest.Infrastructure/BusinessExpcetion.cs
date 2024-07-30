using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FluentTest.Infrastructure
{
    public sealed class BusinessExpcetion : Exception
    {
        public BusinessExpcetion() : base()
        {
        }

        public BusinessExpcetion(string message) : base(message)
        {
        }

        public BusinessExpcetion(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
