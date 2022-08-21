using System.Collections.Generic;
using System.Threading.Tasks;
using Play.Catalog.Services.Dtos;

namespace Play.Catalog.Services.Cache
{
    public interface IItemCache
    {
        void AddToCache(IEnumerable<ItemDto> items);
        Task<IEnumerable<ItemDto>> GetCachedItems();
    }
}
