using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    public class PersonelDetayVMSP
    {
        private readonly string _connectionString;

        public PersonelDetayVMSP(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<PersonelDetayVM> PersonelListe()
        {
            List<PersonelDetayVM> liste = new();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("sp_PersonelDetay_Getir", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    liste.Add(new PersonelDetayVM
                    {
                        PersonelId = (int)dr["PersonelId"],
                        PersonelAd = dr["PersonelAd"].ToString(),
                        PersonelSoyad = dr["PersonelSoyad"].ToString(),
                        RoleAd = dr["RoleAd"].ToString(),
                        DepartmanAd = dr["DepartmanAd"].ToString()
                    });
                }
            }

            return liste;
        }
    }
}
