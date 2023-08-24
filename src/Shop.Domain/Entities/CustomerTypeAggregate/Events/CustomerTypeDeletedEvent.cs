using System;

namespace Shop.Domain.Entities.CustomerTypeAggregate.Events;
public class CustomerTypeDeletedEvent : CustomerTypeBaseEvent
{
    public CustomerTypeDeletedEvent(
        Guid id,
        string customerTypeCode,
        string description,
        int tenantId) : base(id, customerTypeCode, description, tenantId)
    {
    }
}
