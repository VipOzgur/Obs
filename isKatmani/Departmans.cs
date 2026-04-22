using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    public class Departmans : BaseClass
    {
        private string _ad;
        private int _fakulteId;

        public Departmans(int Id, string ad, int fakulteId) : base(Id)
        {
            this._ad = ad;
            this._fakulteId = fakulteId;
        }
        public string ad { get { return this._ad; } set { this._ad = value; } }
        public int fakulteId { get { return this._fakulteId; } set { this._fakulteId = value ;} }  
    }
}
