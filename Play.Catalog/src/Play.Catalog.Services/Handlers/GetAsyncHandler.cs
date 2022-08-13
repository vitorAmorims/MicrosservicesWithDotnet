using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Play.Catalog.Service;
using Play.Catalog.Services.Dtos;
using Play.Catalog.Services.Entities;
using Play.Catalog.Services.Request;
using Play.Catalog.Services.Services;
using Play.Common;

namespace Play.Catalog.Services.Handlers
{
    public class GetAsyncHandler: IRequestHandler<GetAsyncRequest, IEnumerable<ItemDto>>
    {
        private readonly IRepository<Item> _itemsRepository;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IValidationService _validationService;

        public GetAsyncHandler(IRepository<Item> itemsRepository, IPublishEndpoint publishEndpoint, IValidationService validationService)
        {
            _itemsRepository = itemsRepository;
            this.publishEndpoint = publishEndpoint;
            _validationService = validationService;
        }

        public async Task<IEnumerable<ItemDto>> Handle(GetAsyncRequest request, CancellationToken cancellationToken)
        {
            _validationService.Validate(request);
            return (await _itemsRepository.GetAllAsync())
                         .Select(item => item.AsDto());
        }
    }
}
