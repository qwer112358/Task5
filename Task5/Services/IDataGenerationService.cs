using FakeUserGenerator.Models;

namespace FakeUserGenerator.Services
{
	public interface IDataGenerationService
	{
		List<UserData> GenerateUsers(DataGenerationRequest request);
		static int TotalRecordsGenereted { get; }
	}
}
