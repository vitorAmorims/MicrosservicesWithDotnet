using System.Collections.Generic;
using System.Threading;
using MediatR;
using Play.Common;
using Play.Inventory.Entities;
using Play.Inventory.Service.Entities;
using Play.Inventory.Service.Request;
using Play.Inventory.Service.Services;

namespace Play.Inventory.Service.Handlers
{
    public class GetAsyncByIdHandler : IRequestHandler<GetAsyncRequest, IEnumerable<InventoryItemDto>>
    {
        private readonly IRepository<InventoryItem> inventoryItemsRepository;
        private readonly IRepository<CatalogItem> catalogItemsRepository;
        private readonly IValidationService _validationService;

        public GetAsyncByIdHandler(IRepository<InventoryItem> inventoryItemsRepository, IRepository<CatalogItem> catalogItemsRepository, IValidationService validationService)
        {
            this.inventoryItemsRepository = inventoryItemsRepository;
            this.catalogItemsRepository = catalogItemsRepository;
            _validationService = validationService;
        }

        public System.Threading.Tasks.Task<IEnumerable<InventoryItemDto>> Handle(GetAsyncRequest request, CancellationToken cancellationToken)
        {
            
        }
    }
}
