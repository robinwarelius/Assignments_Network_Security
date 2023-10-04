using IoT_Unit.Model;
using IoT_Unit.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IoT_Unit.Utilities
{
    // Krypterar min data
    public static class Encryption
    {
        public static EncryptedDto EncryptData(string data_to_encrypt)
        {
            using (Aes myAes = Aes.Create())
            {
                EncryptedDto encryptedDto = EncryptStringToBytes_Aes(data_to_encrypt, myAes.Key, myAes.IV);  
                return encryptedDto;
            }
        }
        private static EncryptedDto EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            EncryptedDto encryptDto = new EncryptedDto();
            
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
          
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                encryptDto.IV = IV;
                encryptDto.Key = Key;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                        encryptDto.SecretValue = encrypted;
                    }
                }
            }
            
            return encryptDto;
        }

    }
}
