using FakeUserGenerator.Models;
using Bogus;

namespace FakeUserGenerator.Services
{
	public class DataGenerationService : IDataGenerationService
	{
		private const int FirstRecordsGenerationCount = 20;
		private const int SecondRecordsGenerationCount = 10;
		private readonly IErrorService _errorService;
		private readonly IAlphabetService _alphabetService;
		private Random _random = new Random();
		private int _recordsPerPageCount = FirstRecordsGenerationCount;

		public DataGenerationService(IErrorService errorService, IAlphabetService alphabetService)
		{
			_errorService = errorService;
			_alphabetService = alphabetService;
		}

		public List<UserData> GenerateUsers(DataGenerationRequest request)
		{
			InitialSeed(request.Seed + request.PageNumber);
			var region = request.Region ?? _alphabetService.DefaultRegion;
			var alphabet = _alphabetService.GetAlphabetByRegionOrDefault(region);
			return Enumerable.Range(GetNextUserNumber(request.PageNumber), _recordsPerPageCount)
				.Select(i => _errorService.IntroduceErrors(
					GenerateUser(i, region, alphabet),
					request.ErrorCount,
					alphabet))
				.ToList();
		}

		private void InitialSeed(int seed)
		{
			_random = new Random(seed);
			Randomizer.Seed = new Random(seed);
			_errorService.Seed = seed;
		}

		private int GetNextUserNumber(int pageNumber)
		{
			if (pageNumber == 0) 
				return 1;
			_recordsPerPageCount = SecondRecordsGenerationCount;
			return FirstRecordsGenerationCount + 1 + (pageNumber - 1) * SecondRecordsGenerationCount;
		}

		private UserData GenerateUser(int number, string region, string alphabet)
		{
			var faker = new Faker(region);
			return new UserData
			{
				Number = number,
				Identifier = GenerateSeededGuid(),
				FullName = faker.Name.FullName(),
				Address = faker.Address.FullAddress(),
				Phone = faker.Phone.PhoneNumber()
			};
		}

		public Guid GenerateSeededGuid()
		{
			const int guidLength = 16;
			var guid = new byte[guidLength];
			_random.NextBytes(guid);
			return new Guid(guid);
		}
	}
}
