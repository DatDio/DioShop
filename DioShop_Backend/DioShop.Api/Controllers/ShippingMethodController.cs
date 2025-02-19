using DioShop.Application.Contants;
using DioShop.Application.DTOs.ShippingMethod;
using DioShop.Application.Features.ShippingMethods.Requests.Commands;
using DioShop.Application.Features.ShippingMethods.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DioShop.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShippingMethodController : ControllerBase
	{
		private readonly IMediator _mediator;
		public ShippingMethodController(IMediator mediator)
		{
			_mediator = mediator;
		}



		[HttpGet]
		public async Task<ActionResult<List<ShippingMethodDto>>> Get()
		{
			var shippingMethods = await _mediator.Send(new GetShippingMethodListRequest());
			return Ok(shippingMethods);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<ShippingMethodDto>> Get(int id)
		{
			var shippingMethod = await _mediator.Send(new GetShippingMethodRequest { Id = id });

			return Ok(shippingMethod);
		}

		[HttpPost]
		[Authorize(Roles = Role.RoleAdmin)]
		public async Task<ActionResult> Post([FromBody] CreateShippingMethodDto createShippingMethodDto)
		{
			var command = new CreateShippingMethodCommand { ShippingMethodDto = createShippingMethodDto };
			var response = await _mediator.Send(command);

			return Ok(response);
		}

		[HttpPut]
		[Authorize(Roles = Role.RoleAdmin)]
		public async Task<ActionResult> Put([FromBody] UpdateShippingMethodDto updateShippingMethodDto)
		{
			var command = new UpdateShippingMethodCommand { ShippingMethodDto = updateShippingMethodDto };
			await _mediator.Send(command);

			return NoContent();
		}

		[HttpDelete]
		[Route("{id}")]
		[Authorize(Roles = Role.RoleAdmin)]
		public async Task<ActionResult> Delete(int id)
		{
			var command = new DeleteShippingMethodCommand { Id = id };
			await _mediator.Send(command);

			return NoContent();
		}
	}
}
