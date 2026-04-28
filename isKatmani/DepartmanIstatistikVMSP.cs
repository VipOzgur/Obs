using isKatmani.EntityLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    public class DepartmanIstatistikVMSP
    {
        private readonly string _connectionString;

        public DepartmanIstatistikVMSP(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<DepartmanIstatistikVM> DepartmanIstatistik()
        {
            List<DepartmanIstatistikVM> liste = new();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_DepartmanIstatistik_Getir", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    liste.Add(new DepartmanIstatistikVM
                    {
                        FakulteAd = dr["FakulteAd"].ToString(),
                        DepartmanAd = dr["DepartmanAd"].ToString(),
                        PersonelSayisi = (int)dr["PersonelSayisi"],
                        DersSayisi = (int)dr["DersSayisi"],
                        ToplamAkts = (int)dr["ToplamAkts"]
                    });
                }
            }

            return liste;
        }
    }
}
