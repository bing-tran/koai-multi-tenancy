using System;
using System.Threading.Tasks;
using Koai.MultiTenancy.Abstractions;
using Koai.MultiTenancy.Strategies;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MultiTenantBuilderExtensions
    {

        /// <summary>
        /// Adds and configures a StaticStrategy to the application.
        /// </summary>
        /// <param name="identifier">The tenant identifier to use for all tenant resolution.</param>
        public static MultiTenantBuilder<TTenant, TKey> WithStaticStrategy<TTenant, TKey>(this MultiTenantBuilder<TTenant, TKey> builder,
            string identifier)
            where TTenant : class, IIdentityTenant<TKey>, new()
        {
            if (string.IsNullOrWhiteSpace(identifier))
            {
                throw new ArgumentException("Invalid value for \"identifier\"", nameof(identifier));
            }

            return builder.WithStrategy<StaticStrategy<TKey>>(ServiceLifetime.Singleton, new object[] { identifier }); ;
        }

        /// <summary>
        /// Adds and configures a DelegateStrategy to the application.
        /// </summary>
        /// <param name="doStrategy">The delegate implementing the strategy.</returns>
        public static MultiTenantBuilder<TTenant, TKey> WithDelegateStrategy<TTenant, TKey>(this MultiTenantBuilder<TTenant, TKey> builder,
            Func<object, Task<TKey>> doStrategy)
            where TTenant : class, IIdentityTenant<TKey>, new()
        {
            if (doStrategy == null)
            {
                throw new ArgumentNullException(nameof(doStrategy));
            }

            return builder.WithStrategy<DelegateStrategy<TKey>>(ServiceLifetime.Singleton, new object[] { doStrategy });
        }
    }
}
