using System;
using System.Collections.Generic;
using MediatR;

namespace Play.Inventory.Service.Request
{
    public class GetAsyncRequest:IRequest<IEnumerable<InventoryItemDto>>
    {
        public Guid id { get; set; }
    }
}
