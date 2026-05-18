using System.ComponentModel.DataAnnotations;
using isKatmani;

namespace Obs.Models
{
    public class DersSecimFormViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Ogrenci")]
        public int OgrenciId { get; set; }

        [Required(ErrorMessage = "Ders secimi zorunludur.")]
        [Display(Name = "Ders")]
        public int DersId { get; set; }

        [Range(0, 100, ErrorMessage = "Puan 0 ile 100 arasinda olmalidir.")]
        public int Puan { get; set; }

        public bool Basari { get; set; }

        [Display(Name = "Onay durumu")]
        public string OnayDurumu { get; set; } = "Beklemede";

        public List<Dersler> Dersler { get; set; } = new();
        public List<Ogrenci> Ogrenciler { get; set; } = new();
    }
}
