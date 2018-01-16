using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CSharpLibrary.EncryptDecrypt
{
    public class Crypto
    {
        public void Run()
        {
            var exit = false;
            while (!exit)
            {
                Console.WriteLine("Please select an option: \n1. Encrypt/Decrypt string 2. Encrypt/Decrypt connectionstrings in xml file/files:");
                var option = 0;
                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid option selected");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        ModifyString();
                        break;
                    case 2:
                        ModifyFiles();
                        break;
                    default:
                        Console.WriteLine("Invalid option selected");
                        continue;
                }

                Console.Write("Do you want to continue (y/n) ");
                exit = string.Equals(Console.ReadLine(), "y", StringComparison.OrdinalIgnoreCase) ? false : true;
            }
        }

        private void ModifyString()
        {
            Console.WriteLine("Select an Option. 1.Encrypt 2.Decrypt");
            var option = 0;
            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invalid option selected");
                return;
            }

            using (Aes aes = Aes.Create())
            {
                SetKeys(aes);
                if (option == 1)
                {
                    Console.WriteLine("Enter the text to Encrypt:");
                    Console.WriteLine(Encrypt(Console.ReadLine(), aes));
                }
                else if (option == 2)
                {
                    Console.WriteLine("Enter the Encrypted text to Decrypt:");
                    Console.WriteLine(Decrypt(Console.ReadLine(), aes));
                }
            }
        }

        private void ModifyFiles()
        {
            Console.WriteLine("Enter the path of config file or folder containing config files: ");
            var path = Console.ReadLine();
            using (Aes aes = Aes.Create())
            {
                SetKeys(aes);
                Console.WriteLine("Select an Option. 1.Encrypt 2.Decrypt");
                var option = 0;
                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid option selected");
                    return;
                }

                if (Directory.Exists(path))
                {
                    foreach(var filePath in Directory.GetFiles(path, "Web*config"))
                    {
                        ModifyConfigFile(filePath, aes, option == 1);
                    }
                }
                else if (File.Exists(path))
                {
                    ModifyConfigFile(path, aes, option == 1);
                }
            }
        }

        private void ModifyConfigFile(string path, Aes aes, bool encrypt)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNodeList connectionStringNodes = doc.SelectNodes("descendant::connectionStrings");
            foreach (XmlNode connectionStringNode in connectionStringNodes)
            {
                XmlNodeList connectionStrings = connectionStringNode.SelectNodes("descendant::add");
                foreach(XmlNode connectionString in connectionStrings)
                {
                    XmlAttribute connection = connectionString.Attributes["connectionString"];
                    if (connection != null)
                    {
                        connection.Value = encrypt ? Encrypt(connection.Value, aes) : Decrypt(connection.Value, aes);
                    }
                }
            }

            doc.Save(path);
        }

        private void SetKeys(Aes aes)
        {
            Console.Write("Enter Key to Encrypt/Decrypt: ");
            var k = new Rfc2898DeriveBytes(Console.ReadLine(), Encoding.UTF8.GetBytes("Sample Salt"), 1000);
            aes.Key = k.GetBytes(32);
            aes.IV = k.GetBytes(16);
        }

        private string Encrypt(string plainText, Aes aes)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (aes.Key == null || aes.Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (aes.IV == null || aes.IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        private string Decrypt(string cipherText, Aes aes)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (aes.Key == null || aes.Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (aes.IV == null || aes.IV.Length <= 0)
                throw new ArgumentNullException("IV");

            string plaintext = null;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
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
