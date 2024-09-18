using Microsoft.AspNetCore.Mvc;
using FakeUserGenerator.Models;
using FakeUserGenerator.Services;
using System.Globalization;
using CsvHelper;
using System.Text;

namespace FakeUserGenerator.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DataController : ControllerBase
	{
		private readonly IDataGenerationService _dataService;

		public DataController(IDataGenerationService dataService)
		{
			_dataService = dataService;
		}

		[HttpPost("generate")]
		public IActionResult Generate([FromBody] DataGenerationRequest request) => Ok(_dataService.GenerateUsers(request));

		[HttpPost("export")]
		public IActionResult ExportToCsv([FromBody] DataGenerationRequest request)
		{
			using (var memoryStream = new MemoryStream())
			using (var writer = new StreamWriter(memoryStream, Encoding.UTF8))
			using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
			{
				var allUsers = new List<UserData>();
				Enumerable.Range(0, request.PageNumber).ToList().ForEach(i =>
					allUsers.AddRange(_dataService.GenerateUsers(CreateRequest(request.Region!, request.ErrorCount, request.Seed, i))));
				csv.WriteRecords(allUsers);
				writer.Flush();
				var fileName = $"{request.Region}_error_{request.ErrorCount}_seed_{request.Seed}_users.csv";
				return File(memoryStream.ToArray(), "text/csv", fileName);
			}
		}

		private DataGenerationRequest CreateRequest(string region, double errorCount, int seed, int pageNumber)
			=> new DataGenerationRequest
			{
				Region = region,
				ErrorCount = errorCount,
				Seed = seed,
				PageNumber = pageNumber
			};
	}
}
