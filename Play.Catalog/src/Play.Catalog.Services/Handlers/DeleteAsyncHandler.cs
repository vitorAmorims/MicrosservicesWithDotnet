using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Play.Catalog.Contracts;
using Play.Catalog.Services.Entities;
using Play.Catalog.Services.Request;
using Play.Catalog.Services.Services;
using Play.Common;

namespace Play.Catalog.Services.Handlers
{
    public class DeleteAsyncHandler : IRequestHandler<DeleteAsyncRequest, Boolean>
    {
        private readonly IRepository<Item> _itemsRepository;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IValidationService _validationService;

        public DeleteAsyncHandler(IRepository<Item> itemsRepository, IPublishEndpoint publishEndpoint, IValidationService validationService)
        {
            _itemsRepository = itemsRepository;
            this.publishEndpoint = publishEndpoint;
            _validationService = validationService;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public async Task<Boolean> Handle(DeleteAsyncRequest request, CancellationToken cancellationToken)
        {
            _validationService.Validate(request);

            await _itemsRepository.RemoveAsync(request.existingItem.Id);
            await publishEndpoint.Publish(new CatalogItemDeleted(request.existingItem.Id));
            return await Task.Run(() => true);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
