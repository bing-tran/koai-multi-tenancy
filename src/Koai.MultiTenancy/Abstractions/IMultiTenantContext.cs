using System;

namespace Koai.MultiTenancy.Abstractions
{
    public interface IMultiTenantContext<TTenant, TKey>
        where TTenant : class, IIdentityTenant<TKey>, new()
    {
        TTenant Tenant { get; set; }
        IMultiTenantStrategy<TKey> Strategy { get; set; }
        ITenantProvider<TTenant, TKey> Provider { get; set; }
    }
}
