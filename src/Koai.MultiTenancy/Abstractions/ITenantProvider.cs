using System;
using System.Threading.Tasks;

namespace Koai.MultiTenancy.Abstractions
{
    public interface ITenantProvider<TTenant, TKey>
        where TTenant : class, IIdentityTenant<TKey>, new()
    {
        Task<TTenant> GetTenantAsync(TKey key);
    }
}
