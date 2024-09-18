namespace FakeUserGenerator.Models
{
	public class DataGenerationRequest
	{
		public string? Region { get; set; }
		public double ErrorCount { get; set; }
		public int Seed { get; set; }
		public int PageNumber { get; set; }
	}

}
