using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace LicenceManager
{
    /// <summary>
    /// This class concatenate the comany names with the private key, and generate product keys for them.
    /// And validate the product key in the Licence.key file
    /// </summary>
    class SHAProductKeyManager : ILicenceManager
    {
        string privateKey;
        string licenceKeyPath; //file path for licence file
        List<string> users; //cointains name of the companies who has licence
        Dictionary<string, string> ProductKeys; // <companynme, productkey>

        public SHAProductKeyManager(string privateKey, string licenceKeyPath, List<string> users )
        {
            this.licenceKeyPath = licenceKeyPath;
            this.privateKey = privateKey;
            this.users = users;
            ProductKeys = new Dictionary<string, string>();
        }

        /// <summary>
        /// Generate product keys for companies
        /// take them to a Dictionary and write it to a file
        /// </summary>
        public void generateProductKeys()
        {
            foreach (string user in users)
            {
                ProductKeys.Add(user, generateKey(user));
            }

            // save it to a file
            var startDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            File.WriteAllLines(startDirectory+"/"+"ProductKeys.txt",
               ProductKeys.Select(x => "[" + x.Key + " " + x.Value + "]").ToArray());

        }

        /// <summary>
        /// Generate hash for username and Private Key with sha256
        /// </summary>
        /// <param name="usernameAndPrivateKey"></param>
        /// <returns></returns>
        string sha256(string usernameAndPrivateKey)
        {
            SHA256Managed crypt = new SHA256Managed();
            StringBuilder hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(usernameAndPrivateKey), 0, Encoding.UTF8.GetByteCount(usernameAndPrivateKey));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        /// <summary>
        /// Generate Product key for username/comanyname
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        string generateKey(string username)
        {
            return sha256(username + privateKey);
        }

        /// <summary>
        /// Chekc if the Licence.key file contains a valid productnumber.
        /// </summary>
        /// <returns></returns>
        public Boolean validateProductKey()
        {
            string productKey = File.ReadAllText(licenceKeyPath);
            foreach (string user in users)
            {
                if (productKey.Equals(sha256(user + privateKey)))
                    return true;
            }
            return false;
        }
    }
}
