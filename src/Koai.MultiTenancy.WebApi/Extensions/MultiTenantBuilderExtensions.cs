using System;
using Koai.MultiTenancy.Abstractions;
using Koai.MultiTenancy.WebApi.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MultiTenantBuilderExtensions
    {
        /// <summary>
        /// Adds and configures a ClaimStrategy with tenant claim type key to the application.
        /// </summary>
        /// <returns>The same MultiTenantBuilder passed into the method.</returns>
        public static MultiTenantBuilder<TTenant, TKey> WithClaimStrategy<TTenant, TKey>(this MultiTenantBuilder<TTenant, TKey> builder)
            where TTenant : class, IIdentityTenant<TKey>, new()
        {
            return builder.WithStrategy<ClaimStrategy<TKey>>(ServiceLifetime.Singleton, "__tenant__");
        }

        /// <summary>
        /// Adds and configures a ClaimStrategy to the application.
        /// </summary>
        /// <param name="tenantClaimTypeKey">The claim type key for determining the tenant identifier in the claims identity.</param>
        /// <returns>The same MultiTenantBuilder passed into the method.</returns>
        public static MultiTenantBuilder<TTenant, TKey> WithClaimStrategy<TTenant, TKey>(this MultiTenantBuilder<TTenant, TKey> builder, string tenantClaimTypeKey)
            where TTenant : class, IIdentityTenant<TKey>, new()
        {
            return builder.WithStrategy<ClaimStrategy<TKey>>(ServiceLifetime.Singleton, tenantClaimTypeKey);
        }

        /// <summary>
        /// Adds and configures a HttpHeaderAttrStrategy with tenant http request header attribute to the application.
        /// </summary>
        /// <returns>The same MultiTenantBuilder passed into the method.</returns>
        public static MultiTenantBuilder<TTenant, TKey> WithHttpHeaderAttributeStrategy<TTenant, TKey>(this MultiTenantBuilder<TTenant, TKey> builder)
            where TTenant : class, IIdentityTenant<TKey>, new()
        {
            return builder.WithStrategy<HttpHeaderAttrStrategy<TKey>>(ServiceLifetime.Singleton, "x-tenant-id");
        }

        /// <summary>
        /// Adds and configures a HttpHeaderAttrStrategy to the application.
        /// </summary>
        /// <param name="httpHeaderAttributeKey">The http request header attribute key for determining the tenant identifier in the http request header attribute.</param>
        /// <returns>The same MultiTenantBuilder passed into the method.</returns>
        public static MultiTenantBuilder<TTenant, TKey> WithHeaderAttributeStrategy<TTenant, TKey>(this MultiTenantBuilder<TTenant, TKey> builder,
            string httpHeaderAttributeKey)
            where TTenant : class, IIdentityTenant<TKey>, new()
        {
            return builder.WithStrategy<HttpHeaderAttrStrategy<TKey>>(ServiceLifetime.Singleton, httpHeaderAttributeKey);
        }
    }
}
