using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnsekTest.Service.Common.Exceptions.Base
{
    public abstract class ServiceException : Exception
    {
        protected ServiceException()
        {
        }

        protected ServiceException(string message) : base(message)
        {
        }

        protected ServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}