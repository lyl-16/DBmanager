using System.Text;
using System.Security.Cryptography;

//用于密码加密，MD5加密方式
namespace Crypto
{
    class md5Crypto
    {
        public static string MD5Encrypt32(string password)
        {
            string cl = password;
            string pwd = "";
            MD5 md5 = MD5.Create(); 

            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            
            for (int i = 0; i < s.Length; i++)
            { 
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }
    }
}