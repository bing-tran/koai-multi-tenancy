using System;
using Koai.MultiTenancy.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Koai.MultiTenancy
{
    public static class HttpContextExtension
    {
        /// <summary>
        /// Returns the current MultiTenantContext or null if there is none.
        /// </summary>
        public static IMultiTenantContext<TTenant, TKey> GetMultiTenantContext<TTenant, TKey>(this HttpContext httpContext)
            where TTenant : class, IIdentityTenant<TKey>, new()
        {
            return httpContext.RequestServices.GetRequiredService<IMultiTenantContextAccessor<TTenant, TKey>>().MultiTenantContext;
        }

        /// <summary>
        /// Sets the provided Tenant info on the MultiTenantContext.
        /// Sets Strategy and Provider on the MultiTenant Context to null.
        /// Optionally resets the current dependency injection service provider.
        /// </summary>
        public static bool TrySetTenantInfo<TTenant, TKey>(this HttpContext httpContext, TTenant tenant, bool resetServiceProviderScope)
            where TTenant : class, IIdentityTenant<TKey>, new()
        {
            if (resetServiceProviderScope)
                httpContext.RequestServices = httpContext.RequestServices.CreateScope().ServiceProvider;

            var multitenantContext = new MultiTenantContext<TTenant, TKey>
            {
                Tenant = tenant,
                Strategy= null,
                Provider = null
            };

            var accessor = httpContext.RequestServices.GetRequiredService<IMultiTenantContextAccessor<TTenant, TKey>>();
            accessor.MultiTenantContext = multitenantContext;

            return true;
        }
    }
}
