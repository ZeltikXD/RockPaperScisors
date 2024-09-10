using System.Security.Cryptography;
using System.Text;

namespace RockPaperScisors.Code
{
    public static class HmacGenerator
    {
        public static byte[] GenerateKey(int size = 32)
        {
            return RandomNumberGenerator.GetBytes(size);     
        }

        public static byte[] GetBytes(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static string ToString(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "").ToUpper();
        }

        public static string GenerateHmac(byte[] key, string data)
        {
            using var hmac = new HMACSHA256(key);
            var dataBytes = GetBytes(data);
            var hashBytes = hmac.ComputeHash(dataBytes);
            
            var strBuilder = new StringBuilder();
            foreach (var b in hashBytes)
                strBuilder.Append(b.ToString("x2"));

            return strBuilder.ToString().ToUpper();
        }
    }
}
