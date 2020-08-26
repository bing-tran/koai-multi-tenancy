using System;
using System.Threading.Tasks;
using Koai.MultiTenancy.Abstractions;

namespace Koai.MultiTenancy
{
    public class TenantResolver<TTenant, TKey> : ITenantResolver<TTenant, TKey>, ITenantResolver
        where TTenant : class, IIdentityTenant<TKey>, new()
    {
        public IMultiTenantStrategy<TKey> Strategy { get; set; }
        public ITenantProvider<TTenant, TKey> Provider { get; set; }

        public TenantResolver(IMultiTenantStrategy<TKey> strategy, ITenantProvider<TTenant, TKey> provider)
        {
            Strategy = strategy;
            Provider = provider;
        }

        public async Task<IMultiTenantContext<TTenant, TKey>> ResolveAsync(object context)
        {
            IMultiTenantContext<TTenant, TKey> result = null;

            var tenantId = await Strategy.GetIdentifierAsync(context);
            if (!Equals(tenantId, default(TKey)))
            {
                var tenant = await Provider.GetTenantAsync(tenantId);
                if (tenant != null)
                {
                    result = new MultiTenantContext<TTenant, TKey>();
                    result.Strategy = Strategy;
                    result.Provider = Provider;
                    result.Tenant = tenant;
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
