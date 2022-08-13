using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Contracts;
using Play.Catalog.Services.Dtos;
using Play.Catalog.Services.Entities;
using Play.Catalog.Services.Request;
using Play.Common;

namespace Play.Catalog.Service.Controllers
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAsync()
        {
            var items = await _mediator.Send(new GetAsyncRequest());
            return Ok(items);
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            return await _mediator.Send(new GetAsyncByIdRequest{ id = id});
        }

        // POST /items
        [HttpPost]
        public async Task<ActionResult> PostAsync(CreateItemDto createItemDto)
        {
            var result = await _mediator.Send(new PostAsyncRequest{ createItemDto = createItemDto });
            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id}, result);
        }

        // PUT /items/{id}
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
        // {
        //     var existingItem = await _itemsRepository.GetAsync(id);

        //     if (existingItem == null)
        //     {
        //         return NotFound();
        //     }

        //     existingItem.Name = updateItemDto.Name;
        //     existingItem.Description = updateItemDto.Description;
        //     existingItem.Price = updateItemDto.Price;

        //     await _itemsRepository.UpdateAsync(existingItem);
        //     await publishEndpoint.Publish(new CatalogItemUpdated(existingItem.Id, existingItem.Name, existingItem.Description));

        //     return NoContent();
        // }

        // DELETE /items/{id}
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteAsync(Guid id)
        // {
        //     var item = await _itemsRepository.GetAsync(id);

        //     if (item == null)
        //     {
        //         return NotFound();
        //     }

        //     await _itemsRepository.RemoveAsync(item.Id);
        //     await publishEndpoint.Publish(new CatalogItemDeleted(id));

        //     return NoContent();
        // }
    }
}
