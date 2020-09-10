using System;
using Koai.MultiTenancy;
using Koai.MultiTenancy.Abstractions;
using Koai.MultiTenancy.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configure MultiTenancy services for the application.
        /// </summary>
        /// <param name="services">The IServiceCollection<c/> instance the extension method applies to.</param>
        /// <returns>An new instance of MultiTenantBuilder.</returns>
        public static MultiTenantBuilder<TTenant, TKey> AddMultiTenant<TTenant, TKey>(this IServiceCollection services)
            where TTenant : class, IIdentityTenant<TKey>, new()
        {
            return services.AddMultiTenant<TTenant, TKey>(_ => { });
        }

        /// <summary>
        /// Configure MultiTenancy services for the application.
        /// </summary>
        /// <param name="services">The IServiceCollection<c/> instance the extension method applies to.</param>
        /// <param name="config">An action to configure the MultiTenantOptions instance.</param>
        /// <returns>An new instance of MultiTenantBuilder.</returns>
        public static MultiTenantBuilder<TTenant, TKey> AddMultiTenant<TTenant, TKey>(this IServiceCollection services, Action<MultiTenantOptions> config)
            where TTenant : class, IIdentityTenant<TKey>, new()
        {
            services.AddScoped<ITenantResolver<TTenant, TKey>, TenantResolver<TTenant, TKey>>();
            services.AddScoped<ITenantResolver>(sp => (ITenantResolver)sp.GetRequiredService<ITenantResolver<TTenant, TKey>>());

            services.AddScoped<IMultiTenantContext<TTenant, TKey>>(sp => sp.GetRequiredService<IMultiTenantContextAccessor<TTenant, TKey>>().MultiTenantContext);

            services.AddScoped<TTenant>(sp => sp.GetRequiredService<IMultiTenantContextAccessor<TTenant, TKey>>().MultiTenantContext?.Tenant);
            services.AddScoped<TTenant>(sp => sp.GetService<TTenant>());

            services.AddSingleton<IMultiTenantContextAccessor<TTenant, TKey>, MultiTenantContextAccessor<TTenant, TKey>>();
            services.AddSingleton<IMultiTenantContextAccessor>(sp => (IMultiTenantContextAccessor)sp.GetRequiredService<IMultiTenantContextAccessor<TTenant, TKey>>());

            services.Configure<MultiTenantOptions>(config);

            return new MultiTenantBuilder<TTenant, TKey>(services);
        }
    }
}
