using FakeUserGenerator.Models;
using System.Reflection;

namespace FakeUserGenerator.Services
{
	public class ErrorService : IErrorService
	{
		private const string Digits = "0123456789";
		private const int StartProperty = 2; // 0 - Number, 1 - Id
		private static readonly PropertyInfo[] RecordProperties = typeof(UserData).GetProperties();
		private Random _random = new Random();
		private readonly Dictionary<Func<string, bool>, Func<string, string, string, string>> _modifiers;
		private readonly Dictionary<int, Func<string, int, string, string>> _errorActions;

		public ErrorService()
		{
			_modifiers = new()
			{
				{ IsPhoneNumber, (value, _, digits) => ModifyPhoneNumber(value, digits) },
				{ IsAddress, (value, alphabet, digits) => ModifyAddress(value, alphabet + digits) }
			};
			_errorActions = new()
			{
				{ 0, (input, position, _) => RemoveSymbol(input, position) },
				{ 1, (input, position, alphabet) => AddSymbol(input, position, alphabet!) },
				{ 2, (input, position, _) => SwapNearSymbols(input, position) }
			};
		}

		public UserData IntroduceErrors(UserData user, double errorCount, string alphabet)
		{
			for (int i = 0; i < (int)errorCount; i++)
				user = ApplyError(user, alphabet);
			if (IsProbabilisticError(errorCount))
				user = ApplyError(user, alphabet);
			return user;
		}

		public int Seed
		{
			set => _random = new Random(value);
		}

		private bool IsProbabilisticError(double errorCount) => GetFractional(errorCount) > _random.NextDouble();

		private double GetFractional(double number) => number % 1;

		private UserData ApplyError(UserData user, string alphabet)
		{
			var property = RecordProperties[_random.Next(StartProperty, RecordProperties.Length)];
			var fieldValue = property.GetValue(user)?.ToString() ?? string.Empty;
			foreach (var modifier in _modifiers)
				if (modifier.Key(property.Name))
				{
					property.SetValue(user, modifier.Value(fieldValue, alphabet, Digits));
					return user;
				}
			property.SetValue(user, ModifyString(fieldValue, alphabet));
			return user;
		}

		private bool IsPhoneNumber(string fieldName) => fieldName.ToLower().Contains("phone");

		private bool IsAddress(string fieldName) => fieldName.ToLower().Contains("address");

		private string ModifyPhoneNumber(string phoneNumber, string digits) => ApplyErrorAction(phoneNumber, digits);

		private string ModifyAddress(string address, string alphabet) => ApplyErrorAction(address, alphabet);

		private string ModifyString(string fieldValue, string alphabet) => ApplyErrorAction(fieldValue, alphabet);

		private string ApplyErrorAction(string input, string alphabet)
		{
			int action = _random.Next(_errorActions.Count);
			int position = _random.Next(input.Length);
			return _errorActions[action](input, position, alphabet);
		}

		private string RemoveSymbol(string input, int position) =>
			position < input.Length ? input.Remove(position, 1) : input;

		private string AddSymbol(string input, int position, string alphabet) =>
			input.Insert(Math.Min(position, input.Length), alphabet[_random.Next(alphabet.Length)].ToString());

		private string SwapNearSymbols(string input, int position)
		{
			if (position >= input.Length - 1)
				return input;
			var chars = input.ToCharArray();
			(chars[position], chars[position + 1]) = (chars[position + 1], chars[position]);
			return new string(chars);
		}
	}
}
