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
    public class DerslerSP
    {
        private ArrayList _liste;
        string _listele = "sp_Ders_Listele", _ekle = "sp_Ders_Ekle";
        string _sil = "sp_Ders_Sil", _guncelle = "sp_Ders_Guncelle";

        public DerslerSP()
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
                _liste.Add(new Dersler(
                    Convert.ToInt32(reader["Id"]),
                    reader["Ad"].ToString(),
                    Convert.ToInt32(reader["Akts"]),
                    Convert.ToInt32(reader["Donem"]),
                    Convert.ToInt32(reader["PersonelId"]),
                    Convert.ToInt32(reader["SinifId"])
                ));
            }
            conn.Close();
        }

        public void ekle(Dersler d)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_ekle, conn);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Ad", d.ad);
            com.Parameters.AddWithValue("@Akts", d.akts);
            com.Parameters.AddWithValue("@Donem", d.donem);
            com.Parameters.AddWithValue("@PersonelId", d.personelId);
            com.Parameters.AddWithValue("@SinifId", d.sinifId);

            com.ExecuteNonQuery();
            conn.Close();
        }

        public void guncelle(Dersler d)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_guncelle, conn);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Id", d.Id);
            com.Parameters.AddWithValue("@Ad", d.ad);
            com.Parameters.AddWithValue("@Akts", d.akts);
            com.Parameters.AddWithValue("@Donem", d.donem);
            com.Parameters.AddWithValue("@PersonelId", d.personelId);
            com.Parameters.AddWithValue("@SinifId", d.sinifId);

            com.ExecuteNonQuery();
            conn.Close();
        }
        public ArrayList Liste()
        {
            return _liste;
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
