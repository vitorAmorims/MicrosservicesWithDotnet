using System.Collections.Generic;
using MediatR;
using Play.Catalog.Services.Dtos;
using Play.Catalog.Services.Entities;

namespace Play.Catalog.Services.Request
{
    public class GetAsyncRequest: IRequest<IEnumerable<ItemDto>>
    {
        public GetAsyncRequest()
        {
        }

        public IEnumerable<ItemDto> Items { get; private set; }
    }
}
