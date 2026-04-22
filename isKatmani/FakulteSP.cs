using isKatmani;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

public class FakulteSP
{
    private ArrayList _liste;
    string _listele = "sp_Fak_Listele", _ekle = "sp_Fak_Ekle";
    string _sil = "sp_Fak_Sil", _guncelle = "sp_Fak_Guncelle";

    public FakulteSP()
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
            _liste.Add(new Fakulteler(
                Convert.ToInt32(reader["Id"]),
                reader["Ad"].ToString(),
                reader["Adres"].ToString(),
                DateOnly.FromDateTime(Convert.ToDateTime(reader["KurulusTarihi"]))
            ));
        }
        conn.Close();
    }

    public void ekle(Fakulteler f)
    {
        SqlConnection conn = UtilConnection.getCon();
        SqlCommand com = new SqlCommand(_ekle, conn);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.AddWithValue("@Ad", f.ad);
        com.Parameters.AddWithValue("@KurulusTarihi", f.kurulusTarihi);
        com.Parameters.AddWithValue("@Adres", f.adres);

        com.ExecuteNonQuery();
        conn.Close();
    }

    public void guncelle(Fakulteler f)
    {
        SqlConnection conn = UtilConnection.getCon();
        SqlCommand com = new SqlCommand(_guncelle, conn);
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.AddWithValue("@Id", f.Id);
        com.Parameters.AddWithValue("@Ad", f.ad);
        com.Parameters.AddWithValue("@KurulusTarihi", f.kurulusTarihi);
        com.Parameters.AddWithValue("@Adres", f.adres);

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