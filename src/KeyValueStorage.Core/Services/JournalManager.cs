using System.Text.Json;

namespace KeyValueStorage.Core.Services;

public interface IJournalManager
{
	Task SaveJournalAsync(List<JournalEntry> entries);
	Task<List<JournalEntry>> LoadJournalAsync();
}

public sealed class JournalManager : IJournalManager
{
	private readonly string _journalFilePath = "journal.json";
	private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

	public async Task SaveJournalAsync(List<JournalEntry> entries)
	{
		await using var createStream = File.Create(_journalFilePath);
		await JsonSerializer.SerializeAsync(createStream, entries, _options);
	}

	public async Task<List<JournalEntry>> LoadJournalAsync()
	{
		if (!File.Exists(_journalFilePath))
			return new List<JournalEntry>();

		await using FileStream openStream = File.OpenRead(_journalFilePath);
		return await JsonSerializer.DeserializeAsync<List<JournalEntry>>(openStream)
		       ?? new List<JournalEntry>();
	}
}

public sealed record JournalEntry(
	string Path,
	DateTime Date,
	string Hash
);