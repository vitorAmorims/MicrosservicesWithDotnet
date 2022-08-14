using System;
using Play.Catalog.Services.Dtos;
using Play.Common;

namespace Play.Catalog.Services.Entities
{
    public class Item: IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public static implicit operator Item(ItemDto v)
        {
            throw new NotImplementedException();
        }
    }
}
