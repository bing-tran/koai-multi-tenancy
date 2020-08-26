using System;

namespace Koai.MultiTenancy.Abstractions
{
    public interface IMultiTenantContextAccessor
    {
        object MultiTenantContext { get; set; }
    }

    public interface IMultiTenantContextAccessor<TTenant, TKey>
        where TTenant : class, IIdentityTenant<TKey>, new()
    {
        IMultiTenantContext<TTenant, TKey> MultiTenantContext { get; set; }
    }
}
