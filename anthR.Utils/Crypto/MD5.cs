using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Utils.Crypto
{
    public class MD5
    {

        public static string GetHash(string value){

            byte[] source;
            byte[] hash;

            source = ASCIIEncoding.ASCII.GetBytes(value);
            hash = new MD5CryptoServiceProvider().ComputeHash(source);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();

        }

    }
}
