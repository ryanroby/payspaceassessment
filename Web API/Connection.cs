using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API
{
    public class Connections
    {
        public static SqlConnection LocalConnection()
        {
            return new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True");
        }
    }
}
