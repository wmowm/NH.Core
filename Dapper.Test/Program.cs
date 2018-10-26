using System;
using static Dapper.Test.DapperHelper;

namespace Dapper.Test
{
    class Program
    {
        private static string connString = "Data Source=193.112.104.103;Initial Catalog=666;port=3339; User ID=root;Password=123456;SslMode = none;";
        static void Main(string[] args)
        {
            DapperHelper DH = new DapperHelper(connString, Providers.MySql);
            var res = DH.Query("select * from test");
            Console.WriteLine("Hello World!");
        }
    }
}
