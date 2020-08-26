using System;
using Koai.MultiTenancy.Abstractions;

namespace Koai.MultiTenancy
{
    public class MultiTenantContext<TTenant, TKey> : IMultiTenantContext<TTenant, TKey>
        where TTenant : class, IIdentityTenant<TKey>, new()
    {
        public TTenant Tenant { get; set; }
        public IMultiTenantStrategy<TKey> Strategy { get; set; }
        public ITenantProvider<TTenant, TKey> Provider { get; set; }
    }
}
