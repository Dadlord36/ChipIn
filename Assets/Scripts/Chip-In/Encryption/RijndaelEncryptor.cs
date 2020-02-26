using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encryption
{
    public static class RijndaelEncryptor
    {
        private static byte[] IvBytes => Encoding.ASCII.GetBytes("1234567890123456");

        /// <summary>
        /// Encodes the given original string
        /// </summary>
        /// <param name="original">String to be encoded</param>
        /// <param name="key">Encryption key, myst be 32 chars</param>
        public static byte[] Encrypt(string original, string key)
        {
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);

            using (RijndaelManaged myRijndaelManaged = new RijndaelManaged())
            {
                myRijndaelManaged.Key = keyBytes;
                myRijndaelManaged.IV = IvBytes;

                return EncryptStringToBytes(original, myRijndaelManaged.Key, myRijndaelManaged.IV);
            }
        }

        public static string Decrypt(byte[] original, string key)
        {
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            using (RijndaelManaged myRijndaelManaged = new RijndaelManaged())
            {
                myRijndaelManaged.Key = keyBytes;
                myRijndaelManaged.IV = IvBytes;

                return DecryptStringFromBytes(original, myRijndaelManaged.Key, myRijndaelManaged.IV);
            }
        }

        private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException(nameof(plainText));
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException(nameof(iv));
            byte[] encrypted;
            // Create an Rijndael object
            // with the specified key and IV.
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException(nameof(cipherText));
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException(nameof(iv));

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Rijndael object
            // with the specified key and IV.
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        private static string BytesToString(byte[] bytesToConvert)
        {
            char[] chars = new char[bytesToConvert.Length / sizeof(char)];
            Buffer.BlockCopy(bytesToConvert, 0, chars, 0, bytesToConvert.Length);
            return new string(chars);

            return Encoding.ASCII.GetString(bytesToConvert);
        }

        private static byte[] StringToBytes(in string stringToConvert)
        {
            return Encoding.Default.GetBytes(stringToConvert);
        }
    }
}