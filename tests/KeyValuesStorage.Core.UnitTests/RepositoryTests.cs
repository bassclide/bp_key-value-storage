using KeyValueStorage.Core.Services;

namespace KeyValuesStorage.Core.UnitTests;

public class Tests
{
	[SetUp]
	public void Setup()
	{
	}

	[Test]
	public void ProvideData()
	{
		var repository = new Repository();
		Assert.Pass();
	}
}