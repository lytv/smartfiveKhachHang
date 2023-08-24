using System;
using Shop.Core.SharedKernel;

namespace Shop.Application.CustomerType.Responses;
public class CreatedCustomerTypeResponse : IResponse
{
    public CreatedCustomerTypeResponse(Guid id) => Id = id;

    public Guid Id { get; }
}
