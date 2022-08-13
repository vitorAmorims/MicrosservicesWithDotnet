using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Initializers;
using MediatR;
using Play.Catalog.Contracts;
using Play.Catalog.Service;
using Play.Catalog.Services.Dtos;
using Play.Catalog.Services.Entities;
using Play.Catalog.Services.Request;
using Play.Catalog.Services.Services;
using Play.Common;

namespace Play.Catalog.Services.Handlers
{
    public class PostAsynHandler : IRequestHandler<PostAsyncRequest, Item>
    {
        private readonly IRepository<Item> _itemsRepository;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IValidationService _validationService;

        public PostAsynHandler(IRepository<Item> itemsRepository, IPublishEndpoint publishEndpoint, IValidationService validationService)
        {
            _itemsRepository = itemsRepository;
            this.publishEndpoint = publishEndpoint;
            _validationService = validationService;
        }

        public async Task<Item> Handle(PostAsyncRequest request, CancellationToken cancellationToken)
        {
            var item = new Item
            {
                Name = request.createItemDto.Name,
                Description = request.createItemDto.Description,
                Price = request.createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            _validationService.Validate(request);
            await _itemsRepository.CreateAsync(item);
            await publishEndpoint.Publish(new CatalogItemCreated(item.Id, item.Name, item.Description));
            return await _itemsRepository.GetAsync(item.Id);
        }
    }
}
