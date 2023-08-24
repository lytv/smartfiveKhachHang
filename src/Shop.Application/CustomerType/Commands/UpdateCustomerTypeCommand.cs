using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using MediatR;

namespace Shop.Application.CustomerType.Commands;
public class UpdateCustomerTypeCommand : IRequest<Result>
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(10)]
    public string CustomerTypeCode { get; set; }

    [Required]
    [MaxLength(255)]
    public string Description { get; set; }
}
