using System;
using OneTimePasswordGeneratorBTCode;


class Program
{
    static void Main(string[] args)
    {
        string userId = "dragos.sutac";
        DateTime dateTime = DateTime.Now;

        OTPGenerator generator = new OTPGenerator();
        OneTimePassword otp = generator.GeneratePassword(userId, dateTime);

        Console.WriteLine("Generated Password: " + otp.Password);

        bool isValid = generator.VerifyPasswordExpiration(otp);
        Console.WriteLine("Password Validity: " + isValid);

        System.Threading.Thread.Sleep(5000);

        isValid = generator.VerifyPasswordExpiration(otp);
        Console.WriteLine("Password Validity after 5 seconds: " + isValid);

        System.Threading.Thread.Sleep(10000);

        isValid = generator.VerifyPasswordExpiration(otp);
        Console.WriteLine("Password Validity after 15 seconds: " + isValid);

        System.Threading.Thread.Sleep(16000);

        isValid = generator.VerifyPasswordExpiration(otp);
        Console.WriteLine("Password Validity after 31 seconds: " + isValid);

        Console.ReadKey();
    }
}
