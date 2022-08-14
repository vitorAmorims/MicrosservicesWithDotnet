using System;
using MediatR;
using Play.Catalog.Services.Dtos;
using Play.Catalog.Services.Entities;

namespace Play.Catalog.Services.Request
{
    public class GetAsyncByIdRequest: IRequest<Item>
    {
        public Guid id { get; set; }
    }
}
