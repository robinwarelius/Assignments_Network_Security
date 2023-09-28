using IoT_BackEnd.Models.Dto;
using System.Security.Cryptography;

namespace IoT_BackEnd.Utilities
{
    public static class Decryption
    {
        public static async Task <string> DecryptData (EncryptedDto encryptedDto)
        {
            using (Aes myAes = Aes.Create())
            {            
                string secretValue = await DecryptStringFromBytes_Aes(encryptedDto.SecretValue, encryptedDto.Key, encryptedDto.IV);
                return secretValue;         
            }
        }

        private static async Task <string> DecryptStringFromBytes_Aes(byte[] value, byte[] Key, byte[] IV)
        {
            
            if (value == null || value.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            
            string plaintext = null;
          
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

             
                using (MemoryStream msDecrypt = new MemoryStream(value))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {                       
                            plaintext = srDecrypt.ReadToEnd();                           
                        }
                    }
                }

                return plaintext;
            }         
        }
    }
}
