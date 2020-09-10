using System.Collections.Generic;
using System.Threading.Tasks;

namespace Koai.MultiTenancy.Abstractions
{
    public interface ITenantResolver
    {
        Task<object> ResolveAsync(object context);
    }

    public interface ITenantResolver<TTenant, TKey>
        where TTenant : class, IIdentityTenant<TKey>, new()
    {
        IEnumerable<IMultiTenantStrategy<TKey>> Strategies { get; set; }
        ITenantProvider<TTenant, TKey> Provider { get; set; }

        Task<IMultiTenantContext<TTenant, TKey>> ResolveAsync(object context);
    }
}
