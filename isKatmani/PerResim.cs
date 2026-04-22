using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    public class PerResim : BaseClass
    {
        private int _perId;
        private string _data;
        private string _path;

        public PerResim(int id, int perId, string data, string path) : base(id)
        {
            _perId = perId;
            _data = data;
            _path = path;
        }

        public int perId { get { return _perId; } set { _perId = value; } }
        public string data { get { return _data; } set { _data = value; } }
        public string path { get { return _path; } set { _path = value; } }
    }
}