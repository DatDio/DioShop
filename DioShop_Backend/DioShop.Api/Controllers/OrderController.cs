using DioShop.Application.Contants;
using DioShop.Application.DTOs.Order;
using DioShop.Application.Features.Orders.Requests.Commands;
using DioShop.Application.Features.Orders.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DioShop.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class OrderController : ControllerBase
	{
		private readonly IMediator _mediator;
		public OrderController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult> Get()
		{
			var response = await _mediator.Send(new ClientGetOrderListRequest());
			return Ok(response);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult> Get(int id)
		{
			var response = await _mediator.Send(new ClientGetOrderRequest { Id = id });
			return Ok(response);
		}


		[HttpGet]
		[Route("admin")]
		[Authorize(Roles = Role.RoleAdmin)]
		public async Task<ActionResult> AdminGet()
		{
			var response = await _mediator.Send(new AdminGetOrderListRequest());
			return Ok(response);
		}

		[HttpGet]
		[Route("admin/{id}")]
		[Authorize(Roles = Role.RoleAdmin)]
		public async Task<ActionResult> AdminGet(int id)
		{
			var response = await _mediator.Send(new AdminGetOrderRequest { Id = id });
			return Ok(response);
		}




		[HttpPost]
		public async Task<ActionResult> Post([FromBody] CreateOrderDto createOrderDto)
		{
			var command = new CreateOrderCommand
			{
				OrderDto = createOrderDto
			};
			var orderId = await _mediator.Send(command);

			//createOrderWithStripeSetupDto.StripeSetupDto = new StripeSetupDto
			//{
			//	ApprovedUrl = createOrderWithStripeSetupDto.StripeSetupDto.ApprovedUrl + orderId.ToString(),
			//	CancelUrl = createOrderWithStripeSetupDto.StripeSetupDto.CancelUrl
			//};

			//var url = await _mediator
			//	.Send(new CreateCheckoutSessionCommand { StripeSetupDto = createOrderWithStripeSetupDto.StripeSetupDto, OrderId = orderId });

			//return Ok(new { url = url });
			return Ok(orderId);
		}

		[HttpPatch]
		public async Task<ActionResult> Patch([FromBody] UpdateOrderStatusDto updateOrderStatusCommand)
		{
			var command = new UpdateOrderStatusCommand { OrderDto = updateOrderStatusCommand };
			var response = await _mediator.Send(command);

			return Ok(response);
		}

		//[HttpPost]
		//[Route("confirmpayment/{id}")]
		//public async Task<ActionResult> ConfirmPayment(int id)
		//{

		//	var command = new ValidateStripeSessionCommand
		//	{
		//		OrderId = id,
		//	};

		//	var response = await _mediator.Send(command);

		//	return Ok(response);
		//}


		[HttpPost]
		[Route("completeOrder/{id}")]
		[Authorize(Roles = Role.RoleAdmin)]
		public async Task<ActionResult> CompleteOrder(int id)
		{

			var command = new CompleteOrderCommand
			{
				Id = id,
			};

			var response = await _mediator.Send(command);

			return Ok(response);
		}

	}
}
