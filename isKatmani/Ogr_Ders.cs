using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    public class Ogr_Ders : BaseClass
    {
        private int _ogrId;
        private int _dersId;
        private bool _basari;
        private int _puan;

        public Ogr_Ders(int id, int ogrId, int dersId, bool basari, int puan) : base(id)
        {
            _ogrId = ogrId;
            _dersId = dersId;
            _basari = basari;
            _puan = puan;
        }

        public int ogrId { get { return _ogrId; } set { _ogrId = value; } }
        public int dersId { get { return _dersId; } set { _dersId = value; } }
        public bool basari { get { return _basari; } set { _basari = value; } }
        public int puan { get { return _puan; } set { _puan = value; } }
    }
}