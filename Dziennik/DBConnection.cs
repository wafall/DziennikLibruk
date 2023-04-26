using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dziennik
{
    public class DBConnection
    {
        public static MySqlConnection Connection;

        static DBConnection()
        {
            string connectionString = "server=localhost;user id=root;database=dziennik";
            Connection = new MySqlConnection(connectionString);
            Connection.Open();
        }
    }
}
