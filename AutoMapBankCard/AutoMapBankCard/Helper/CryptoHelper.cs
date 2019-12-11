using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AutoMapBankCard.Helper
{
    public class CryptoHelper
    {
        public class AES
        {
            public const string Key = "5367566B59703373357638792F423F45";
            public static string EncryptToHexString(string data, string key, Encoding encoding, string iv = null, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, int blockSize = 128)
            {
                var bData = encoding.GetBytes(data);
                var result = Encrypt(bData, key, encoding, cipherMode, paddingMode, blockSize, iv);
                return string.Join("", result.Select(b => b.ToString("x2")));
            }
            public static string DecryptFromHexString(string data, string key, Encoding encoding, string iv = null, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, int blockSize = 128)
            {
                var bData = Enumerable.Range(0, data.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(data.Substring(x, 2), 16)).ToArray();
                return encoding.GetString(Decrypt(bData, key, encoding, cipherMode, paddingMode, blockSize, iv));
            }
            public static string EncryptToBase64String(string data, string key, Encoding encoding, string iv = null, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, int blockSize = 128)
            {
                var bData = encoding.GetBytes(data);
                var result = Encrypt(bData, key, encoding, cipherMode, paddingMode, blockSize, iv);
                return Convert.ToBase64String(result);
            }
            public static string DecryptFromBase64String(string data, string key, Encoding encoding, string iv = null, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, int blockSize = 128)
            {
                var bData = Convert.FromBase64String(data);
                return encoding.GetString(Decrypt(bData, key, encoding, cipherMode, paddingMode, blockSize, iv));
            }
            private static byte[] Encrypt(byte[] data, string key, Encoding encoding, CipherMode cipherMode, PaddingMode paddingMode, int blockSize, string iv = null)
            {
                //after testing, key size will be set up automatically while you fill in key
                //note! some mode, such as ECB, will generate different result each time if you set up key size after key
                //but ECB should return same result each time 
                //so this function will not allow user to set up key size
                var aes = new RijndaelManaged
                {
                    Key = encoding.GetBytes(key),
                    Mode = cipherMode,
                    Padding = paddingMode,
                    BlockSize = blockSize
                };
                if (iv != null)
                    aes.IV = encoding.GetBytes(iv);

                return aes.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);
            }
            private static byte[] Decrypt(byte[] data, string key, Encoding encoding, CipherMode cipherMode, PaddingMode paddingMode, int blockSize, string iv = null)
            {
                //after testing, key size will be set up automatically while you fill in key
                //and note! if key size is set up after key, TransformFinalBlock will throw a exception
                //so this function will not allow user to set up key size
                var aes = new RijndaelManaged
                {
                    Key = encoding.GetBytes(key),
                    Mode = cipherMode,
                    Padding = paddingMode,
                    BlockSize = blockSize
                };
                if (iv != null)
                    aes.IV = encoding.GetBytes(iv);

                return aes.CreateDecryptor().TransformFinalBlock(data, 0, data.Length);
            }
        }
    }
}