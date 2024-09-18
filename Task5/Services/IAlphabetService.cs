namespace FakeUserGenerator.Services
{
	public interface IAlphabetService
	{
		string DefaultRegion { get; }
		string GetAlphabetByRegionOrDefault(string region);
	}
}
