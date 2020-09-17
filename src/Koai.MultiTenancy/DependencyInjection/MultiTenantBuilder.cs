using System;
using Koai.MultiTenancy.Abstractions;

namespace Microsoft.Extensions.DependencyInjection
{
    public class MultiTenantBuilder<TTenant, TKey>
        where TTenant : class, IIdentityTenant<TKey>, new()
    {
        public IServiceCollection Services { get; set; }

        public MultiTenantBuilder(IServiceCollection services)
        {
            Services = services;
        }

        /// <summary>
        /// Adds and configures singleton ITenantProvider to the application using default dependency injection.
        /// </summary>>
        /// <param name="lifetime">The service lifetime.</param>
        /// <param name="parameters">a paramter list for any constructor paramaters not covered by dependency injection.</param>
        /// <returns>The same MultiTenantBuilder passed into the method.</returns>
        public MultiTenantBuilder<TTenant, TKey> WithProvider<TProvider>()
            where TProvider : ITenantProvider<TTenant, TKey>
            => WithProvider<TProvider>(ServiceLifetime.Singleton);

        /// <summary>
        /// Adds and configures a ITenantProvider to the application using default dependency injection.
        /// </summary>>
        /// <param name="lifetime">The service lifetime.</param>
        /// <param name="parameters">a paramter list for any constructor paramaters not covered by dependency injection.</param>
        /// <returns>The same MultiTenantBuilder passed into the method.</returns>
        public MultiTenantBuilder<TTenant, TKey> WithProvider<TProvider>(ServiceLifetime lifetime, params object[] parameters)
            where TProvider : ITenantProvider<TTenant, TKey>
            => WithProvider<TProvider>(lifetime, sp => ActivatorUtilities.CreateInstance<TProvider>(sp, parameters));

        /// <summary>
        /// Adds and configures a ITenantProvider to the application using a factory method.
        /// </summary>
        /// <param name="lifetime">The service lifetime.</param>
        /// <param name="factory">A delegate that will create and configure the strategy.</param>
        /// <returns>The same MultiTenantBuilder passed into the method.</returns>
        public MultiTenantBuilder<TTenant, TKey> WithProvider<TProvider>(ServiceLifetime lifetime, Func<IServiceProvider, TProvider> factory)
            where TProvider : ITenantProvider<TTenant, TKey>
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            // Note: can't use TryAddEnumerable here because ServiceDescriptor.Describe with a factory can't set implementation type.
            Services.Add(ServiceDescriptor.Describe(typeof(ITenantProvider<TTenant, TKey>), sp => factory(sp), lifetime));

            return this;
        }

        /// <summary>
        /// Adds and configures a IMultiTenantStrategy to the applicationusing default dependency injection.
        /// </summary>
        /// <param name="lifetime">The service lifetime.</param>
        /// <param name="parameters">a paramter list for any constructor paramaters not covered by dependency injection.</param>
        /// <returns>The same MultiTenantBuilder passed into the method.</returns>
        public MultiTenantBuilder<TTenant, TKey> WithStrategy<TStrategy>(ServiceLifetime lifetime, params object[] parameters)
            where TStrategy : IMultiTenantStrategy<TKey>
            => WithStrategy(lifetime, sp => ActivatorUtilities.CreateInstance<TStrategy>(sp, parameters));

        /// <summary>
        /// Adds and configures a IMultiTenantStrategy to the application using a factory method.
        /// </summary>
        /// <param name="lifetime">The service lifetime.</param>
        /// <param name="factory">A delegate that will create and configure the strategy.</param>
        /// <returns>The same MultiTenantBuilder passed into the method.</returns>
        public MultiTenantBuilder<TTenant, TKey> WithStrategy<TStrategy>(ServiceLifetime lifetime, Func<IServiceProvider, TStrategy> factory)
            where TStrategy : IMultiTenantStrategy<TKey>
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            // Potential for multiple entries per service is intended.
            Services.Add(ServiceDescriptor.Describe(typeof(IMultiTenantStrategy<TKey>), sp => factory(sp), lifetime));

            return this;
        }
    }
}
