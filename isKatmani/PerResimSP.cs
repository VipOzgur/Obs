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
    public class PerResimSP
    {
        private ArrayList _liste;
        string _listele = "sp_PerResim_Listele", _ekle = "sp_PerResim_Ekle";
        string _sil = "sp_PerResim_Sil", _guncelle = "sp_PerResim_Guncelle";

        public PerResimSP()
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
                _liste.Add(new PerResim(
                    Convert.ToInt32(reader["Id"]),
                    Convert.ToInt32(reader["PerId"]),
                    reader["Data"].ToString(),
                    reader["Path"].ToString()
                ));
            }
            conn.Close();
        }

        public void ekle(PerResim r)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_ekle, conn);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@PerId", r.perId);
            com.Parameters.AddWithValue("@Data", r.data);
            com.Parameters.AddWithValue("@Path", r.path);

            com.ExecuteNonQuery();
            conn.Close();
        }

        public void guncelle(PerResim r)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_guncelle, conn);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Id", r.Id);
            com.Parameters.AddWithValue("@PerId", r.perId);
            com.Parameters.AddWithValue("@Data", r.data);
            com.Parameters.AddWithValue("@Path", r.path);

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
