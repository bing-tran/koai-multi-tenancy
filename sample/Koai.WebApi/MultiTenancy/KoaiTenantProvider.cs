using System;
using System.Threading.Tasks;
using Koai.MultiTenancy.Abstractions;
using Koai.MultiTenancy.Exceptions;

namespace Koai.WebApi.MultiTenancy
{
    public class KoaiTenantProvider : ITenantProvider<Tenant, int>
    {
        public KoaiTenantProvider()
        {
        }

        public Task<Tenant> GetTenantAsync(int key)
        {
            switch (key)
            {
                case 1:
                    var koaiTenant = new Tenant
                    {
                        Id = 1,
                        Name = "Koai",
                        ConnectionString = "koai_conn"
                    };
                    return Task.FromResult(koaiTenant);
                case 2:
                    var huyTenant = new Tenant
                    {
                        Id = 2,
                        Name = "Huy",
                        ConnectionString = "huy_conn"
                    };
                    return Task.FromResult(huyTenant);
            }

            throw new MultiTenantException($"Cannot find the tenant key {key}");
        }
    }
}
