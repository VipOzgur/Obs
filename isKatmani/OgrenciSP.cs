using isKatmani;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace isKatmani
{
    public class OgrenciSP
    {
        private ArrayList _liste;
        string _listele = "sp_Ogrenci_Listele", _ekle = "sp_Ogrenci_Ekle";
        string _sil = "sp_Ogrenci_Sil", _guncelle = "sp_Ogrenci_Guncelle";

        public OgrenciSP()
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
                _liste.Add(new Ogrenci(
                    Convert.ToInt32(reader["Id"]),
                    Convert.ToInt32(reader["RoleId"]),
                    reader["Ad"].ToString(),
                    reader["Soyad"].ToString(),
                    reader["Sifre"].ToString(),
                    Convert.ToDateTime(reader["KayitTarihi"]),
                    reader["MezunTarihi"] == DBNull.Value
                        ? (DateTime?)null
                        : Convert.ToDateTime(reader["MezunTarihi"]),
                    reader["Adres"].ToString()
                ));
            }
            conn.Close();
        }

        // 🔹 EKLE
        public void ekle(Ogrenci o)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_ekle, conn);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Ad", o.ad);
            com.Parameters.AddWithValue("@Soyad", o.soyad);
            com.Parameters.AddWithValue("@Sifre", o.sifre);
            com.Parameters.AddWithValue("@RoleId", o.roleId);
            com.Parameters.AddWithValue("@Adres", o.adres);

            com.ExecuteNonQuery();
            conn.Close();
        }

        // 🔹 GÜNCELLE
        public void guncelle(Ogrenci o)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_guncelle, conn);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Id", o.Id);
            com.Parameters.AddWithValue("@Ad", o.ad);
            com.Parameters.AddWithValue("@Soyad", o.soyad);
            com.Parameters.AddWithValue("@Sifre", o.sifre);
            com.Parameters.AddWithValue("@RoleId", o.roleId);
            com.Parameters.AddWithValue("@KayitTarihi", o.kayitTarihi);
            com.Parameters.AddWithValue("@MezunTarihi", (object)o.mezunTarihi ?? DBNull.Value);
            com.Parameters.AddWithValue("@Adres", o.adres);

            com.ExecuteNonQuery();
            conn.Close();
        }

        // 🔹 SİL
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