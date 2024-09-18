namespace FakeUserGenerator.Services
{
	public class AlphabetService : IAlphabetService
	{
		private const string DefaultAlphabet = "abcdefghijklmnopqrstuvwxyz";
		public string DefaultRegion => "en_US";

		private static readonly Dictionary<string, string> _alphabets = new()
		{
            { "en_US", "abcdefghijklmnopqrstuvwxyz" },
			{ "en_GB", "abcdefghijklmnopqrstuvwxyz" },
			{ "en_AU", "abcdefghijklmnopqrstuvwxyz" },
			{ "en_CA", "abcdefghijklmnopqrstuvwxyz" },
			{ "en_IE", "abcdefghijklmnopqrstuvwxyz" },
			{ "en_IND", "abcdefghijklmnopqrstuvwxyz" },
			{ "en_NG", "abcdefghijklmnopqrstuvwxyz" },
			{ "en_ZA", "abcdefghijklmnopqrstuvwxyz" },
			{ "en_BORK", "abcdefghijklmnopqrstuvwxyz" },
			{ "en_AU_ocker", "abcdefghijklmnopqrstuvwxyz" },
        
            { "fr", "abcdefghijklmnopqrstuvwxyzàâçéèêëîïôùûüÿ" },
			{ "fr_FR", "abcdefghijklmnopqrstuvwxyzàâçéèêëîïôùûüÿ" },
			{ "fr_CA", "abcdefghijklmnopqrstuvwxyzàâçéèêëîïôùûüÿ" },
			{ "fr_CH", "abcdefghijklmnopqrstuvwxyzàâçéèêëîïôùûüÿ" },

            { "es", "abcdefghijklmnñopqrstuvwxyz" },
			{ "es_MX", "abcdefghijklmnñopqrstuvwxyz" },

            { "pt_BR", "abcdefghijklmnopqrstuvwxyzáâãàçéêíóôõúü" },
			{ "pt_PT", "abcdefghijklmnopqrstuvwxyzáâãàçéêíóôõúü" },

            { "pl", "aąbcćdeęfghijklłmnńoópqrsśtuvwxyzźż" },

            { "ru", "абвгдеёжзийклмнопрстуфхцчшщыэюя" },

            { "zh_CN", "abcdefghijklmnopqrstuvwxyz" },
			{ "zh_TW", "abcdefghijklmnopqrstuvwxyz" },

            { "ar", "ابتثجحخدذرزسشصضطظعغفقكلمنهوي" },

            { "fi", "abcdefghijklmnopqrstuvwxyzåäö" },

            { "sv", "abcdefghijklmnopqrstuvwxyzåäö" },

            { "tr", "abcçdefgğhıijklmnoöprsştuüvyz" },

            { "de", "abcdefghijklmnopqrstuvwxyzäöüß" },
			{ "de_AT", "abcdefghijklmnopqrstuvwxyzäöüß" },
			{ "de_CH", "abcdefghijklmnopqrstuvwxyzäöüß" },

            { "cz", "aábcčdďeéěfghhiíjklmnňoóprsštťuúůvwxyýzž" },

            { "el", "αβγδεζηθικλμνξοπρστυφχψω" },

            { "id_ID", "abcdefghijklmnopqrstuvwxyz" },

            { "it", "abcdefghijklmnopqrstuvwxyz" },

            { "lv", "aābcčdeēfgģhiījkķlļmnņoprsštuūvzž" },

            { "nb_NO", "abcdefghijklmnopqrstuvwxyzæøå" },

            { "ne", "abcdefghijklmnopqrstuvwxyz" },

            { "nl", "abcdefghijklmnopqrstuvwxyz" },
			{ "nl_BE", "abcdefghijklmnopqrstuvwxyz" },

            { "ro", "abcdefghijklmnopqrstuvwxyzăâîșț" },

            { "sk", "aáäbcčdďeéfghiíjklĺľmnňoóôprsšťuúvwxyýzž" },

            { "uk", "абвгґдеєжзиіїйклмнопрстуфхцчшщьюя" },

            { "vi", "abcdefghijklmnopqrstuvwxyzâăđêôơư" },

            { "ja", "abcdefghijklmnopqrstuvwxyz" },
			{ "ko", "abcdefghijklmnopqrstuvwxyz" },


            { "fa", "ابپتثجچحخدذرزسشصضطظعغفقکگلمنوهی" },

            { "zu_ZA", "abcdefghijklmnopqrstuvwxyz" },

            { "ge", "აბგდევზთიკლმნოპჟრსტუფქღყშჩცძწჭხჯჰ" },

            { "hr", "abcdefghijklmnopqrstuvwxyzčćđšž" }
		};

		public string GetAlphabetByRegionOrDefault(string region) => _alphabets.GetValueOrDefault(region, DefaultAlphabet);
	}
}
