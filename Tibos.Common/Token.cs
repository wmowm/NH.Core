using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
namespace Tibos.Common
{
    public class Token
    {
        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="user_name"></param>
        /// <param name="password"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public string CreateToken(string user_name,string password,string sign)
        {
            //加密规则
            //1.用户名 + 密码  进行md5加密 得到加密字符串
            //2.加密字符串 + 签名 进行md5加密  得到加密字符串2
            //3.加密字符串转大写
            //4.去掉字符串里面的-
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(user_name + password));
                var result2 = md5.ComputeHash(Encoding.UTF8.GetBytes(result + sign));
                var strResult = BitConverter.ToString(result2);
                string result3 = strResult.Replace("-", "");
                return result3;
            }
        }
    }
}
