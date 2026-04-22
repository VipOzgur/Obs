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
    public class OgrDersSP
    {
        private ArrayList _liste;
        string _listele = "sp_OgrDers_Listele", _ekle = "sp_OgrDers_Ekle";
        string _sil = "sp_OgrDers_Sil", _guncelle = "sp_OgrDers_Guncelle";

        public OgrDersSP()
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
                _liste.Add(new Ogr_Ders(
                    Convert.ToInt32(reader["Id"]),
                    Convert.ToInt32(reader["OgrId"]),
                    Convert.ToInt32(reader["DersId"]),
                    Convert.ToBoolean(reader["Basari"]),
                    Convert.ToInt32(reader["Puan"])
                ));
            }
            conn.Close();
        }

        public void ekle(Ogr_Ders od)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_ekle, conn);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@OgrId", od.ogrId);
            com.Parameters.AddWithValue("@DersId", od.dersId);
            com.Parameters.AddWithValue("@Basari", od.basari);
            com.Parameters.AddWithValue("@Puan", od.puan);

            com.ExecuteNonQuery();
            conn.Close();
        }
        public void guncelle(Ogr_Ders od)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_guncelle, conn);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@OgrId", od.ogrId);
            com.Parameters.AddWithValue("@DersId", od.dersId);
            com.Parameters.AddWithValue("@Basari", od.basari);
            com.Parameters.AddWithValue("@Puan", od.puan);

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
