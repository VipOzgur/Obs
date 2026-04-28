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
    public class DepartmansSP
    {
        private ArrayList _liste;
        string _procListele = "sp_Departman_Listele", _procEkle = "sp_Departman_Ekle";
        string _procSil = "sp_Departman_Sil", _procGuncelle = "sp_Departman_Guncelle";

        public DepartmansSP() { this._liste = new ArrayList(); this.doldur(); }
        public ArrayList Liste()
        {
            return _liste;
        }
        public void ekle(Departmans d)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_procEkle, conn);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Ad", d.ad);
            com.Parameters.AddWithValue("@FakulteId", d.fakulteId);

            com.ExecuteNonQuery();
            conn.Close();
        }

        public void doldur()
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_procListele, conn);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                this._liste.Add(new Departmans(Convert.ToInt32(reader["Id"]), reader["Ad"].ToString(), Convert.ToInt32(reader["FakulteId"])));
            }
            conn.Close();
        }

        public void guncelle(Departmans obj)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_procGuncelle, conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", obj.Id);
            com.Parameters.AddWithValue("@Ad", obj.ad);
            com.Parameters.AddWithValue("@FakulteId", obj.fakulteId);
            com.ExecuteNonQuery();
            conn.Close();
        }

        public void sil(int id)
        {
            SqlConnection conn = UtilConnection.getCon();
            SqlCommand com = new SqlCommand(_procSil, conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", id);
            com.ExecuteNonQuery();
            conn.Close();
        }
    }
}

