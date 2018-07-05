using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Tibos.Common;

namespace Tibos.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            var mm = HttpCommon.HttpPost("http://api.1cloudsp.com/intl/api/v2/send", "accesskey=as5xjYLRv6ehVZL7&secret=GwxOKj0WOOon4NAJi52s0I4GnZqlGZ2u&sign=【IDCM】&templateId=999&mobile=8615019400599&content=27672");

            string param = "json=kfPL3B20IkN2F7W85/3qrJjUfphS83IZp9VwT3no/AxLfwG46DUu3+cmzxgyGxoUmjpnKtmbzkdsDLQrIiISJGRpv2L4oJRhcpXjYKbeopqhNJ16Mnk0qH2Z2Y3LvSJAPi/4tgcjNY52uED7etWgAoeOWKlR1VBq88eJBH0wlr/nGesDHH2lMl4VAu99xE8CKwnfsI2R+BJxTdZzQIAjQjGWeTvNqArcXN+VBD/9o16x9OTpMXqNg7itaWZ1ddWbC4nIdO9KoXv7vd02dhNwY2ovAzUorNj+J5xnLfVZZqp44jxcLw2a7AGZ3TMlfinTU76mbm4+n/rhMpdryWTwyw==";
            HttpCommon.HttpPost("http://preapi.idcm.io:8303/api/ExternalAPI/SignIn", param); 


            //2048 公钥
            string publicKey =
                "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwnsbSgsVdgscvfHpcwIE+3/Ny5swzVlUpJMveQDmdNool++SESap8r9Vhsc1lvO8W3omj6KAQ0j6as8ZTSGS3ngp0uf7nTgnuVxLfCF8UNhtOkbWomGZWoobM8LM+RX0VzzbPsVB7q0gmUPkH4M2TcPV2cjDyl2t3Om0ZDh0qaQjtTCUrc6BmCfYoAZpBoMIlM8gUViBi9TxxW2HvKlreUkM/IOyikRRmkIm/rkhqHO4z5HQoZzz0+QkxoqFMmkV5YZaoM0DQ7957vMNZUOxvmjg17GjJHVJzF/Keo25bEYdvnTVnjmAc+EMc+6qjrFIVuhE+T5l3rfE+4jqdzkN+QIDAQAB";
            
            
            
            
            //2048 私钥
            string privateKey =
                "MIIEogIBAAKCAQEAwnsbSgsVdgscvfHpcwIE+3/Ny5swzVlUpJMveQDmdNool++SESap8r9Vhsc1lvO8W3omj6KAQ0j6as8ZTSGS3ngp0uf7nTgnuVxLfCF8UNhtOkbWomGZWoobM8LM+RX0VzzbPsVB7q0gmUPkH4M2TcPV2cjDyl2t3Om0ZDh0qaQjtTCUrc6BmCfYoAZpBoMIlM8gUViBi9TxxW2HvKlreUkM/IOyikRRmkIm/rkhqHO4z5HQoZzz0+QkxoqFMmkV5YZaoM0DQ7957vMNZUOxvmjg17GjJHVJzF/Keo25bEYdvnTVnjmAc+EMc+6qjrFIVuhE+T5l3rfE+4jqdzkN+QIDAQABAoIBAHUJwRJ+ORwg89sbinI79OOltiOh8WyjJd0k9pzLfeU0pNlKw4yux/feTYoeuJFvfRtQF4S5NpdHg+xjVcalPc3EYucZ1MJ42O8kLAk33aiJlrJLjz+JnNBv7I7p3JDKNZGKfib4bwVECyoxQUf1nuiNwlSeDbXrSoZ33qexkgPER1zm2TUUi/17Wo62p1/J5l8SoxebaATzme0YWdP6rdbGIQ3ujYsgBxql+txWgNgiy0Yfduv/msdzL4X4oiZZa1XV8ivW8KyNs4XiUm5rIehQFmlYf1an/1qhHQvQXM+k0Bh8v5VJ8LflxkYddLdVnW+dPjT9FRegI4mrfjfgTkECgYEA41h2bwWFIn6CbeF+4eJxpdZENt5GKDDjCQB0jMj2Lx4hrE2cfOEybORAsE8RL2nLGKiTII2+bMqvHY1Sover5C/NraKCXyHPTji27eubTbPUWFfgIB/EvucXs/MqTn9W1xtZZGdDq1Ioxy87/0f9ervwSo1IKEZYQNkYNnEhulsCgYEA2v47TUCNE8r7Je12qf526jcpljIsNgrm6HQOObQcvVVP7wpmqvPcSlCPMEIUzbJupygRS/3p6E0kDgS4FzOaFKK1MlSI+eSsxN+H4eKQ6uzdcmvIG8sAyFww+4v5iNXeWXPFMOp2Qsog95y96zgYgrFU8Q91ZXHPMMetnUIGQTsCgYBlSW5KfD0aZx6Y8dPjs01KwzFU+KZtFYqw4gELECWOTHBYIaPMh1A971tasX9Ijmurqr8Ry9TBB3QJSIM+k/WDDkEmULagx6FiiiYFzeg9MBc03MG/zieLnc2ToIyCuHzqDQdAkjk1xL7iLwsd6ublnYGq1VMGjoCXM6Fz7+dE7QKBgEPnKrokdtoZSVCUVgQV7AdpvVJeEklbjger3LFVwMeQSW3EWttNLBQ68Hs1MkApwJfCG8LlY37tVG2oVvCSxob5gJevkJ1zo4KUEQ0gdHDzqyKMewrrIj3+IA4Pc/tS3VP9SrqFKNBC3oAIsPbbePYlSEJK2crvxB/K+nFVhJz7AoGAFx/SJsinfHcuvSrgSQdNbORmJbqnYULh2LDKp/z15D29Anya3Qzq/pHdTb8wOC7FM388pMy9eyNaga50GzfhhAiqm5CnKoMRjFWwTUVe33w+JA2an/ZHObeb/DyRuf8KrPJT7QuLCveQg5RA4mXNeBW1dBS0ebUqn1vOs8R8+X0=";




            //1024 私钥
            string publicKey2 = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDCA++n8NNERrGNAq9w+sKYCW/2gc5yfBDvfwf1W6P1zA4VANyQr9Kr3WmJRi6TUFfrHpOKNApJRNgcKegxJAvxAih1v6z2KGS/OFaojBr+/06MbZ/vVNpQrMr00z5wRFI1hQ8C8o7xRmMuDQtfHr5DAM5nF7tIUNjD2N2AT2c3FwIDAQAB";

            string privateKey2 = "MIICXAIBAAKBgQDCA++n8NNERrGNAq9w+sKYCW/2gc5yfBDvfwf1W6P1zA4VANyQr9Kr3WmJRi6TUFfrHpOKNApJRNgcKegxJAvxAih1v6z2KGS/OFaojBr+/06MbZ/vVNpQrMr00z5wRFI1hQ8C8o7xRmMuDQtfHr5DAM5nF7tIUNjD2N2AT2c3FwIDAQABAoGAcVd3VDb+VO6vnt8ouunjcIpi3DFs97i+9ArUDWRh3qA8wcxlDimI+1o34zga0XioQ/Ty4FQShkcvsRRSTxLt3fM61VSq0Aa8/8vtgzGHS8tnLc3kNJniJuhP6EDHowyEC7lsOxiwH6dr2GNH4Yts0x/Xz9GzZB9KV4wIY57tjtECQQDsYFoIeevJxmeoxUHfdtePKiZTnkVG1Qu01+m/b+PKu0bJLEtdgDvMYxeaB+potHRDLNKxWBUWfWPN9XE3kG2NAkEA0h9L9Jt+HcCgfbujlOMviZWloTUzXlWAmIb/rc+6CS6RWoRQOrN5y7aCskr7lTERL9gQTeuNgTewuDiv8tP0MwJAcE3C2N4cQYxrOYsmpeYPuiw7c2Tx4xpbanteyh596pcZpYDO+chwIMY/s2XpX//EcRn9rFZ+BmZiobroJI8RDQJBAJIfwaqyF9qJAxNtUi1QcNa1gyHA5aBMxclM2LH/K9kG0X6pVUH9Xk4U9n8XNi5imRk0oOIPVbDvEa6LfZDZZZECQF/BcEVRB7txuIryMUQTHoguAn9yIdmiyEAdfujlFcEo7b9vyh3grr29vAC95jy4fjXNBPSgffE76MTGNmCykvE=";



           var rsa = new RSAHelper(RSAType.RSA, Encoding.UTF8, privateKey, publicKey);

           var test = new RSACrypto(privateKey,publicKey);


            string str = "123456";

            Console.WriteLine("原始字符串：" + str);

            //加密
            string enStr = rsa.Encrypt(str);
            enStr = "NR4tG5D/sRriuNoonT5AqRMNetmpcnndgQ8Rr8c3AJbkBwSSejlYl7Xraqi4UDiYZfAxLMf2sPpwT9IJmTGBO+xRpaHN0BumgRE1w+5quB7wae6zagIE5DC+M8dkC+kuBSPD8FLn7Vgo8r84vFGTsYKpUt7JvpoqkypU1nEsN4akv/mpGXCQOFqN9EWKggq3uniDXiZ5Wg35ls4AqmPSY5A/XMf84WKNR6xcBt+rUz/Z8lRn7mEBphhyEmHr0tDs3sY23hGnU9kKmV9rjcqh4rgQ+N7SnY+koJYHd32pOTdmrcXYRuWv4szk86wWp+GTj1wZ2bLe5j44idfWSuQkPw==";
            //enStr = "NR4tG5D/sRriuNoonT5AqRMNetmpcnndgQ8Rr8c3AJbkBwSSejlYl7Xraqi4UDiYZfAxLMf2sPpwT9IJmTGBO xRpaHN0BumgRE1w 5quB7wae6zagIE5DC M8dkC kuBSPD8FLn7Vgo8r84vFGTsYKpUt7JvpoqkypU1nEsN4akv/mpGXCQOFqN9EWKggq3uniDXiZ5Wg35ls4AqmPSY5A/XMf84WKNR6xcBt rUz/Z8lRn7mEBphhyEmHr0tDs3sY23hGnU9kKmV9rjcqh4rgQ N7SnY koJYHd32pOTdmrcXYRuWv4szk86wWp GTj1wZ2bLe5j44idfWSuQkPw==";
            string enStr2 = test.Encrypt(str);
            string enStr3 = "t3N885z0IL9aASYjcJppftNoF5eoDfVyuJsxcQSRc3C+/otctMs8azKgPo12zHERr/3AOLoVXNk4GQX2o36ksqhf2BbrDFphVqEBbC/sh175XLXVSKcXP2Tb4EoE1B4QtzoRymV1nlTGERDrzeJ5vZUTEJStVpgNpp2UoDp3CIT4ASoIGhbnVieRLtDS/EukdYz2GKRAoygf9UBlzZkooit6Grb7JeF0zxcfY4X5EO4oehylvNDYTJcP9yfPF9NybW05DMpoNMNYHgDamYbKRwcv9SWIhZ6kY59NO4FO4Gv6/PYsNaLE6hMY4LE+0ITL0X8QAFhrG3j3fCWOw076mQ==";
            Console.WriteLine("A生成的加密字符串a：" + enStr);
            Console.WriteLine("B生成的加密字符串b：" + enStr2);
            Console.WriteLine("JS生成的加密字符串js：" + enStr3);
            //解密




            string deStr = rsa.Decrypt(enStr);
            string deStr2 = rsa.Decrypt(enStr2);
            string deStr3 = rsa.Decrypt(enStr3);
            Console.WriteLine("A解密字符串a：" + deStr);
            Console.WriteLine("A解密字符串b：" + deStr2);
            Console.WriteLine("A解密字符js：" + deStr3);


            string deStr4 = test.Decrypt(enStr);
            string deStr5 = test.Decrypt(enStr2);
            string deStr6 = test.Decrypt(enStr3);
            Console.WriteLine("B解密字符串a：" + deStr4);
            Console.WriteLine("B解密字符串b：" + deStr5);
            Console.WriteLine("B解密字符js：" + deStr6);

            Console.ReadKey();
        }


        private void UploadDataCompleted(object sender, UploadDataCompletedEventArgs e)
        {
            Console.WriteLine(Encoding.GetEncoding("GB2312").GetString(e.Result));
        }
    }
}
