using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    public class PersonelDetayVM
    {
        public int PersonelId { get; set; }
        public string PersonelAd { get; set; }
        public string PersonelSoyad { get; set; }
        public string TamAd { get; set; }
        public string PersonelAdres { get; set; }

        public int RoleId { get; set; }
        public string RoleAd { get; set; }

        public int DepartmanId { get; set; }
        public string DepartmanAd { get; set; }

        public int FakulteId { get; set; }
        public string FakulteAd { get; set; }
        public DateTime FakulteKurulusTarihi { get; set; }
        public string FakulteAdres { get; set; }
    }
}
