using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace QuotesConversion {
	public class QuotesConverter {
		private static char[] _separator = new char[] { ',' };
		private static CultureInfo _culture = CultureInfo.GetCultureInfo("en-US");
		private static DateTime _epoch = new DateTime(1970, 1, 1);
		public static DateTime GetQuoteDateTimeFromString(string text) {
			string[] parts = text.Split(_separator);
			long time = long.Parse(parts[0]);
			return _epoch + TimeSpan.FromMilliseconds(time);
		}
		public static void GetQuoteDateTimeFromString(string text, out DateTime dateTime) {
			string[] parts = text.Split(_separator);
			long time = long.Parse(parts[0]);
			dateTime = _epoch + TimeSpan.FromMilliseconds(time);
		}
		public static void GetQuoteDateTimeFromString(string text, out DateTime dateTime, out long time) {
			string[] parts = text.Split(_separator);
			time = long.Parse(parts[0]);
			dateTime = _epoch + TimeSpan.FromMilliseconds(time);
		}
		public static void GetQuoteFromString(string text, out DateTime dateTime, out decimal ask, out decimal bid) {
			string[] parts = text.Split(_separator);
			long time = long.Parse(parts[0]);
			dateTime = _epoch + TimeSpan.FromMilliseconds(time);
			ask = decimal.Parse(parts[1], _culture);
			bid = decimal.Parse(parts[2], _culture);
		}
		public static void GetQuoteFromString(string text, out DateTime dateTime, out long time, out decimal ask, out decimal bid) {
			string[] parts = text.Split(_separator);
			time = long.Parse(parts[0]);
			dateTime = _epoch + TimeSpan.FromMilliseconds(time);
			ask = decimal.Parse(parts[1], _culture);
			bid = decimal.Parse(parts[2], _culture);
		}
		public static bool GetQuoteDateTimeFromBinaryReader(BinaryReader reader, out DateTime dateTime) {
			if (reader == null) {
				dateTime = default(DateTime);
				return false;
			}
			try {
				long time = reader.ReadInt64();
				dateTime = _epoch + TimeSpan.FromMilliseconds(time);
				reader.ReadDecimal();
				reader.ReadDecimal();
				return true;
			}
			catch (Exception) {
				dateTime = default(DateTime);
				return false;
			}
		}
		public static bool GetQuoteDateTimeFromStreamReader(StreamReader reader, out DateTime dateTime) {
			if (reader == null) {
				dateTime = default(DateTime);
				return false;
			}
			try {
				string line = reader.ReadLine();
				GetQuoteDateTimeFromString(line, out dateTime);
				return true;
			}
			catch (Exception) {
				dateTime = default(DateTime);
				return false;
			}
		}
		public static bool GetQuoteFromBinaryReader(BinaryReader reader, out DateTime dateTime, out decimal ask, out decimal bid) {
			if (reader == null) {
				dateTime = default(DateTime);
				ask = default(decimal);
				bid = default(decimal);
				return false;
			}
			try {
				long time = reader.ReadInt64();
				dateTime = _epoch + TimeSpan.FromMilliseconds(time);
				ask = reader.ReadDecimal();
				bid = reader.ReadDecimal();
				return true;
			}
			catch (Exception) {
				dateTime = default(DateTime);
				ask = default(decimal);
				bid = default(decimal);
				return false;
			}
		}
	}
}
