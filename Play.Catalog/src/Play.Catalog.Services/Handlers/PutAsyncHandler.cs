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
    public class PutAsyncHandler : IRequestHandler<PutAsyncRequest, Item>
    {
        private readonly IRepository<Item> _itemsRepository;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IValidationService _validationService;

        public PutAsyncHandler(IRepository<Item> itemsRepository, IPublishEndpoint publishEndpoint, IValidationService validationService)
        {
            _itemsRepository = itemsRepository;
            this.publishEndpoint = publishEndpoint;
            _validationService = validationService;
        }

        public async Task<Item> Handle(PutAsyncRequest request, CancellationToken cancellationToken)
        {
            _validationService.Validate(request);

            request.existingItem.Name = request.updateItemDto.Name;
            request.existingItem.Description = request.updateItemDto.Description;
            request.existingItem.Price = request.updateItemDto.Price;
            
            await _itemsRepository.UpdateAsync(request.existingItem);
            await publishEndpoint.Publish(new CatalogItemUpdated(request.existingItem.Id, request.existingItem.Name, request.existingItem.Description));
            
            return await Task.Run(() => request.existingItem);            
        }
    }
}
