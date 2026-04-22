using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace isKatmani
{
    public static class UtilConnection
    {
        public static SqlConnection conn = new SqlConnection(@"Server=HUAWEI\SQLEXPRESS01;Database=obs;Trusted_Connection=True;TrustServerCertificate=True;");


        public static SqlConnection getCon()
        {
            // connection kapali ise aç
            if (UtilConnection.conn.State == ConnectionState.Closed)
                conn.Open();
            return conn;
        }

    }
}
