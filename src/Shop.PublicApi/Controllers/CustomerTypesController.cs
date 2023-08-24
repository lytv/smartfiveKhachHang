using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.CustomerType.Commands;
using Shop.PublicApi.Extensions;
using Shop.PublicApi.Models.Responses;
using Shop.Query.Application.CustomerType.Queries;
using Shop.Query.QueriesModel;

namespace Shop.PublicApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
public class CustomerTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Register a new Customer Type
    /// </summary>
    /// <param name="command"></param>
    /// <response code="200">Returns the Id of the new customer type.</response>
    /// <response code="400">Returns list of errors if the request is invalid.</response>
    /// <response code="500">When an unexpected internal error occurs on the server.</response>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<CreateCustomerTypeCommand>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody][Required] CreateCustomerTypeCommand command)
        => (await _mediator.Send(command)).ToActionResult();

    /// <summary>
    /// Query all customer type
    /// </summary>
    /// <response code="200">Returns the list of customer type.</response>
    /// <response code="500">When an unexpected internal error occurs on the server.</response>
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CustomerTypeQueryModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll() =>
        (await _mediator.Send(new GetAllCustomerTypeQuery())).ToActionResult();

    /// <summary>
    /// Query customer type by id
    /// </summary>
    /// <response code="200">Returns a customer type.</response>
    /// <response code="400">Returns list of errors if the request is invalid.</response>
    /// <response code="404">When no client is found by the given Id.</response>
    /// <response code="500">When an unexpected internal error occurs on the server.</response>
    [HttpGet("{id:guid}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<GetCustomerTypeByIdQuery>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([Required] Guid id) =>
        (await _mediator.Send(new GetCustomerTypeByIdQuery(id))).ToActionResult();

        /// <summary>
    /// Updates an existing client type.
    /// </summary>
    /// <param name="command"></param>
    /// <response code="200">Returns the response with the success message.</response>
    /// <response code="400">Returns list of errors if the request is invalid.</response>
    /// <response code="404">When no client type is found by the given Id.</response>
    /// <response code="500">When an unexpected internal error occurs on the server.</response>
    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody][Required] UpdateCustomerTypeCommand command) =>
        (await _mediator.Send(command)).ToActionResult();

    /// <summary>
    /// Deletes the client type by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200">Returns the response with the success message.</response>
    /// <response code="400">Returns list of errors if the request is invalid.</response>
    /// <response code="404">When no client type is found by the given Id.</response>
    /// <response code="500">When an unexpected internal error occurs on the server.</response>
    [HttpDelete("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([Required] Guid id) =>
        (await _mediator.Send(new DeleteCustomerTypeCommand(id))).ToActionResult();
}
