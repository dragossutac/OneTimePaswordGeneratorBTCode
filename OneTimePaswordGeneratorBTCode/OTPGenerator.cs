using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Runtime.Intrinsics.Arm;

namespace OneTimePasswordGeneratorBTCode
{
    internal class OTPGenerator
    {
        private const int ValidityPeriodInSeconds = 30;

        public OneTimePassword GeneratePassword(string userId, DateTime dateTime)
        {
            string data = userId + dateTime.ToString("yyyyMMddHHmmss");

            var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var expirationTimestamp = currentTimestamp + ValidityPeriodInSeconds;

            string expirationTimestampString = expirationTimestamp.ToString();

            string password = HashSHA256(data + expirationTimestampString);

            return new OneTimePassword(password, expirationTimestamp);
        }
        public bool VerifyPasswordExpiration(OneTimePassword otp)
        {
            var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            return currentTimestamp <= otp.ExpirationTimestamp;
        }

        private string HashSHA256(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = SHA256.HashData(bytes);
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }
    }
    public class OneTimePassword
    {
        public string Password { get; }
        public long ExpirationTimestamp { get; }

        public OneTimePassword(string password, long expirationTimestamp)
        {
            Password = password;
            ExpirationTimestamp = expirationTimestamp;
        }
    }
}
