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
    public class PersonelSP
    {
        private ArrayList _liste;
        string _listele = "sp_Per_Listele", _ekle = "sp_Per_Ekle";
        string _sil = "sp_Per_Sil", _guncelle = "sp_Per_Guncelle";

        public PersonelSP()
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
                _liste.Add(new Personel(
                    Convert.ToInt32(reader["Id"]),
                    reader["Ad"].ToString(),
                    reader["Soyad"].ToString(),
                    reader["Adres"].ToString(),
                    Convert.ToInt32(reader["RoleId"]),
                    Convert.ToInt32(reader["DepartmanId"])
                ));
            }
            conn.Close();
        }

        public void ekle(Personel p)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_ekle, conn);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Ad", p.ad);
            com.Parameters.AddWithValue("@Soyad", p.soyad);
            com.Parameters.AddWithValue("@RoleId", p.roleId);
            com.Parameters.AddWithValue("@DepartmanId", p.departmanId);
            com.Parameters.AddWithValue("@Adres", p.adres);

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

        public void guncelle(Personel p)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_guncelle, conn);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Id", p.Id);
            com.Parameters.AddWithValue("@Ad", p.ad);
            com.Parameters.AddWithValue("@Soyad", p.soyad);
            com.Parameters.AddWithValue("@RoleId", p.roleId);
            com.Parameters.AddWithValue("@DepartmanId", p.departmanId);
            com.Parameters.AddWithValue("@Adres", p.adres);

            com.ExecuteNonQuery();
            conn.Close();
        }
    }
}
