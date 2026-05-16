using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace KeyValueStorage.Api.IntegrationTests.Controllers;

public sealed class KeysControllerTests
{
	private WebApplicationFactory<Program> _factory = null!;
	private HttpClient _client = null!;

	[SetUp]
	public void SetUp()
	{
		_factory = new WebApplicationFactory<Program>();
		_client = _factory.CreateClient(new WebApplicationFactoryClientOptions
		{
			AllowAutoRedirect = false
		});
	}

	[TearDown]
	public void TearDown()
	{
		_client.Dispose();
		_factory.Dispose();
	}

	[Test]
	public async Task GetList_ReturnsOk()
	{
		using var response = await _client.GetAsync("/Keys");

		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
	}

	[Test]
	public async Task GetList_ReturnsEmptyBody()
	{
		using var response = await _client.GetAsync("/Keys");

		var content = await response.Content.ReadAsStringAsync();

		Assert.That(content, Is.Empty);
	}

	[Test]
	public async Task Post_ReturnsMethodNotAllowed()
	{
		using var response = await _client.PostAsync("/Keys", content: null);

		Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.MethodNotAllowed));
	}
}
