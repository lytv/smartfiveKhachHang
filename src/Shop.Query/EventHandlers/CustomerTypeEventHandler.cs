using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Shop.Core.SharedKernel;
using Shop.Domain.Entities.CustomerTypeAggregate.Events;
using Shop.Query.Abstractions;
using Shop.Query.Application.CustomerType.Queries;
using Shop.Query.QueriesModel;

namespace Shop.Query.EventHandlers;
public class CustomerTypeEventHandler :
    INotificationHandler<CustomerTypeCreatedEvent>,
    INotificationHandler<CustomerTypeUpdatedEvent>,
    INotificationHandler<CustomerTypeDeletedEvent>
{
    private readonly ICacheService _cacheService;
    private readonly ILogger<CustomerTypeEventHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IReadDbContext _readDbContext;

    public CustomerTypeEventHandler(ICacheService cacheService, ILogger<CustomerTypeEventHandler> logger, IMapper mapper, IReadDbContext readDbContext)
    {
        _cacheService = cacheService;
        _logger = logger;
        _mapper = mapper;
        _readDbContext = readDbContext;
    }

    public async Task Handle(CustomerTypeCreatedEvent notification, CancellationToken cancellationToken)
    {
        LogEvent(notification);

        var customerTypeQueryModel = _mapper.Map<CustomerTypeQueryModel>(notification);
        await _readDbContext.UpsertAsync(customerTypeQueryModel, filter => filter.Id == customerTypeQueryModel.Id);

        await ClearCacheAsync(notification);
    }

    public async Task Handle(CustomerTypeUpdatedEvent notification, CancellationToken cancellationToken)
    {
        LogEvent(notification);

        var customerTypeQueryModel = _mapper.Map<CustomerTypeQueryModel>(notification);
        await _readDbContext.UpsertAsync(customerTypeQueryModel, filter => filter.Id == customerTypeQueryModel.Id);

        await ClearCacheAsync(notification);
    }

    public async Task Handle(CustomerTypeDeletedEvent notification, CancellationToken cancellationToken)
    {
        LogEvent(notification);

        await _readDbContext.DeleteAsync<CustomerTypeQueryModel>(filter => filter.Id == notification.Id);

        await ClearCacheAsync(notification);
    }

    private async Task ClearCacheAsync(CustomerTypeBaseEvent @event)
    {
        var cacheKeys = new[] { nameof(GetAllCustomerTypeQuery), $"{nameof(GetCustomerTypeByIdQuery)}_{@event.Id}" };
        await _cacheService.RemoveAsync(cacheKeys);
    }

    private void LogEvent<TEvent>(TEvent @event) where TEvent : CustomerTypeBaseEvent =>
        _logger.LogInformation("----- Triggering the event {EventName}, model: {EventModel}", typeof(TEvent).Name, @event.ToJson());
}
