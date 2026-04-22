using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    public class OgrResim : BaseClass
    {
        private int _ogrId;
        private string _data;
        private string _path;

        public OgrResim(int id, int ogrId, string data, string path) : base(id)
        {
            _ogrId = ogrId;
            _data = data;
            _path = path;
        }

        public int ogrId { get { return _ogrId; } set { _ogrId = value; } }
        public string data { get { return _data; } set { _data = value; } }
        public string path { get { return _path; } set { _path = value; } }
    }
}