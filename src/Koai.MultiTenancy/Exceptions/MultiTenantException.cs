using System;
namespace Koai.MultiTenancy.Exceptions
{
    public class MultiTenantException : Exception
    {
        public MultiTenantException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}
