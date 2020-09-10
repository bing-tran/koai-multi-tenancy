using System;
using System.Threading.Tasks;
using Koai.MultiTenancy.Abstractions;
using Koai.MultiTenancy.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Koai.MultiTenancy.CoreApi.Strategies
{
	public class ClaimStrategy<TKey> : IMultiTenantStrategy<TKey>
	{
		private readonly string _tenantClaimTypeKey;
		public ClaimStrategy(string tenantClaimTypeKey)
		{
			if (string.IsNullOrWhiteSpace(tenantClaimTypeKey))
				throw new ArgumentException(nameof(tenantClaimTypeKey));

			_tenantClaimTypeKey = tenantClaimTypeKey;
		}

		public async Task<TKey> GetIdentifierAsync(object context)
		{
			if (!(context is HttpContext httpContext))
				throw new MultiTenantException(null, new ArgumentException($@"""{nameof(context)}"" type must be of type HttpContext", nameof(context)));

			var claimValue = await Task.FromResult(httpContext.User.FindFirst(_tenantClaimTypeKey)?.Value);
            if (string.IsNullOrEmpty(claimValue))
            {
				return default;
            }

			return (TKey)Convert.ChangeType(claimValue, typeof(TKey));
		}
	}
}
