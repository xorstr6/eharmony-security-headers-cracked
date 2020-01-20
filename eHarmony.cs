using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "eHarmony";

            string email = "info@test.com";
            string password = "password";
            string clientid = RandomDeviceID(16);
            long unixTimestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var cfauth = HmacSHA256(unixTimestamp + "," + clientid + "," + email + "," + password, "dIH8PImtz8cnVEZCbLSwhy4YcW9vQpyE");
            var authorization = Base64Encode(clientid + ":3=?gBo*7Rkp,_Cg%)7h=u");
            var clienttimestamp = unixTimestamp;
            Console.WriteLine("cf_auth: " + cfauth);
            Console.WriteLine("Authorization: Basic " + authorization);
            Console.WriteLine("client_timestamp: " + clienttimestamp);
            Console.ReadLine();
        }

        public static Random random = new Random();
        public static string RandomDeviceID(int length)
        {
            const string chars = "abcdef0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string HmacSHA256(string s1, string key)
        {
            var hmac = new HMac(new Sha256Digest());
            hmac.Init(new KeyParameter(Encoding.UTF8.GetBytes(key)));
            byte[] result = new byte[hmac.GetMacSize()];
            byte[] bytes = Encoding.UTF8.GetBytes(s1);

            hmac.BlockUpdate(bytes, 0, bytes.Length);
            hmac.DoFinal(result, 0);

            return Base64.ToBase64String(result);
        }


    }
}
