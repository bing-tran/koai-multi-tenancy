using System.Collections.Generic;
using System.Threading.Tasks;
using Koai.MultiTenancy.Abstractions;

namespace Koai.MultiTenancy
{
    public class TenantResolver<TTenant, TKey> : ITenantResolver<TTenant, TKey>, ITenantResolver
        where TTenant : class, IIdentityTenant<TKey>, new()
    {
        public IEnumerable<IMultiTenantStrategy<TKey>> Strategies { get; set; }
        public ITenantProvider<TTenant, TKey> Provider { get; set; }

        public TenantResolver(IEnumerable<IMultiTenantStrategy<TKey>> strategies, ITenantProvider<TTenant, TKey> provider)
        {
            Strategies = strategies;
            Provider = provider;
        }

        public async Task<IMultiTenantContext<TTenant, TKey>> ResolveAsync(object context)
        {
            IMultiTenantContext<TTenant, TKey> result = null;

            foreach (var strategy in Strategies)
            {
                var tenantIdentifier = await strategy.GetIdentifierAsync(context);
                if (!Equals(tenantIdentifier, default(TKey)))
                {
                    var tenant = await Provider.GetTenantAsync(tenantIdentifier);
                    if (tenant != null)
                    {
                        return new MultiTenantContext<TTenant, TKey>
                        {
                            Strategy = strategy,
                            Provider = Provider,
                            Tenant = tenant
                        };
                    }
                }
            }

            return result;
        }

        async Task<object> ITenantResolver.ResolveAsync(object context)
        {
            var multiTenantContext = await ResolveAsync(context);
            return multiTenantContext;
        }
    }
}
