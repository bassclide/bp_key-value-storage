using KeyValueStorage.Core;
using Microsoft.AspNetCore.Mvc;

namespace KeyValueStorage.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class StorageController : ControllerBase
{
	private readonly Repository _repository;

	public StorageController(Repository repository)
	{
		_repository = repository;
	}

	[HttpGet("{key}")]
	public async Task<IActionResult> Get([FromRoute] string key)
	{
		return File(await _repository.ProvideData(key), "application/octet-stream");
	}

	[HttpPost("{key}")]
	[DisableRequestSizeLimit]
	public async Task<IActionResult> Post(string key)
	{
		var hash = await _repository.StoreData(key, Request.Body);
		return Ok(new { Key = key, Hash = hash });
	}
}