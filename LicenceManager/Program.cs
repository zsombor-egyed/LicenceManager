using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace LicenceManager
{
    class Program
    {   
        static void Main(string[] args)
        {
            var startDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string licenceKeyPath = startDirectory + "/"+"Licence.key";

            string privateKey = "Starschema4ever";           

            List <string> users = new List<string>();
            users.Add("company1");
            users.Add("company2");
            users.Add("company3");

            SHAProductKeyManager lm = new SHAProductKeyManager(privateKey, licenceKeyPath, users);
            lm.generateProductKeys();

            Console.WriteLine(lm.validateProductKey());


        }
    }
}