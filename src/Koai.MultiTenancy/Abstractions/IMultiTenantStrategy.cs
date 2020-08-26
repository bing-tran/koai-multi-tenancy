using System;
using System.Threading.Tasks;

namespace Koai.MultiTenancy.Abstractions
{
    public interface IMultiTenantStrategy<TKey>
    {
        Task<TKey> GetIdentifierAsync(object context);
    }
}
