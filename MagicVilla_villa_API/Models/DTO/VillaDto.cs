using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_villa_API.Models.DTO
{
    public class VillaDto
    {
        public int id { get; set; }
        [Required]
        [StringLength(100)]
        [MaxLength(30)]
        [DisplayName("Name Required ")]
        public string name { get; set; }
        public string detalis { get; set; }
        public double rate { get; set; }
        public double sqft { get; set; }
        public string imgurl { get; set; }
    }
}
