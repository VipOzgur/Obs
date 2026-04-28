using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    public class OgrenciDersNotlariVMSP
    {
        private readonly string _connectionString;

        public OgrenciDersNotlariVMSP(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<OgrenciDersNotlariVM> OgrenciNotlariniGetir(int? ogrenciId)
        {
            List<OgrenciDersNotlariVM> liste = new List<OgrenciDersNotlariVM>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("sp_OgrenciDersNotlari_Getir", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (ogrenciId.HasValue)
                        cmd.Parameters.AddWithValue("@OgrenciId", ogrenciId.Value);
                    else
                        cmd.Parameters.AddWithValue("@OgrenciId", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            liste.Add(new OgrenciDersNotlariVM
                            {
                                OgrenciId = Convert.ToInt32(dr["OgrenciId"]),
                                OgrenciAd = dr["OgrenciAd"].ToString(),
                                OgrenciSoyad = dr["OgrenciSoyad"].ToString(),
                                DersId = Convert.ToInt32(dr["DersId"]),
                                DersAd = dr["DersAd"].ToString(),
                                Puan = Convert.ToDecimal(dr["Puan"]),
                                HarfNotu = dr["HarfNotu"].ToString(),
                                DurumAd = dr["DurumAd"].ToString()
                            });
                        }
                    }
                }
            }

            return liste;
        }
    }
}
