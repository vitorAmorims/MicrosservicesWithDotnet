using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Initializers;
using MediatR;
using Play.Catalog.Service;
using Play.Catalog.Services.Dtos;
using Play.Catalog.Services.Entities;
using Play.Catalog.Services.Request;
using Play.Catalog.Services.Services;
using Play.Common;

namespace Play.Catalog.Services.Handlers
{
    public class GetAsyncByIdHandler : IRequestHandler<GetAsyncByIdRequest, ItemDto>
    {
        private readonly IRepository<Item> _itemsRepository;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IValidationService _validationService;

        public GetAsyncByIdHandler(IRepository<Item> itemsRepository, IPublishEndpoint publishEndpoint, IValidationService validationService)
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

        public async Task<ItemDto> Handle(GetAsyncByIdRequest request, CancellationToken cancellationToken)
        {
            _validationService.Validate<Guid>(request.id);

            return (await _itemsRepository.GetAsync(request.id)
                .Select(item => item.AsDto()));

        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
