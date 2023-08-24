using System;
using Shop.Core.SharedKernel;
using Shop.Domain.Entities.CustomerTypeAggregate;

namespace Shop.Domain.Entities.CustomerAggregate.Events;

public abstract class CustomerBaseEvent : BaseEvent
{
    protected CustomerBaseEvent(
        Guid id,
        string firstName,
        string lastName,
        EGender gender,
        string email,
        DateTime dateOfBirth,
        int tenantId,
        CustomerType customerType)
    {
        Id = id;
        AggregateId = id;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Email = email;
        DateOfBirth = dateOfBirth;
        TenantId = tenantId;
        CustomerType = customerType;
    }

    public Guid Id { get; private init; }
    public string FirstName { get; private init; }
    public string LastName { get; private init; }
    public EGender Gender { get; private init; }
    public string Email { get; private init; }
    public DateTime DateOfBirth { get; private init; }
    public int TenantId { get; private init; }
    public CustomerType CustomerType { get; private init; }
}