using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlConnector
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DB db = new DB("server", "username", "password", "dbName");

            IDB d = new LogReg(dB);

            var a = d.GetDataTable<Users>("SELECT * FROM users").Result;

            Console.ReadKey();
        }
    }
}
