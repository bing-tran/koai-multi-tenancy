using System;
using System.Threading;
using Koai.MultiTenancy.Abstractions;

namespace Koai.MultiTenancy.Core
{
    public class MultiTenantContextAccessor<TTenant, TKey> : IMultiTenantContextAccessor<TTenant, TKey>, IMultiTenantContextAccessor
        where TTenant : class, IIdentityTenant<TKey>, new()
    {
        private static AsyncLocal<IMultiTenantContext<TTenant, TKey>> _asyncLocalContext = new AsyncLocal<IMultiTenantContext<TTenant, TKey>>();

        public IMultiTenantContext<TTenant, TKey> MultiTenantContext
        {
            get
            {
                return _asyncLocalContext.Value;
            }

            set
            {
                _asyncLocalContext.Value = value;
            }
        }

        object IMultiTenantContextAccessor.MultiTenantContext
        {
            get => MultiTenantContext;
            set => MultiTenantContext = value as IMultiTenantContext<TTenant, TKey> ?? MultiTenantContext;
        }
    }
}
