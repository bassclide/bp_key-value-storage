using Microsoft.AspNetCore.Mvc;

namespace KeyValueStorage.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class KeysController : ControllerBase
{
	[HttpGet]
	public IActionResult GetList()
	{
		return Ok();
	}
}