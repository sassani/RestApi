using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EF_V0.Core.Helpers
{
	public class StringHelper
	{
		public static string GenerateRandom(int length)
		{
			string randomChars =
				"ABCDEFGHJKLMNOPQRSTUVWXYZ" +    // uppercase 
				"abcdefghijkmnopqrstuvwxyz" +    // lowercase
				"0123456789";                   // digits


			Random rand = new Random(Environment.TickCount);
			List<char> chars = new List<char>();

			for (int i = chars.Count; i < length; i++)
			{
				chars.Insert(rand.Next(0, chars.Count), randomChars[rand.Next(0, randomChars.Length)]);
			}

			return string.Join("", chars);
		}

		#region Base64
		public static string Base64Encode(string plainText)
		{
			var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			return Convert.ToBase64String(plainTextBytes);
		}

		public static string Base64Decode(string base64EncodedData)
		{
			var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
			return Encoding.UTF8.GetString(base64EncodedBytes);
		}
		#endregion

		#region Hashing Methods
		// ref
		// https://en.wikipedia.org/wiki/Bcrypt
		// http://www.php.net/manual/en/function.crypt.php

		public static string StringToHash(string plainText, int saltLength = 22, int cost = 10)
		{
			string salt = StringHelper.GenerateRandom(saltLength);
			return GenerateHashedString(plainText, salt, cost);
		}

		public static bool CompareStringToHash(string hashedString, string plainText)
		{
			var match = Regex.Match(hashedString, @"\$(\d+)\$(\d+)\$(\w+)\$(\w+)");

			if (match.Success)
			{
				string algoritm = match.Groups[1].Value;
				int cost = int.Parse(match.Groups[2].Value);
				string salt = match.Groups[3].Value;

				return hashedString == GenerateHashedString(plainText, salt, cost);
			}

			return false;
		}

		private static string GenerateHashedString(string plainText, string salt, int cost = 10)
		{
			byte[] password = Encoding.UTF8.GetBytes(salt + "$" + plainText);

			using (HashAlgorithm sha256 = new SHA256Managed())
			{
				for (int i = 0; i <= cost; i++)
					password = sha256.ComputeHash(password);
			}

			return $"$5${cost}${salt}$" + Convert.ToBase64String(password);
		}
		#endregion
	}
}
