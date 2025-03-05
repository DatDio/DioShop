using DioShop.Application.Contants;
using DioShop.Application.DTOs.Product;
using DioShop.Application.DTOs.ProductItem;
using DioShop.Application.DTOs.ProductTag;
using DioShop.Application.Features.ProductItems.Requests.Commands;
using DioShop.Application.Features.ProductItems.Requests.Queries;
using DioShop.Application.Features.Products.Requests.Commands;
using DioShop.Application.Features.Products.Requests.Queries;
using DioShop.Application.Features.ProductTags.Requests.Commands;
using DioShop.Application.Ultils;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DioShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult>>> Get([FromQuery] GetProductListRequest request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product = await _mediator.Send(new GetProductRequest { Id = id });

            return Ok(product);
        }

		[HttpPost]
		[Authorize(Roles = Role.RoleAdmin)]
		[Route("createProduct")]
		[Consumes("multipart/form-data")] // Chỉ rõ rằng API sẽ nhận multipart/form-data
		public async Task<ActionResult> Post([FromForm] CreateProductDto product)
		{
			var command = new CreateProductCommand { ProductDto = product };
			var response = await _mediator.Send(command);

			return Ok(response);
		}


		[HttpPatch]
	
		[Authorize(Roles = Role.RoleAdmin)]
        public async Task<ActionResult> Patch([FromBody] UpdateProductDto product)
        {
            var command = new UpdateProductCommand { ProductDto = product };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = Role.RoleAdmin)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteProductCommand { Id = id };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPost]
        [Route("producttag")]
        [Authorize(Roles = Role.RoleAdmin)]
        public async Task<ActionResult> Post([FromBody] CreateProductTagDto productTag)
        {
            var command = new CreateProductTagCommand { ProductTagDto = productTag };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete]
        [Route("producttag")]
        [Authorize(Roles = Role.RoleAdmin)]
        public async Task<ActionResult> DeleteProductTag(DeleteProductTagDto deleteProductTagDto)
        {
            var command = new DeleteProductTagCommand { ProductTagDto = deleteProductTagDto };
            var response = await _mediator.Send(command);

            return Ok(response);
        }


        //Product Item
        [HttpGet]
        [Route("productitem")]
        [Authorize(Roles = Role.RoleAdmin)]
        public async Task<ActionResult<List<ProductItemDto>>> GetProductTags()
        {
            var tags = await _mediator.Send(new GetProductItemListRequest());
            return Ok(tags);
        }

        [HttpGet]
        [Route("productitem/{id}")]
        [Authorize(Roles = Role.RoleAdmin)]
        public async Task<ActionResult<ProductItemDto>> GetProductTagById(int id)
        {
            var tag = await _mediator.Send(new GetProductItemRequest { Id = id });

            return Ok(tag);
        }

        [HttpPost]
        [Route("productitem")]
        [Authorize(Roles = Role.RoleAdmin)]
        public async Task<ActionResult> Post([FromBody] CreateProductItemDto productItem)
        {
            var command = new CreateProductItemCommand { ProductItemDto = productItem };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        [Route("productitem")]
        [Authorize(Roles = Role.RoleAdmin)]
        public async Task<ActionResult> Put([FromBody] UpdateProductItemDto productItem)
        {
            var command = new UpdateProductItemCommand { ProductItemDto = productItem };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete]
        [Route("productitem/{id}")]
        [Authorize(Roles = Role.RoleAdmin)]
        public async Task<ActionResult> DeleteProductItem(int id)
        {
            var command = new DeleteProductItemCommand { Id = id };
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
