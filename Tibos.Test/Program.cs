using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            decimal d = decimal.Round(decimal.Parse("0.550580"), 4);
            decimal k = decimal.Parse(d.ToString("#0.######"));
            Growing.Start();




            //string sign = "DXoBFvxuFJaHgEqLIYjTnPALQ36O96tYd7Br/2ucb8bq+sjQUUhnps1udLy5mzMsm00SQqRjVngCC2/yVm1C/Q/B/1aYTRRwH1XSGfiVaBqTz/+MSBwQrzw1Y4gTXmkZoVXQqhrBGuKEQWd+AJsPUngXETE1TzEePLUOnIU2KnsKANIXvrax/auJbHKUpdcW1Dg/7uM2DDV85qc9UposKxjWfYozHVBWPSflslrn6tXXDuVUrcD0gaIrAznu8R8sMoo3DtsebIW1ooIrUXRDdZ/SijRhsGR3/Yx137EWPGVrUwhif+SQkkZyT15xcwkilpOpvhrtI/crJGEPURVe5A==";
            //var res = HttpCommon.HttpPost("http://preapi.bi-xing.com:8303/api/ExternalAPI/RegisterUser", "json=M0gZC6V7d9T1WCQcBZaiBxH11g7ngogtG8u5tE7HHDcTVrGYajN7KYUpRHTeHv//+3LHuMjrdcQHbab0t86scH1gpJPx/nP2RBz/a+5CzCm516rlF2C+9Yj1Mj2F8Zgya4jXTOjV5bmYHekjXR8DPDCHNJwAfO2qPjlSPTutXYKvTO+ufXUK9WRhHPGkfYilXC31JIERFI57kNVDMIaXcn/M72ID4pSL2SxdCviXCixP4TL4G7xNpYlEI8Wg3Gg97rAT0ZGvWUJYZX+gAMVrSD4MU+xz/JZSFvQ2XBUn40qZRDBwZJF6O1XD4OlmCnGrFvzqnTK+j6BqZvIHSAhD6A==", sign: sign);
            //var aa = "btc/vhkd";
            //var cc = aa.Substring(0, aa.LastIndexOf("/"));
            //Console.WriteLine(cc);
            //Test();
            Console.ReadLine();
        }


        public static void Test()
        {

            //2048 公钥
            string publicKey =
            //    "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAyYxuXiE4XfjXpxz2OJVdorMiDNBJn6VV37IPo30FRvHMjTsVazi45wo75H8tYfa5rya5au4WVTt+EPr4is8qXIe+8NA90OAwDvbNhuxuLlnCyIKIp9Nqn8uLF+9ryHEdVOpuILOysuOvcpwxsqudn1yQk27QbqULLo+9dMGXN+JBd7PtnSDd/cde3WZE1yQSM3N0zwwF6HjAHVjEzz3nD3BKbF14UI3w0+2pYiktpeBUXKYOSNpMyF/KAVgxelRJccXdxFhnHKC6gUd8MrcIDaglj/L4wW06vW1JwCsai5wHsN5g99v30+EhoC6TTlqse1tqlvStz6W7eepMYeFuuQIDAQAB";
            "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAyYxuXiE4XfjXpxz2OJVdorMiDNBJn6VV37IPo30FRvHMjTsVazi45wo75H8tYfa5rya5au4WVTt+EPr4is8qXIe+8NA90OAwDvbNhuxuLlnCyIKIp9Nqn8uLF+9ryHEdVOpuILOysuOvcpwxsqudn1yQk27QbqULLo+9dMGXN+JBd7PtnSDd/cde3WZE1yQSM3N0zwwF6HjAHVjEzz3nD3BKbF14UI3w0+2pYiktpeBUXKYOSNpMyF/KAVgxelRJccXdxFhnHKC6gUd8MrcIDaglj/L4wW06vW1JwCsai5wHsN5g99v30+EhoC6TTlqse1tqlvStz6W7eepMYeFuuQIDAQAB";
            //2048 私钥
            string privateKey = 
                "MIIEowIBAAKCAQEAyYxuXiE4XfjXpxz2OJVdorMiDNBJn6VV37IPo30FRvHMjTsVazi45wo75H8tYfa5rya5au4WVTt+EPr4is8qXIe+8NA90OAwDvbNhuxuLlnCyIKIp9Nqn8uLF+9ryHEdVOpuILOysuOvcpwxsqudn1yQk27QbqULLo+9dMGXN+JBd7PtnSDd/cde3WZE1yQSM3N0zwwF6HjAHVjEzz3nD3BKbF14UI3w0+2pYiktpeBUXKYOSNpMyF/KAVgxelRJccXdxFhnHKC6gUd8MrcIDaglj/L4wW06vW1JwCsai5wHsN5g99v30+EhoC6TTlqse1tqlvStz6W7eepMYeFuuQIDAQABAoIBAB/2RjZlPEQix0g4Ho/gmAXKNJclnNdVZ+Xenf5GQET86XxoJ0BmsWPqSo803+SONOoi0Pq1IROJKLfWpP2WVthvqocT7wml09k7qGJCMkLldy9HDBbZPypyI++6xzP1RDqIJsjG1orp8pPRU0xLTObNzhujUiiJwdkzcE8IuEtjLQL554wgD+EgRtWowYFIHjsb/2dZRFXF/aAR26dcnvD/lh6TXeMV5Y/kI/wpCDB+3+KFSauDEgjNEgBJm3o5k1fYDk6y5SkR9p6DlVZSMe29D73JzYiP65sSy9hDun+D3O0EQCTpjBTFS0CX/uiT4gdfDviW9hyX2CzrdQyyKlUCgYEA5sEALc5deFvAYFMkmcXVqQzMuR3iankFO/SRJ8rLrTYRX9uuuQTeUrHoqcidSKACY2SnYJYJSMHOT+81fxIkjaQOxCyLZqQ2mkkmznPfdXESMZZw/c2+JTOLvfbeWCbw2NsRupYzv6R7doCVyEtRzvylICS4V7Nb6T0+2NrXt0cCgYEA35lxAiQMetxtBVcOhzonxnOFo8pef/Hxf7lbgq4tpKNXAap8JEzZWlnXxvanpW9Ql/0D+D1grjYID1s6bFuZJPjDF433t5XScyr+a1cN9DSEFUGRRNBspciViKgU8vxxI6FtcYRroABEV0EdZi3ZoXw42z3YXNJFOIPEQaTcqf8CgYBBpgT8Ayr6c/f+L4lJKkyIBLk1rTfeHMINdZ3aWUqq46f7wo8p2iW6Fx1fOFDu+X7Uci1gQC8yANlGSoLIvQujNFpzG00pjWxvZoJg1/xp+Bo5+1f4DRGcmdaW2+YT5Dg/KcwYbmUYj54Eb76tNih3H8+GnovHf6KVb7Qyk8gtPwKBgCM+clueBwGOoPiIgle6SO604smwajjMj/5L3yq3/aXHwT0X/D1Uh6YAJ0zLMB2ZcCCk1u1X71dGWPrirFDMF1WGiGm8DOG5EdTpX/TXYfGuHWukTBuGprly5m8F31d1hvfQyAluj/BGWn9OWi3y6CzWAbg9BU+MLTD9Q//81nNNAoGBAKw4arEFyRMFnC4l6H838rO6hxxCvdU88QOtcGjyauVAwHA4WuQjLVrXgUcu4zbq1vR+X6ffUFJEGE2CrCutb5ItsaWnT3BdeIkIl2PV33ekzBYv43QMlc1AB4zv2AJRInC+s75t2iDMnh8l/347Xx2US6uFIuGWw9EOjubq6Ohu";


                var rsa = new RSAHelper(RSAType.RSA, Encoding.UTF8, privateKey, publicKey);

            var test = new RSACrypto(privateKey, publicKey);


            string str = "vXM8ukepokQnTy5pMRGXBQ";

            Console.WriteLine("原始字符串：" + str);

            //加密
            string enStr = rsa.Encrypt(str);
            Console.WriteLine("A生成的加密字符串a：" + enStr);
            //解密



            enStr =
            "k0RXI4OgYuIQ7yQH1CO+Unql5y1XYqUe4ywsoobgvcsoJep4y5y+/Ozz9Az3Ft0NL/LVjdUtFreSN91gr32rCJaoHANUCQ0BnhW9op46O6CEaryW11Dc9u20H8G/3F6PhLgc0+U/bh5ew6G2Ez0V9XDVx0SBMby5ihpg4FCivdQ6CxklGxOVKAZKKAOpciWLjdlMsP7/ylKJuM2xDj3UcIeV4Ie+ZFFeR5DXsEna2R+ywKIdAyrj0PgMKU5cA1/0iPAmiKeLp7XB9lqyo6qI7mZaEhL1gfOGOO68vXAFeWuJaM7scHL51UDkEbEoplqF1d/SonfRCMCtwdT8vYSLqQ==";
                string deStr = rsa.Decrypt(enStr);
            string deStr2 = test.Decrypt(enStr);
            Console.WriteLine("B解密字符串a：" + deStr);
            Console.WriteLine("B解密字符串a：" + deStr2);

            Console.ReadKey();
        }


        private void UploadDataCompleted(object sender, UploadDataCompletedEventArgs e)
        {
            Console.WriteLine(Encoding.GetEncoding("GB2312").GetString(e.Result));
        }
    }
}
