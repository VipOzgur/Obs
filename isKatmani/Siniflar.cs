using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    public class Siniflar : BaseClass
    {
        private string _ad;
        private string _no;
        private int _fakulteId;

        public Siniflar(int id, string ad, string no, int fakulteId):base(id)
        {
            _ad = ad;
            _no = no;
            _fakulteId = fakulteId;
        }
        public string ad { get { return this._ad; } set { this._ad = value; } }
        public string no { get { return this._no; } set { this._no= value; } }
        public int fakulteId { get { return this._fakulteId; } set { this._fakulteId = value; } }
    }
}
