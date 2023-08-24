using System;
using Ardalis.Result;
using MediatR;

namespace Shop.Application.CustomerType.Commands;
public class DeleteCustomerTypeCommand : IRequest<Result>
{
    public DeleteCustomerTypeCommand(Guid id) => Id = id;

    public Guid Id { get; set; }
}
