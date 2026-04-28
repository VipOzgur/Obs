using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    namespace EntityLayer.ViewModels
    {
        public class DepartmanIstatistikVM 
        {
            

            public int FakulteId { get; set; }
            public string FakulteAd { get; set; }

            public int DepartmanId { get; set; }
            public string DepartmanAd { get; set; }

            public int PersonelSayisi { get; set; }
            public int DersSayisi { get; set; }
            public int ToplamAkts { get; set; }

            public int ProfesorSayisi { get; set; }
            public int DocentSayisi { get; set; }
            public int ArastirmaciSayisi { get; set; }
        }
    }
}
