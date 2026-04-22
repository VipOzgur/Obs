using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    public class Personel : BaseClass
    {
        private string _ad;
        private string _soyad;
        private string _adres;
        private int _roleId;
        private int _departmanId;

        public Personel(int Id, string ad, string soyad, string adres, int roleId, int departmanId) : base(Id)
        {
            this._ad = ad;
            this._soyad = soyad;
            this._adres = adres;
            this._roleId = roleId;
            this._departmanId = departmanId;
        }
        public string ad { get { return this._ad; } set { this._ad = value; } }
        public string soyad { get { return this._soyad; } set { this._soyad = value; } }
        public string adres { get { return this._adres; } set { this._adres = value; } }
        public int roleId { get { return this._roleId; } set { this._roleId = value; } }
        public int departmanId { get { return this._departmanId; } set { this._departmanId = value; } }
    }
}
