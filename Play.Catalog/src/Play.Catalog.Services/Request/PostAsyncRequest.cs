using MediatR;
using Play.Catalog.Services.Dtos;
using Play.Catalog.Services.Entities;

namespace Play.Catalog.Services.Request
{
    public class PostAsyncRequest: IRequest<Item>
    {
        public CreateItemDto createItemDto {get; set; }

    }
}
