using System;
using Shop.Domain.Entities.CustomerTypeAggregate;

namespace Shop.Domain.Entities.CustomerAggregate.Events;

public class CustomerDeletedEvent : CustomerBaseEvent
{
    public CustomerDeletedEvent(
        Guid id,
        string firstName,
        string lastName,
        EGender gender,
        string email,
        DateTime dateOfBirth,
        int tenantId,
        CustomerType customerType) : base(id, firstName, lastName, gender, email, dateOfBirth, tenantId, customerType)
    {
    }
}