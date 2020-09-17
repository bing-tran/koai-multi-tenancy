using System;
using Koai.MultiTenancy.Abstractions;

namespace Koai.WebApi.MultiTenancy
{
    public class Tenant : IIdentityTenant<int>
    {
        public Tenant()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }
}
