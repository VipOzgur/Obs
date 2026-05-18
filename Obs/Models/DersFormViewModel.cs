using isKatmani;

namespace Obs.Models
{
    public class DersFormViewModel
    {
        public Dersler Ders { get; set; } = new();
        public List<Personel> Personeller { get; set; } = new();
        public List<Siniflar> Siniflar { get; set; } = new();
    }
}
