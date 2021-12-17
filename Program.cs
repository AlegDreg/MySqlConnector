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
            DB d = new DB("server", "username", "password", "dbName");

            IDB dB = new LogReg(d);

            if (dB.DoRequest($"UPDATE {Tables.Users} SET {UsersFiels.Login} = \"admin\" WHERE {UsersFiels.Id} = \"1\" "))
            {
                var t = dB.GetDataTable($"SELECT * FROM {Tables.Users} WHERE {UsersFiels.Id} = \"1\"").Result;
            }
        }
    }
}
