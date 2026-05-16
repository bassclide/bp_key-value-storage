using System.Security.Cryptography;

namespace KeyValueStorage.Core.Services;

public interface IRepository
{
	public Stream ProvideData(string key);
	Task<string> StoreData(string key, Stream dataStream, CancellationToken cancellationToken);
}

public sealed class Repository : IRepository
{
	private readonly int _bufferSize = 4096;
	private readonly string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "storage");

	public Stream ProvideData(string key)
	{
		var filePath = Path.Combine(_storagePath, key);

		if (!File.Exists(filePath))
		{
			throw new FileNotFoundException();
		}

		return new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, _bufferSize,
			useAsync: true);
	}


	public async Task<string> StoreData(string key, Stream dataStream, CancellationToken cancellationToken)
	{
		var filePath = Path.Combine(_storagePath, key);
		await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None,
			_bufferSize, useAsync: true);

		using var sha256 = SHA256.Create();
		await using var cryptoStream = new CryptoStream(fileStream, sha256, CryptoStreamMode.Write);

		await dataStream.CopyToAsync(cryptoStream, cancellationToken);
		await cryptoStream.FlushFinalBlockAsync(cancellationToken);

		return Convert.ToHexString(sha256.Hash!);
	}
}