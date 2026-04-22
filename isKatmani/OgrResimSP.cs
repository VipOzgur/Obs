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
    public class OgrResimSP
    {
        private ArrayList _liste;
        string _listele = "sp_OgrResim_Listele", _ekle = "sp_OgrResim_Ekle";
        string _sil = "sp_OgrResim_Sil", _guncelle = "sp_OgrResim_Guncelle";

        public OgrResimSP()
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
                _liste.Add(new OgrResim(
                    Convert.ToInt32(reader["Id"]),
                    Convert.ToInt32(reader["OgrId"]),
                    reader["Data"].ToString(),
                    reader["Path"].ToString()
                ));
            }
            conn.Close();
        }

        public void ekle(OgrResim r)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_ekle, conn);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@OgrId", r.ogrId);
            com.Parameters.AddWithValue("@Data", r.data);
            com.Parameters.AddWithValue("@Path", r.path);

            com.ExecuteNonQuery();
            conn.Close();
        }

        public void guncelle(OgrResim r)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_guncelle, conn);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Id", r.Id);
            com.Parameters.AddWithValue("@OgrId", r.ogrId);
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
