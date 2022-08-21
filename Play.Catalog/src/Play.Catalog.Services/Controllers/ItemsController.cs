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
            return Ok(await _mediator.Send(new GetAsyncRequest()));
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetByIdAsync(Guid id)
        {
            return await _mediator.Send(new GetAsyncByIdRequest { id = id });
        }

        // POST /items
        [HttpPost]
        public async Task<ActionResult> PostAsync(CreateItemDto createItemDto)
        {
            var result = await _mediator.Send(new PostAsyncRequest { createItemDto = createItemDto });
            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = await _mediator.Send(new GetAsyncByIdRequest { id = id });
            if (existingItem.GetType() == typeof(Item))
            {
                await _mediator.Send(new PutAsyncRequest { updateItemDto = updateItemDto, existingItem = existingItem });
                return NoContent();
            }
            else if (existingItem == null)
            {
                return NotFound();
            }
            else
                return BadRequest();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var existingItem = await _mediator.Send(new GetAsyncByIdRequest { id = id });
            if (existingItem.GetType() == typeof(Item) || existingItem != null)
            {
                await _mediator.Send(new DeleteAsyncRequest { existingItem = existingItem });
                return NoContent();
            }
            else if (existingItem == null)
            {
                return NotFound();
            }
            else
                return BadRequest();
        }
    }
}
