using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Play.Common;
using Play.Inventory.Entities;
using Play.Inventory.Exception;
using Play.Inventory.Service.Clients;
using Play.Inventory.Service.Entities;

namespace Play.Inventory.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // private readonly IRepository<InventoryItem> inventoryItemsRepository;
        // private readonly IRepository<CatalogItem> catalogItemsRepository;

        // public ItemsController(IRepository<InventoryItem> inventoryItemsRepository, IRepository<CatalogItem> catalogItemsRepository)
        // {
        //     this.inventoryItemsRepository = inventoryItemsRepository;
        //     this.catalogItemsRepository = catalogItemsRepository;
        // }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAsync(Guid userId)
        {
            // if (userId == Guid.Empty) return BadRequest();
            // try
            // {
            //     var inventoryItemEntities = await inventoryItemsRepository.GetAllAsync(item => item.UserId == userId);
            //     if (inventoryItemEntities.Count() == 0)
            //         throw new DefaultException("UserId not exists");

            //     var itemIds = inventoryItemEntities.Select(item => item.CatalogItemId);
            //     var catalogItemEntities = await catalogItemsRepository.GetAllAsync(item => itemIds.Contains(item.Id));
            //     if (catalogItemEntities.Count() == 0)
            //         throw new DefaultException("CatalogItemId not exists");

            //     var inventoryItemDtos = catalogItemEntities.Select(catalogItem =>
            //     {
            //         var inventoryItem = inventoryItemEntities.SingleOrDefault(i => i.CatalogItemId == catalogItem.Id);
            //         return inventoryItem.AsDto(catalogItem.Name, catalogItem.Description);
            //     });

            //     return Ok(inventoryItemDtos);
            // }
            // catch (DefaultException ex)
            // {
            //     return NotFound(ex.Message);
            // }
            if (userId == Guid.Empty) return BadRequest();
            try
            {
                var inventoryItemDtos = await _mediator.Send(new GetAsyncRequest());
                return Ok(inventoryItemDtos);    
            }
            catch (DefaultException ex)
            {
                
                return NotFound(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(GrantItemsDto grantItemsDto)
        {
            var inventoryItem = await inventoryItemsRepository.GetAsync(
                item => item.UserId == grantItemsDto.UserId && item.CatalogItemId == grantItemsDto.CatalogItemId);


            if (inventoryItem == null)
            {
                inventoryItem = new InventoryItem
                {
                    CatalogItemId = grantItemsDto.CatalogItemId,
                    UserId = grantItemsDto.UserId,
                    Quantity = grantItemsDto.Quantity,
                    AcquiredDate = DateTimeOffset.UtcNow
                };

                await inventoryItemsRepository.CreateAsync(inventoryItem);
            }
            else
            {
                inventoryItem.Quantity += grantItemsDto.Quantity;
                await inventoryItemsRepository.UpdateAsync(inventoryItem);
            }

            return Ok();
        }
    }
}
