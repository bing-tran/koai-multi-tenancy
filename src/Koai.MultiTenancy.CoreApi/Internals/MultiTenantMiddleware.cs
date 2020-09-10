using System.Threading.Tasks;
using Koai.MultiTenancy.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Koai.MultiTenancy.CoreApi
{
    /// <summary>
    /// Middleware for resolving the TenantContext and storing it in HttpContext.
    /// </summary>
    internal class MultiTenantMiddleware
    {
        private readonly RequestDelegate _next;

        public MultiTenantMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var accessor = context.RequestServices.GetRequiredService<IMultiTenantContextAccessor>();

            if (accessor.MultiTenantContext == null)
            {
                var resolver = context.RequestServices.GetRequiredService<ITenantResolver>();
                var multiTenantContext = await resolver.ResolveAsync(context);
                accessor.MultiTenantContext = multiTenantContext;
            }

            if (_next != null)
            {
                await _next(context);
            }
        }
    }
}
