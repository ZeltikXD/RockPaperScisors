using System.Security.Cryptography;
using System.Text;

namespace RockPaperScisors.Code
{
    public static class HmacGenerator
    {
        public static string GenerateKey(int size = 32)
        {
            var bytes = RandomNumberGenerator.GetBytes(size);
            return BitConverter.ToString(bytes).Replace("-", "").ToUpper();
        }

        public static string GenerateHmac(string key, string data)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            using var hmac = new HMACSHA256(keyBytes);
            var dataBytes = Encoding.UTF8.GetBytes(data);
            var hashBytes = hmac.ComputeHash(dataBytes);
            
            var strBuilder = new StringBuilder();
            foreach (var b in hashBytes)
                strBuilder.Append(b.ToString("x2"));

            return strBuilder.ToString().ToUpper();
        }
    }
}
