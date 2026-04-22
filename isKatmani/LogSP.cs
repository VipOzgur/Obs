using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    public class LogSP
    {
        private ArrayList _liste;
        string _listele = "sp_Log_Listele", _ekle = "sp_Log_Ekle";
        string _sil = "sp_Log_Sil", _guncelle = "sp_Log_Guncelle";

        public LogSP()
        {
            _liste = new ArrayList();
            doldur();
        }

        public void doldur()
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_listele, conn);
            com.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                _liste.Add(new Log(
                    Convert.ToInt32(reader["Id"]),
                    reader["ParentId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ParentId"]),
                    reader["ChildId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ChildId"]),
                    reader["Data"].ToString()
                ));
            }
            conn.Close();
        }

        public void ekle(Log l)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_ekle, conn);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@ParentId", (object)l.parentId ?? DBNull.Value);
            com.Parameters.AddWithValue("@ChildId", (object)l.childId ?? DBNull.Value);
            com.Parameters.AddWithValue("@Data", l.data);

            com.ExecuteNonQuery();
            conn.Close();
        }

        public void guncelle(Log l)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_guncelle, conn);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Id", l.Id);
            com.Parameters.AddWithValue("@ParentId", (object)l.parentId ?? DBNull.Value);
            com.Parameters.AddWithValue("@ChildId", (object)l.childId ?? DBNull.Value);
            com.Parameters.AddWithValue("@Data", l.data);

            com.ExecuteNonQuery();
            conn.Close();
        }

        public void sil(int id)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_sil, conn);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Id", id);
            com.ExecuteNonQuery();
            conn.Close();
        }
    }
}
