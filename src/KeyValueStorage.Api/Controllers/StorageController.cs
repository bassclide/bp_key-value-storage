using KeyValueStorage.Core.Services;
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
	public IActionResult Get([FromRoute] string key)
	{
		return File(_repository.ProvideData(key), "application/octet-stream");
	}

	[HttpPost("{key}")]
	[DisableRequestSizeLimit]
	public async Task<IActionResult> Post(string key, CancellationToken cancellationToken)
	{
		var hash = await _repository.StoreData(key, Request.Body, cancellationToken);
		return Ok(new { Key = key, Hash = hash });
	}
}