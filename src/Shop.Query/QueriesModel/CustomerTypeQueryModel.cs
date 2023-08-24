using System;
using Shop.Query.Abstractions;

namespace Shop.Query.QueriesModel;
public class CustomerTypeQueryModel : IQueryModel<Guid>
{
    public CustomerTypeQueryModel(Guid id, string customerTypeCode, string description, int tenantId)
    {
        Id = id;
        CustomerTypeCode = customerTypeCode;
        Description = description;
        TenantId = tenantId;
    }

    public Guid Id { get; }
    public string CustomerTypeCode { get; }
    public string Description { get; }
    public int TenantId { get; }
}
