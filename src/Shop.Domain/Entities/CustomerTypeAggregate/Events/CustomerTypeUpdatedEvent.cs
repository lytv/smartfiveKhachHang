using System;

namespace Shop.Domain.Entities.CustomerTypeAggregate.Events;
public class CustomerTypeUpdatedEvent : CustomerTypeBaseEvent
{
    public CustomerTypeUpdatedEvent(
        Guid id,
        string customerTypeCode,
        string description,
        int tenantId) : base(id, customerTypeCode, description, tenantId)
    {
    }
}
