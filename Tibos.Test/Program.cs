using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tibos.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Read(@"F:\GitProduct\NH.Core\Tibos.Test\bin\Debug\netcoreapp2.0\EnglishWord4.txt");
            Console.Read();
        }



        static void Baicizhan()
        {
            byte[] byData = new byte[1000];
            char[] charData = new char[1000];
            FileStream file = new FileStream(@"F:\GitProduct\NH.Core\Tibos.Test\bin\Debug\netcoreapp2.0\EnglishWord4.txt", FileMode.Open);
            file.Seek(0, SeekOrigin.Begin);
            file.Read(byData, 0, 1000); //byData传进来的字节数组,用以接受FileStream对象中的数据,第2个参数是字节数组中开始写入数据的位置,它通常是0,表示从数组的开端文件中向数组写数据,最后一个参数规定从文件读多少字符.
            Decoder d = Encoding.Default.GetDecoder();
            d.GetChars(byData, 0, byData.Length, charData, 0);
            file.Close();
            //1.先让文件流执行完毕

            //2.char[] 转string

            string txt = new string(charData);

            //3.string 转 list

            //foreach (var item in charData)
            //{
            //    //这里开始做判断
            //    //去掉空行
            //    if (string.IsNullOrEmpty(item.ToString())) continue;
            //    Console.WriteLine(item);
            //}



        }



        public static void Read(string path)
        {
            List<string> list = new List<string>();
            StreamReader sr = new StreamReader(path, Encoding.UTF8); //这个根据文件的编码格式,防止乱码
            String line;
            bool isA = false;
            while ((line = sr.ReadLine()) != null)
            {
                //1.去掉空行
                if (string.IsNullOrEmpty(line)) continue;
                //2.必须是A后面的行
                if (line.Contains("A")) isA = true;
                //3.每行必须包含. 长度大于10个字符
                if (isA && line.Contains(".") && line.Length > 10)
                {
                    list.Add(line);
                }
            }
        }
    }
}
