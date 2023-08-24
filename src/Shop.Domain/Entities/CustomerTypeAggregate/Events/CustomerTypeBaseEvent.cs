using System;
using Shop.Core.SharedKernel;

namespace Shop.Domain.Entities.CustomerTypeAggregate.Events;
public abstract class CustomerTypeBaseEvent : BaseEvent
{
    protected CustomerTypeBaseEvent(Guid id, string customerTypeCode, string description, int tenantId)
    {
        Id = id;
        AggregateId = id;
        CustomerTypeCode = customerTypeCode;
        Description = description;
        TenantId = tenantId;
    }

    public Guid Id { get; private init; }
    public string CustomerTypeCode { get; private init; }
    public string Description { get; private init; }
    public int TenantId { get; private init; }
}
