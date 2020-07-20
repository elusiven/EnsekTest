using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnsekTest.Service.Common.Exceptions.Base;

namespace EnsekTest.Service.Common.Exceptions
{
    public class AccountServiceException : ServiceException
    {
        public AccountServiceException(string message) : base(message)
        {
        }

        public AccountServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}