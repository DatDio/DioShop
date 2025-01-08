using DioShop.Application.Contants;
using DioShop.Application.DTOs.Brand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DioShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //[HttpGet]
        //public async Task<ActionResult<List<BrandDto>>> Get()
        //{
        //    var coupons = await _mediator.Send(new GetBrandListRequest());
        //    return Ok(coupons);
        //}

        //[HttpGet]
        //[Route("{id}")]
        //public async Task<ActionResult<BrandDto>> Get(int id)
        //{
        //    var coupon = await _mediator.Send(new GetBrandRequest { Id = id });

        //    return Ok(coupon);
        //}

        //[HttpPost]
        //[Authorize(Roles = Role.RoleAdmin)]
        //public async Task<ActionResult> Post([FromBody] CreateBrandDto productItem)
        //{
        //    var command = new CreateBrandCommand { BrandDto = productItem };
        //    var response = await _mediator.Send(command);

        //    return Ok(response);
        //}

        //[HttpPut]
        //[Authorize(Roles = Role.RoleAdmin)]
        //public async Task<ActionResult> Put([FromBody] UpdateBrandDto productItem)
        //{
        //    var command = new UpdateBrandCommand { BrandDto = productItem };
        //    await _mediator.Send(command);

        //    return NoContent();
        //}

        //[HttpDelete]
        //[Route("{id}")]
        //[Authorize(Roles = Role.RoleAdmin)]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    var command = new DeleteBrandCommand { Id = id };
        //    await _mediator.Send(command);

        //    return NoContent();
        //}
    }
}
