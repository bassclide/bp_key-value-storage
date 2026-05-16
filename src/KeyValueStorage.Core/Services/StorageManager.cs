namespace KeyValueStorage.Core.Services;

public sealed class StorageManager
{
	private readonly IRepository _repository;

	public StorageManager(IRepository repository)
	{
		_repository = repository;
	}

	public async Task<string> StoreData(string key, Stream dataStream, CancellationToken cancellationToken)
	{
		try
		{
			// var 
			return await _repository.StoreData(key, dataStream, cancellationToken);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
}