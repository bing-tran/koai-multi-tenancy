using System;

namespace Koai.MultiTenancy.Abstractions
{
    public interface IIdentityTenant<TKey>
    {
        TKey Id { get; set; }
        string Name { get; set; }
        string ConnectionString { get; set; }
    }
}
