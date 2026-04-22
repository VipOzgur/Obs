using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    public class Ogrenci : BaseClass
    {
        private int _roleId;
        private string _ad;
        private string _soyad;
        private string _sifre;
        private DateTime _kayitTarihi;
        private DateTime? _mezunTarihi;
        private string _adres;

        public Ogrenci(int id, int roleId, string ad, string soyad, string sifre,
            DateTime kayitTarihi, DateTime? mezunTarihi, string adres) : base(id)
        {
            _roleId = roleId;
            _ad = ad;
            _soyad = soyad;
            _sifre = sifre;
            _kayitTarihi = kayitTarihi;
            _mezunTarihi = mezunTarihi;
            _adres = adres;
        }

        public int roleId { get { return _roleId; } set { _roleId = value; } }
        public string ad { get { return _ad; } set { _ad = value; } }
        public string soyad { get { return _soyad; } set { _soyad = value; } }
        public string sifre { get { return _sifre; } set { _sifre = value; } }
        public DateTime kayitTarihi { get { return _kayitTarihi; } set { _kayitTarihi = value; } }
        public DateTime? mezunTarihi { get { return _mezunTarihi; } set { _mezunTarihi = value; } }
        public string adres { get { return _adres; } set { _adres = value; } }
    }
}