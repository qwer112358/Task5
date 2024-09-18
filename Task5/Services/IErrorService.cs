using FakeUserGenerator.Models;

namespace FakeUserGenerator.Services
{
	public interface IErrorService
	{
		UserData IntroduceErrors(UserData user, double errorCount, string alphabet);
		int Seed { set; }
	}
}
