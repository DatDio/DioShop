using DioShop.Application.Contants;
using DioShop.Application.DTOs.Tag;
using DioShop.Application.Features.Tags.Requests.Commands;
using DioShop.Application.Features.Tags.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DioShop.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TagController : ControllerBase
	{
		private readonly IMediator _mediator;
		public TagController(IMediator mediator)
		{
			_mediator = mediator;
		}



		[HttpGet]
		public async Task<ActionResult<List<TagDto>>> Get()
		{
			var tags = await _mediator.Send(new GetTagListRequest());
			return Ok(tags);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<TagDto>> Get(int id)
		{
			var tag = await _mediator.Send(new GetTagRequest { Id = id });

			return Ok(tag);
		}

		[HttpPost]
		[Authorize(Roles = Role.RoleAdmin)]
		public async Task<ActionResult> Post([FromBody] CreateTagDto tag)
		{
			var command = new CreateTagCommand { TagDto = tag };
			var response = await _mediator.Send(command);

			return Ok(response);
		}

		[HttpPut]
		[Authorize(Roles = Role.RoleAdmin)]
		public async Task<ActionResult> Put([FromBody] UpdateTagDto tag)
		{
			var command = new UpdateTagCommand { TagDto = tag };
			await _mediator.Send(command);

			return NoContent();
		}

		[HttpDelete]
		[Route("{id}")]
		[Authorize(Roles = Role.RoleAdmin)]
		public async Task<ActionResult> Delete(int id)
		{
			var command = new DeleteTagCommand { Id = id };
			await _mediator.Send(command);

			return NoContent();
		}
	}
}
