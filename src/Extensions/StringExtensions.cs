using System.Text.Json;
using System.Text.RegularExpressions;

namespace Lambifast.Extensions;

public static class StringExtensions
	{
		public static string NormalizeNewLines(this string value)
		{
			return Regex.Replace(value, @"\r\n|\n\r|\n|\r", Environment.NewLine);
		}

		public static string ToCamelCase(this string value)
		{
			string tmp;
			var dotSplit = value.Split(".");
			if (dotSplit.Length > 1)
			{
				tmp = dotSplit.Aggregate((f, s) => $"{f.InternalToCamelCase()}.{s.InternalToCamelCase()}");
			}
			else
			{
				tmp = value.InternalToCamelCase();
			}
			return tmp;
		}

		private static string InternalToCamelCase(this string value)
		{
			return JsonNamingPolicy.CamelCase.ConvertName(value);
		}

		public static string MaskEmail(this string email, double maskPercentage = 0.7)
		{
			var split = email.Split("@");
			if (split.Length == 2)
			{
				var address = split.First();
				var domain = split.Last();

				int charToMask = (int)Math.Ceiling(address.Length * maskPercentage);
				int charToKeep = address.Length - charToMask;
				int charToKeepStart = (int)Math.Ceiling(charToKeep / 2.0);
				int charToKeepEnd = charToKeep - charToKeepStart;

				address = address.Substring(0, charToKeepStart) + new string('*', charToMask) + address.Substring(charToKeepStart + charToMask, charToKeepEnd);
				email = address + "@" + domain;
			}

			return email;
		}

		private static string[] Prefixes = new[] { "+1", "+93", "+355", "+213", "+1-684", "+376", "+244", "+1-264", "+672", "+1-268", "+54", "+374", "+297", "+247", "+61", "+43", "+994", "+1-242", "+973", "+880", "+1-246", "+375", "+32", "+501", "+229", "+1-441", "+975", "+591", "+387", "+267", "+55", "+1-284", "+673", "+359", "+226", "+257", "+855", "+237", "+238", "+1-345", "+236", "+235", "+64", "+56", "+86", "+61-8", "+57", "+269", "+242", "+682", "+506", "+225", "+385", "+53", "+5399", "+599", "+357", "+420", "+45", "+246", "+253", "+1-767", "+1-809", "+1-829", "+670", "+593", "+20", "+503", "+8812", " +8813", "+88213", "+240", "+291", "+372", "+251", "+500", "+298", "+679", "+358", "+33", "+596", "+594", "+689", "+241", "+220", "+995", "+49", "+233", "+350", "+881", "+8810", "+8811", " +8817", "+8818", "+8819", "+30", "+299", "+1-473", "+590", "+1-671", "+502", "+245", "+224", "+592", "+509", "+504", "+852", "+36", "+354", "+91", "+62", "+871", "+874", "+873", "+872", "+870", "+800", "+808", "+98", "+964", "+353", "+8816", "+8817", "+972", "+39", "+1-876", "+81", "+962", "+7", "+254", "+686", "+850", "+82", "+965", "+996", "+856", "+371", "+961", "+266", "+231", "+218", "+423", "+370", "+352", "+853", "+389", "+261", "+265", "+60", "+960", "+223", "+356", "+692", "+222", "+230", "+52", "+691", "+1-808", "+373", "+377", "+976", "+382", "+1-664", "+212", "+258", "+95", "+264", "+674", "+977", "+31", "+1-869", "+687", "+505", "+227", "+234", "+683", "+47", "+968", "+92", "+680", "+970", "+507", "+675", "+595", "+51", "+63", "+48", "+351", "+1-787", "+1-939", "+974", "+262", "+40", "+250", "+290", "+1-758", "+508", "+1-784", "+685", "+378", "+239", "+966", "+221", "+381", "+248", "+232", "+65", "+421", "+386", "+677", "+252", "+27", "+34", "+94", "+249", "+597", "+268", "+46", "+41", "+963", "+886", "+992", "+255", "+66", "+88216", "+228", "+690", "+676", "+1-868", "+216", "+90", "+993", "+1-649", "+688", "+256", "+380", "+971", "+44", "+1-340", "+878", "+598", "+998", "+678", "+379", "+58", "+84", "+681", "+967", "+260", "+263" };

		public static string MaskPhoneNumber(this string phoneNumber, double maskPercentage = 0.7)
		{
			var prefix = (from p in Prefixes
						  where phoneNumber.StartsWith(p)
						  select p).FirstOrDefault();

			var phoneOnly = phoneNumber;
			if (!string.IsNullOrEmpty(prefix))
				phoneOnly = phoneNumber.Replace(prefix, "");

			int charToMask = (int)Math.Ceiling(phoneOnly.Length * maskPercentage);
			int charToKeep = phoneOnly.Length - charToMask;
			phoneNumber = prefix + new string('*', charToMask) + phoneOnly.Substring(charToMask, charToKeep);

			return phoneNumber;
		}
	}