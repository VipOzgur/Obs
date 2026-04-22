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
    public class RolesSP
    {
        //normal array list
        private ArrayList _liste;

        private string _tabloAdi;
        private string _procedureListele = "";//"tum_kayitlar";
        private string _procedureEkle = "";//"personel_ekle";
        private string _procedureSil = "";//"personel_sil";


        public ArrayList Liste()
        {
            return _liste;
        }


        public RolesSP()
        {
            this._liste = new ArrayList();
            this.set_default_variable();
            this.doldur();

        }

        private void set_default_variable()
        {
            _procedureListele = "sp_Role_Listele";
            _procedureEkle = "sp_Role_Ekle";
            _procedureSil = "sp_Role_Sil";
        }

        public ArrayList listArray()
        {
            return this._liste;
        }


        public void doldur()
        {

            SqlCommand com = null;
            //1- sql cümlemiz.
            //string sql = "select * from " + this._tabloAdi;

            try
            {
                //2- connection
                SqlConnection conn = UtilConnection.getCon();

                //3- connection ve command kullanarak DB bağlantısı yaptık.
                com = new SqlCommand(this._procedureListele, conn);
                com.CommandType = CommandType.StoredProcedure;

                // 3.1- bu kısımda classları bir arraya dolduruyoruz.
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    // 4- verileri okuduk
                    int id = Convert.ToInt32(reader["Id"]);
                    string ad = reader["Ad"].ToString();
                    Roles s= new Roles(id,ad);

                    //4.2- ilgili classı  listeye ekledik.
                    this._liste.Add(s);
                }
                //5- bağlantı vb. kulandığımız kaynakları kapattık.
                reader.Close();
                conn.Close();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int veri_ile_ekle(string ad)
        {
            try
            {
                Roles r = new Roles(0,ad);
                Roles objReturn = (Roles)this.ekle(r);
                return objReturn.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public BaseClass ekle(Roles obj)
        {
            SqlCommand com = null;

            try
            {
                //2- connection
                SqlConnection conn = UtilConnection.getCon();

                //3- connection ve command kullanarak DB bağlantısı yaptık.
                com = new SqlCommand(_procedureEkle, conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@ad", obj.ad);

                // 4- DB  ekleme işlemini yaptık.
                //com.ExecuteScalar() ile değer döndürdü.
                //id değerini aldık

                int id = Convert.ToInt32(com.ExecuteScalar());
                obj.Id = id;
                //4.1- ilgili classı  listeye ekledik.
                this._liste.Add(obj);

                //5- bağlantı vb. kulandığımız kaynakları kapattık.
                conn.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;

        }

        public BaseClass bul_id_ile(int id)
        {
            Roles findObj = null;
            foreach (Roles currObj in this._liste)
            {
                if (currObj.Id == id)
                {
                    findObj = currObj;
                    break;
                }

            }
            return findObj;
        }
        public void sil_id_ile(int id)
        {

            try
            {
                Roles p = (Roles)this.bul_id_ile(id);
                this.sil(p);

            }
            catch (Exception ex)
            {
                throw ex;
            }




        }
        public void sil(Roles obj)
        {
            SqlCommand com = null;
            //1- sql cümlemiz.
            //string sql = "delete personel where id=@id";
            //string procedureName = "personel_sil";

            try
            {
                //2- connection
                SqlConnection conn = UtilConnection.getCon();

                //3- connection ve command kullanarak DB bağlantısı yaptık.
                com = new SqlCommand(this._procedureSil, conn);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@Id", obj.Id);



                // 4- çıktı vermeden DB işlemini yap. Yani sildi.
                com.ExecuteNonQuery();


                //4.1- ilgili classı listemizden sildik. 
                this._liste.Remove(obj);

                //5- bağlantı vb. kulandığımız kaynakları kapattık.
                conn.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
