using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    public class Dersler : BaseClass
    {
        private string _ad;
        private int _akts;
        private int _donem;
        private int _personelId;
        private int _sinifId;

        public Dersler(int id, string ad, int akts, int donem, int personelId, int sinifId) : base(id)
        {
            this._ad = ad;
            this._akts = akts;
            this._donem = donem;
            this._personelId = personelId;
            this._sinifId = sinifId;
        }
        public string ad { get { return this._ad; } set { this._ad = value; } }
        public int akts { get { return this._akts; } set { this._akts = value; } }
        public int donem{ get { return this._donem; } set { this._donem= value; } }
        public int personelId{ get { return this._personelId; } set { this._personelId= value; } }
        public int sinifId{ get { return this._sinifId; } set { this._sinifId= value; } }


    }
}
