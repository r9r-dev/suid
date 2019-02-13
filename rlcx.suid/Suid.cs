using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace rlcx.suid
{
    public static class Suid
    {
        private const string Chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789&é№'(-è_çà)=²~#{[|`ↈ^@]}°+¨$£¤%ùµ*<>,?;.:/!§¡¢¥¦©ª¬®±¶·¿ΓΔΘΛΞΠΣΦΨΩαβγδεθλπφωℳ™℮ⅎ⅐⅑⅒⅓⅔⅕⅖⅗⅘⅙⅚⅛⅜⅝⅞ↀↁↂↆↇ←↑→↓↔↗↙∞∟∩∫∴∶≈≠≡≤≥⋲⓿❶❷❸❹❺❻❼❽❾■▪▫▲▼◊○●♠♡♣♥♦♯†‡•…‰‼⁰¹²³⁴⁵⁶⁷⁸⁹⁺⁻⁼⁽⁾ⁿ₀₁₂₃₄₅₆₇₈₉₊₋₌₍₎₦₩₫€₭₮₱₲₳₴₶℅ℓ❿";
        private static char[] Chars64 = "abcdefghijklmnopqrstuvwxyz0123456789_-+=ùéàç@(){}$£¤€#~§!ê°ëãñ%µ".ToCharArray();
        private static char[] CharsBase64 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+/".ToCharArray();
        private static readonly Random Rand = new Random();
        private static ushort _uniquifier = (ushort) Rand.Next(0, 65536);

        /// <summary>
        /// Generate a 16 lowercase characters Suid
        /// </summary>
        public static string NewLettersOnlySuid()
        {
            var bytes = GetUnique();

            // Conversion
            var result = string.Empty;
            foreach (var b in bytes)
            {
                var upper = b >> 4;
                var lower = b & 0x0F;
                result += Chars[upper];
                result += Chars[lower];
            }
            return result;
        }

        /// <summary>
        /// Generate a 8 copy pastable characters Suid (base256)
        /// </summary>
        public static string NewTinySuid()
        {
            var bytes = GetUnique();

            // Conversion
            var result = string.Empty;
            foreach (var b in bytes)
            {
                result += Chars[b];
            }

            return result;
        }

        /// <summary>
        /// Generate a 10 printable characters Suid
        /// </summary>
        /// <returns>string representation</returns>
        public static string NewSuid()
        {
            var bytes = GetUnique();
            return Ascii85.Encode(bytes);
        }

        public static string NewUrlFriendlySuid()
        {
            var bytes = GetUnique();
            return Convert.ToBase64String(bytes).Replace("=", "");
        }

        public static string NewFilenameSuid()
        {
            var bytes = GetUnique();
            var base64 = Convert.ToBase64String(bytes).Replace("=", "");
            var barray = base64.ToCharArray();
            var result = string.Empty;

            for (int i = 0; i < barray.Length; i++)
            {
                result += Chars64[Array.IndexOf(CharsBase64, barray[i])];
            }
            return result;
        }

        /// <summary>
        /// Convert a Guid into it's truly unique TinySuid representation
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public static string ToTinySuid(this Guid g)
        {
            var guid = g.ToByteArray();

            // Conversion
            var result = string.Empty;
            foreach (var b in guid)
            {
                result += Chars[b];
            }

            return result;
        }

        /// <summary>
        /// Convert a Guid into it's truly unique Suid representation
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public static string ToSuid(this Guid g)
        {
            var guid = g.ToByteArray();
            return Ascii85.Encode(guid);
        }

        private static byte[] GetUnique()
        {
            var date = DateTime.UtcNow - new DateTime(2018, 8, 20, 12, 0, 0);
            var value = (uint) date.TotalSeconds;
            var dateBytes = BitConverter.GetBytes(value).Reverse().ToArray();

            var bytes = new byte[7];
            dateBytes.CopyTo(bytes, 2);

            // uniquifier (0 à 65535)
            _uniquifier++;
            bytes[0] = (byte) _uniquifier;
            bytes[1] = (byte) (_uniquifier >> 8);

            // randomization (0 à 255)
            int rand = Rand.Next(0, 256);
            bytes[2] += (byte) rand;

            return bytes;
        }
    }
}
