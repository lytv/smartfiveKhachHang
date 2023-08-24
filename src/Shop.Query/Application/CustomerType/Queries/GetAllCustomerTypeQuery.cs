

using System.Collections.Generic;
using Ardalis.Result;
using MediatR;
using Shop.Query.QueriesModel;

namespace Shop.Query.Application.CustomerType.Queries;
public class GetAllCustomerTypeQuery : IRequest<Result<IEnumerable<CustomerTypeQueryModel>>>
{
}
