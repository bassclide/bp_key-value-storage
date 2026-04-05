using System.Security.Cryptography;

namespace KeyValueStorage.Core.Services;

public interface IRepository
{
	Task<Stream> ProvideData(string key);
	Task<string> StoreData(string key, Stream dataStream);
}

public sealed class Repository : IRepository
{
	private readonly int _bufferSize = 4096;
	private readonly string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "storage");

	public async Task<Stream> ProvideData(string key)
	{
		// var filePath = Path.Combine(Directory.GetCurrentDirectory(), "storage", key);

		if (!File.Exists(_storagePath))
		{
			throw new FileNotFoundException();
		}

		return new FileStream(_storagePath, FileMode.Open, FileAccess.Read, FileShare.Read, _bufferSize, useAsync: true);
	}


	public async Task<string> StoreData(string key, Stream dataStream)
	{
		try
		{

		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
		var filePath = Path.Combine(_storagePath, key);
		await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None,
			_bufferSize, useAsync: true);

		using var sha256 = SHA256.Create();
		await using var cryptoStream = new CryptoStream(fileStream, sha256, CryptoStreamMode.Write);
		
		await dataStream.CopyToAsync(cryptoStream);
		await cryptoStream.FlushFinalBlockAsync();
		
		return Convert.ToHexString(sha256.Hash!);
	}
}