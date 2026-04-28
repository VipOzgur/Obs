using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    public class SinifSP
    {
        private ArrayList _liste;
        string _listele = "sp_Sinif_Listele", _ekle = "sp_Sinif_Ekle";
        string _sil = "sp_Sinif_Sil", _guncelle = "sp_Sinif_Guncelle";

        public SinifSP()
        {
            _liste = new ArrayList();
            doldur();
        }
        public ArrayList Liste()
        {
            return _liste;
        }

        public void doldur()
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_listele, conn);
            com.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                _liste.Add(new Siniflar(
                    Convert.ToInt32(reader["Id"]),
                    reader["Ad"].ToString(),
                    reader["No"].ToString(),
                    Convert.ToInt32(reader["FakulteId"])
                ));
            }
            conn.Close();
        }

        public void ekle(Siniflar s)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_ekle, conn);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Ad", s.ad);
            com.Parameters.AddWithValue("@No", s.no);
            com.Parameters.AddWithValue("@FakulteId", s.fakulteId);

            com.ExecuteNonQuery();
            conn.Close();
        }

        public void guncelle(Siniflar s)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_guncelle, conn);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Id", s.Id);
            com.Parameters.AddWithValue("@Ad", s.ad);
            com.Parameters.AddWithValue("@FakulteId", s.fakulteId);
            com.Parameters.AddWithValue("@No", s.no);

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
