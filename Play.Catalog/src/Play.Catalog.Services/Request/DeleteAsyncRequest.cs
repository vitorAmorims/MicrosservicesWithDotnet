using System;
using MediatR;
using Play.Catalog.Services.Entities;

namespace Play.Catalog.Services.Request
{
    public class DeleteAsyncRequest:IRequest<Boolean>
    {
        public Item existingItem { get; set; }
    }
}
