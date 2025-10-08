using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApiGitHub.Features.Products.Commands;
using WebApiGitHub.Features.Products.Queries;

namespace WebApiGitHub.Features.Products.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{
	private readonly IMediator _mediator = mediator;

	[HttpPost]
	public async Task<IActionResult> Create(CreateProductCommand command)
	{
		var id = await _mediator.Send(command);
		return CreatedAtAction(nameof(GetAll), new { id }, command);
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var products = await _mediator.Send(new GetAllProductsQuery());
		return Ok(products);
	}		
}
