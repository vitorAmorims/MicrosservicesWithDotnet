using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Play.Catalog.Service;
using Play.Catalog.Services.Dtos;
using Play.Catalog.Services.Entities;
using Play.Common;

namespace Play.Catalog.Services.Cache
{
    public class ItemCache : IItemCache
    {
        private const string KEY = "item_cache";
        private readonly IMemoryCache cache;
        private readonly IRepository<Item> _itemsRepository;

        public ItemCache(IMemoryCache cache, IRepository<Item> itemsRepository)
        {
            this.cache = cache;
            _itemsRepository = itemsRepository;
        }

        public void AddToCache(IEnumerable<ItemDto> items)
        {
            //get data from a database
            var option = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(30),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(90)
            };
            cache.Set<IEnumerable<ItemDto>>(KEY, items, option);
        }

        public async Task<IEnumerable<ItemDto>> GetCachedItems()
        {
            IEnumerable<ItemDto> items;
            if (!cache.TryGetValue(KEY, out items))
            {
                items = (await _itemsRepository.GetAllAsync())
                         .Select(item => item.AsDto());

                AddToCache(items);
            }
            return items;
        }
    }
}
