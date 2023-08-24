using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using MediatR;
using Shop.Application.CustomerType.Responses;

namespace Shop.Application.CustomerType.Commands;
public class CreateCustomerTypeCommand : IRequest<Result<CreatedCustomerTypeResponse>>
{
    [Required]
    [MaxLength(10)]
    [DataType(DataType.Text)]
    public string CustomerTypeCode { get; set; }

    [MaxLength(255)]
    [DataType(DataType.Text)]
    public string Description { get; set; }

    [DataType(DataType.Text)]
    public int TenantId { get; set; }
}
