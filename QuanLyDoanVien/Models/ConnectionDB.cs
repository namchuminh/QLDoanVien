using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoanVien.Models
{
    internal class ConnectionDB
    {
        SqlConnection conn;
        String connStr;
        public ConnectionDB()
        {
            connStr = @"Data Source=CHUMINHNAM; Initial Catalog=QLDoanVien; Integrated Security=SSPI";
        }

        public SqlConnection ConnectDB()
        {
            conn = new SqlConnection(connStr);
            return conn;
        }
    }
}
