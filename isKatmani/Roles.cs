using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    public class Roles :BaseClass
    {
        private string _ad;
        
        public Roles(int Id,string ad):base(Id) { 
            this._ad = ad;
        }
        public string ad { get { return this._ad; } set { this._ad = value; } }
    }
}
