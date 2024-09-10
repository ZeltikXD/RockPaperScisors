namespace RockPaperScisors.Code
{
    public static class CryptRandom
    {
        public static int Next(byte[] key, int minValue = 0, int maxExclusiveValue = int.MaxValue)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(minValue, maxExclusiveValue);
            ThrowIfNegative(minValue);

            long diff = (long)maxExclusiveValue - minValue;

            uint ui = GetUInt(key);
            return (int)(minValue + (ui % diff));
        }

        private static uint GetUInt(byte[] key)
        {
            if (key.Length < 4)
                throw new ArgumentException("The key must generate at least 4 bytes for a valid UInt32.");

            return BitConverter.ToUInt32(key, 0);
        }

        private static void ThrowIfNegative(int min)
        {
            if (min < 0) throw new ArgumentOutOfRangeException(nameof(min), "Minimum value cannot be negative.");
        }
    }
}
