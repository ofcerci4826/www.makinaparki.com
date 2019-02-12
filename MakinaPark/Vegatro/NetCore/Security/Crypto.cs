using System.Security.Cryptography;
using System.Text;

namespace Vegatro.NetCore.Security
{
    /// <summary>
    /// Crytpo class
    /// </summary>
    public class Crypto
    {
        /// <summary>
        /// MD5
        /// </summary>
        /// <param name="text">text to crypt in MD5</param>
        /// <returns>MD5 crypted string</returns>
        public static string MD5(string text)
        {
            // MD5CryptoServiceProvider nesnenin yeni bir instance'sını oluşturalım.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            //Girilen veriyi bir byte dizisine dönüştürelim ve hash hesaplamasını yapalım.
            byte[] btr = Encoding.UTF8.GetBytes(text);
            btr = md5.ComputeHash(btr);

            //byte'ları biriktirmek için yeni bir StringBuilder ve string oluşturalım.
            StringBuilder sb = new StringBuilder();

            //hash yapılmış her bir byte'ı dizi içinden alalım ve her birini hexadecimal string olarak formatlayalım.
            foreach (byte ba in btr)
                sb.Append(ba.ToString("x2").ToUpper());

            return sb.ToString();
        }
    }
}
