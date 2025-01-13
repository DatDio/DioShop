using DioShop.Application.Contants;
using DioShop.Application.DTOs.Brand;
using DioShop.Application.Features.Brand.Requests.Commands;
using DioShop.Application.Features.Brand.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DioShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<List<BrandDto>>> Get()
        {
            var brands = await _mediator.Send(new GetBrandListRequest());
            return Ok(brands);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<BrandDto>> Get(int id)
        {
            var brand = await _mediator.Send(new GetBrandRequest { Id = id });

            return Ok(brand);
        }

        [HttpPost]
        [Authorize(Roles = Role.RoleAdmin)]
        public async Task<ActionResult> Post([FromBody] CreateBrandDto productItem)
        {
            var command = new CreateBrandCommand { BrandDto = productItem };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = Role.RoleAdmin)]
        public async Task<ActionResult> Put([FromBody] UpdateBrandDto brandDto)
        {
            var command = new UpdateBrandCommand { BrandDto = brandDto };
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = Role.RoleAdmin)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteBrandCommand { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
