﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Koai.MultiTenancy.Abstractions;
using Koai.MultiTenancy.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Koai.MultiTenancy.WebApi.Strategies
{
    public class HttpHeaderAttrStrategy<TKey> : IMultiTenantStrategy<TKey>
    {
        private readonly string _tenantAttrKey;

        public HttpHeaderAttrStrategy(string tenantAttrKey)
        {
            _tenantAttrKey = tenantAttrKey;
        }

        public Task<TKey> GetIdentifierAsync(object context)
        {
            if (!(context is HttpContext httpContext))
                throw new MultiTenantException(null, new ArgumentException($@"""{nameof(context)}"" type must be of type HttpContext", nameof(context)));

            var hasTenant = httpContext.Request.Headers.TryGetValue(_tenantAttrKey, out StringValues tenantIdentifierStrVal);
            if (hasTenant && !StringValues.IsNullOrEmpty(tenantIdentifierStrVal))
            {
                var identityStrVal = tenantIdentifierStrVal.ToString();
                var converter = TypeDescriptor.GetConverter(typeof(TKey));
                if (converter.IsValid(identityStrVal))
                {
                    //var tenantIdentifier = (TKey)Convert.ChangeType(tenantIdentifierStrVal.ToString(), typeof(TKey));
                    var tenantIdentifier = (TKey)converter.ConvertFrom(identityStrVal);
                    return Task.FromResult(tenantIdentifier);
                }
            }

            return Task.FromResult<TKey>(default);
        }
    }
}
