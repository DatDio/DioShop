using DioShop.Application.Contants;
using DioShop.Application.DTOs.Coupon;
using DioShop.Application.Features.Coupons.Requests.Commands;
using DioShop.Application.Features.Coupons.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DioShop.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CouponController : ControllerBase
	{
		IMediator _mediator;
		public CouponController(IMediator mediator)
		{
			_mediator = mediator;
		}



		[HttpGet]
		public async Task<ActionResult<List<CouponDto>>> Get()
		{
			var coupons = await _mediator.Send(new GetCouponListRequest());
			return Ok(coupons);
		}


		[HttpGet]
		[Route("GetCouponByCode/{coupon}")]
		public async Task<ActionResult<List<CouponDto>>> GetCouponByCode(string coupon)
		{
			var coupons = await _mediator.Send(new GetCouponByCouponCodeRequest() { CouponCode = coupon });
			return Ok(coupons);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<CouponDto>> Get(int id)
		{
			var coupon = await _mediator.Send(new GetCouponRequest { Id = id });

			return Ok(coupon);
		}

		[HttpPost]
		[Authorize(Roles = Role.RoleAdmin)]
		public async Task<ActionResult> Post([FromBody] CreateCouponDto coupon)
		{
			var command = new CreateCouponCommand { CouponDto = coupon };
			var response = await _mediator.Send(command);

			return Ok(response);
		}

		[HttpPatch]
		[Authorize(Roles = Role.RoleAdmin)]
		public async Task<ActionResult> Patch([FromBody] UpdateCouponDto coupon)
		{
			var command = new UpdateCouponCommand { CouponDto = coupon };
			await _mediator.Send(command);

			return NoContent();
		}

		[HttpDelete]
		[Route("{id}")]
		[Authorize(Roles = Role.RoleAdmin)]
		public async Task<ActionResult> Delete(int id)
		{
			var command = new DeleteCouponCommand { Id = id };
			await _mediator.Send(command);

			return NoContent();
		}
	}
}
