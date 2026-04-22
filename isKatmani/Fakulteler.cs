using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    public class Fakulteler : BaseClass
    {
        private string _ad;
        private string _adres;
        private DateOnly _kurulusTarihi;

        public Fakulteler(int id, string ad,string adres, DateOnly kurulusTarihi):base(id)
        {
            this._ad = ad;
            this._adres = adres;    
            this._kurulusTarihi = kurulusTarihi;
        }
        public string ad { get { return this._ad; } set {this._ad=value ; } }
        public string adres { get { return this._adres; } set {this._adres=value ; } }
        public DateOnly kurulusTarihi{ get { return this._kurulusTarihi; } set {this._kurulusTarihi=value ; } }
    }
}
