using isKatmani;

namespace Obs.Models
{
    public class DersSecimViewModel
    {
        public int? OgrenciId { get; set; }
        public List<Ogr_Ders> Secimler { get; set; } = new();
        public List<Dersler> Dersler { get; set; } = new();
        public List<Ogrenci> Ogrenciler { get; set; } = new();
    }
}

