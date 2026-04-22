using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    public class BaseClass : IBase
    {
        private int _id;
        public BaseClass(int id)
        {
            this._id = id;
        }

        public int Id
        {
            get { return _id; }

            set { this._id = value; }
        }
    }
}
