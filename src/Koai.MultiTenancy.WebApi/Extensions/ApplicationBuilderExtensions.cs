using Koai.MultiTenancy.WebApi;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Use multi-tenancy middleware in processing the request.
        /// </summary>
        /// <param name="builder">The IApplicationBuilder<c/> instance the extension method applies to.</param>
        /// <returns>The same IApplicationBuilder passed into the method.</returns>
        public static IApplicationBuilder UseMultiTenantcy(this IApplicationBuilder builder)
            => builder.UseMiddleware<MultiTenantMiddleware>();
    }
}
