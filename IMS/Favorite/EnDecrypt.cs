using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace IMS.Favorite
{
    /// <summary>
    /// Lớp dùng để ecrypt & decrypt 
    /// </summary>
    public class EnDecrypt
    {
        private string saltValue = "s@1tNamValue";
        private string hashAlgorithm = "SHA1";
        private int passwordIterations = 2;
        private string initVector = "@@nam1B2c35Fg7H8";
        private int keySize = 256;

        public EnDecrypt()
        {

        }

        public string Encrypt(string plainText, string passPhrase)
        {
            try
            {
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                                passPhrase,
                                                                saltValueBytes,
                                                                hashAlgorithm,
                                                                passwordIterations);

                byte[] keyBytes = password.GetBytes(keySize / 8);

                RijndaelManaged symmetricKey = new RijndaelManaged();

                symmetricKey.Mode = CipherMode.CBC;

                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                                 keyBytes,
                                                                 initVectorBytes);

                MemoryStream memoryStream = new MemoryStream();

                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                             encryptor,
                                                             CryptoStreamMode.Write);
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                cryptoStream.FlushFinalBlock();

                byte[] cipherTextBytes = memoryStream.ToArray();

                memoryStream.Close();
                cryptoStream.Close();

                string cipherText = Convert.ToBase64String(cipherTextBytes);

                return cipherText;
            }
            catch //(Exception ex)
            {
                return "Error in encrypting";   //Hide ex.Message in release version;
            }
        }

        public string Decrypt(string cipherText, string passPhrase)
        {
            try
            {
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

                PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                                passPhrase,
                                                                saltValueBytes,
                                                                hashAlgorithm,
                                                                passwordIterations);

                byte[] keyBytes = password.GetBytes(keySize / 8);

                RijndaelManaged symmetricKey = new RijndaelManaged();

                symmetricKey.Mode = CipherMode.CBC;

                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                                 keyBytes,
                                                                 initVectorBytes);
                /*  Read from cryptoStream is not a good way. Write to cryptoStream then 
                    read from memory stream is a better way.
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                              decryptor,
                                                              CryptoStreamMode.Read);

                byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                int decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                                           0,
                                                           plainTextBytes.Length);
                */
                MemoryStream memoryStream = new MemoryStream();

                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                              decryptor,
                                                              CryptoStreamMode.Write);
                cryptoStream.Write(cipherTextBytes, 0, cipherTextBytes.Length);
                // Close to flush the stream
                cryptoStream.Close();
                byte[] plainTextBytes = memoryStream.ToArray();
                memoryStream.Close();

                string plainText = Encoding.UTF8.GetString(plainTextBytes,
                                                           0,
                                                           plainTextBytes.Length);
                return plainText;
            }
            catch// (Exception ex)
            {
                return "Invalid encrypted string or password";  // ex.Message;
            }
        }
        // Encrypt a byte array. Return an encryted byte array
        // If encrypting failed, a null value will be returned
        public byte[] Encrypt(byte[] byData, string passPhrase)
        {
            try
            {
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                                passPhrase,
                                                                saltValueBytes,
                                                                hashAlgorithm,
                                                                passwordIterations);

                byte[] keyBytes = password.GetBytes(keySize / 8);

                RijndaelManaged symmetricKey = new RijndaelManaged();

                symmetricKey.Mode = CipherMode.CBC;

                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                                 keyBytes,
                                                                 initVectorBytes);

                MemoryStream memoryStream = new MemoryStream();

                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                             encryptor,
                                                             CryptoStreamMode.Write);
                cryptoStream.Write(byData, 0, byData.Length);

                cryptoStream.FlushFinalBlock();
                byte[] byEncryted = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();

                return byEncryted;
            }
            catch
            {
                return null;
            }
        }
        // Decrypt an encryted byte array. Return a decryted byte array
        // If decrypting failed, a null value will be returned
        public byte[] Decrypt(byte[] byEncrytedData, string sPassPhrase)
        {
            try
            {
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                                sPassPhrase,
                                                                saltValueBytes,
                                                                hashAlgorithm,
                                                                passwordIterations);

                byte[] keyBytes = password.GetBytes(keySize / 8);

                RijndaelManaged symmetricKey = new RijndaelManaged();

                symmetricKey.Mode = CipherMode.CBC;

                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                                 keyBytes,
                                                                 initVectorBytes);

                MemoryStream memoryStream = new MemoryStream();

                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                              decryptor,
                                                              CryptoStreamMode.Write);
                // Write the data and make it do the decryption 
                cryptoStream.Write(byEncrytedData, 0, byEncrytedData.Length);
                // Close the crypto stream (or do FlushFinalBlock). 
                // This will tell it that we have done our decryption
                // and there is no more data coming in     
                cryptoStream.Close();
                // Set the decrypted data from the MemoryStream. 
                byte[] byDecryted = memoryStream.ToArray();
                memoryStream.Close();
                return byDecryted;
            }
            catch
            {
                return null;
            }
        }
        // Encrypt a file and output a new another encrypted one
        public bool EncryptFile(string sInFile, string sOutFile, string sPassPhrase)
        {
            try
            {
                byte[] byData = System.IO.File.ReadAllBytes(sInFile);
                if (byData == null)
                    return false;
                byte[] byEncrytedData = Encrypt(byData, sPassPhrase);
                if (byEncrytedData == null)
                    return false;
                System.IO.File.WriteAllBytes(sOutFile, byEncrytedData);
                return true;
            }
            catch
            {
                return false;
            }
        }
        // Decrypt a file and return a byte array of decrypted data
        // If failed, a null value will be returned
        public byte[] DecryptFile(string sInFile, string sPassPhrase)
        {
            try
            {
                byte[] byEncrytedData = System.IO.File.ReadAllBytes(sInFile);
                if (byEncrytedData == null)
                    return null;
                return Decrypt(byEncrytedData, sPassPhrase);
            }
            catch
            {
                return null;
            }
        }
        // Decrypt a file and output a new another decrypted one        
        // Return false if failed , true otherwise
        public bool DecryptFile(string sInFile, string sOutFile, string sPassPhrase)
        {
            try
            {
                byte[] byEncrytedData = System.IO.File.ReadAllBytes(sInFile);
                if (byEncrytedData == null)
                    return false;
                byte[] byDecrytedData = Decrypt(byEncrytedData, sPassPhrase);
                if (byDecrytedData == null)
                    return false;
                System.IO.File.WriteAllBytes(sOutFile, byDecrytedData);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string MD5(string str)
        {
            if (str == null) return "";
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(str);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }
    }
}
