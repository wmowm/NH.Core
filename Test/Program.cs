using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using Tibos.Test;

namespace Test
{

    class Model
    {

    }

    class Program
    {

        static void Main(string[] args)
        {
            string SavePath = @"F:\ExcelTemp\whitelist_sample.xlsx";
            Console.WriteLine("ETH地址             此日期前不可购买             此日期前不可售出             KYC/ AML有效期");

            DataTable dt = OfficeHelper.ReadExcelToDataTable(SavePath);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Console.WriteLine($"第{i}行数据:");
                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    Console.Write(dt.Rows[i][j].ToString() + "             ");

                }
                Console.WriteLine("");
            }
            Console.Read();
        }
    }
}
