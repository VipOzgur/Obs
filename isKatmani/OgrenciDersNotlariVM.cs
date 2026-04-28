using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isKatmani
{
    public class OgrenciDersNotlariVM
    {
        public int OgrenciId { get; set; }
        public string OgrenciAd { get; set; }
        public string OgrenciSoyad { get; set; }
        public DateTime KayitTarihi { get; set; }
        public DateTime? MezunTarihi { get; set; }

        public int DersId { get; set; }
        public string DersAd { get; set; }
        public int Akts { get; set; }
        public int Donem { get; set; }

        public string OgretimUyesi { get; set; }
        public string SinifAd { get; set; }

        public bool Basari { get; set; }
        public decimal Puan { get; set; }

        public string HarfNotu { get; set; }
        public string DurumAd { get; set; }
    }
}
