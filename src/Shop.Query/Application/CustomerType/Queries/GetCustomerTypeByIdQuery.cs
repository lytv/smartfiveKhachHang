using System;
using Ardalis.Result;
using MediatR;
using Shop.Query.QueriesModel;

namespace Shop.Query.Application.CustomerType.Queries;
public class GetCustomerTypeByIdQuery : IRequest<Result<CustomerTypeQueryModel>>
{
    public GetCustomerTypeByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
