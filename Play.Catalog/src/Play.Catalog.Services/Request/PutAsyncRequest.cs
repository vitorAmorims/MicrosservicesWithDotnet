using MediatR;
using Play.Catalog.Services.Dtos;
using Play.Catalog.Services.Entities;

namespace Play.Catalog.Services.Request
{
    public class PutAsyncRequest : IRequest<Item>
    {
        public UpdateItemDto updateItemDto { get; set; }
        public Item existingItem { get; set; }
    }
}
