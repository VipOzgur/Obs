using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    public class Log : BaseClass
    {
        private int? _parentId;
        private int? _childId;
        private string _data;

        public Log(int id, int? parentId, int? childId, string data) : base(id)
        {
            _parentId = parentId;
            _childId = childId;
            _data = data;
        }
          
        public int? parentId { get { return _parentId; } set { _parentId = value; } }
        public int? childId { get { return _childId; } set { _childId = value; } }
        public string data { get { return _data; } set { _data = value; } }
    }
}