using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace rlcx.suid
{
    /// <summary>
	/// Converts between binary data and an Ascii85-encoded string.
	/// Source : https://github.com/LogosBible/Logos.Utility/blob/master/src/Logos.Utility/Ascii85.cs
	/// </summary>
	/// <remarks>See <a href="http://en.wikipedia.org/wiki/Ascii85">Ascii85 at Wikipedia</a>.</remarks>
	internal static class Ascii85
	{
        
	    // the first and last characters used in the Ascii85 encoding character set
	    private const char CFirstCharacter = '!';

		/// <summary>
		/// Encodes the specified byte array in Ascii85.
		/// </summary>
		/// <param name="bytes">The bytes to encode.</param>
		/// <returns>An Ascii85-encoded string representing the input byte array.</returns>
		public static string Encode(byte[] bytes)
		{
			if (bytes == null)
				throw new ArgumentNullException(nameof(bytes));

			// preallocate a StringBuilder with enough room to store the encoded bytes
			StringBuilder sb = new StringBuilder(bytes.Length * 5 / 4);

			// walk the bytes
			var count = 0;
			uint value = 0;
			foreach (var b in bytes)
			{
				// build a 32-bit value from the bytes
				value |= ((uint) b) << (24 - (count * 8));
				count++;

				// every 32 bits, convert the previous 4 bytes into 5 Ascii85 characters
			    if (count != 4) continue;
			    if (value == 0)
			        sb.Append('z');
			    else
			        EncodeValue(sb, value, 0);
			    count = 0;
			    value = 0;
			}

			// encode any remaining bytes (that weren't a multiple of 4)
			if (count > 0)
				EncodeValue(sb, value, 4 - count);

			return sb.ToString();
		}

		// Writes the Ascii85 characters for a 32-bit value to a StringBuilder.
		private static void EncodeValue(StringBuilder sb, uint value, int paddingBytes)
		{
			var encoded = new char[5];

			for (var index = 4; index >= 0; index--)
			{
				encoded[index] = (char) ((value % 85) + CFirstCharacter);
				value /= 85;
			}

			if (paddingBytes != 0)
				Array.Resize(ref encoded, 5 - paddingBytes);

			sb.Append(encoded);
		}

	}
}
