using AutoMapper;
using Shop.Domain.Entities.CustomerAggregate.Events;
using Shop.Domain.Entities.CustomerTypeAggregate.Events;
using Shop.Query.QueriesModel;

namespace Shop.Query.Profiles;

public class EventToQueryModelProfile : Profile
{
    public EventToQueryModelProfile()
    {
        CreateMap<CustomerCreatedEvent, CustomerQueryModel>(MemberList.Destination)
            .ConstructUsing(@event => Create(@event));

        CreateMap<CustomerUpdatedEvent, CustomerQueryModel>(MemberList.Destination)
            .ConstructUsing(@event => Create(@event));

        CreateMap<CustomerDeletedEvent, CustomerQueryModel>(MemberList.Destination)
            .ConstructUsing(@event => Create(@event));

        CreateMap<CustomerTypeCreatedEvent, CustomerTypeQueryModel>(MemberList.Destination)
            .ConstructUsing(@event => Create(@event));

        CreateMap<CustomerTypeUpdatedEvent, CustomerTypeQueryModel>(MemberList.Destination)
            .ConstructUsing(@event => Create(@event));
    }

    private static CustomerQueryModel Create(CustomerBaseEvent @event)
    {
        var customerType = new CustomerTypeQueryModel(@event.CustomerType.Id, @event.CustomerType.CustomerTypeCode, @event.CustomerType.Description, @event.CustomerType.TenantId);
        return new CustomerQueryModel(@event.Id, @event.FirstName, @event.LastName, @event.Gender.ToString(), @event.Email, @event.DateOfBirth, customerType, @event.TenantId);
    }

    private static CustomerTypeQueryModel Create(CustomerTypeBaseEvent @event) =>
        new(@event.Id, @event.CustomerTypeCode, @event.Description, @event.TenantId);
}