using System.ComponentModel.DataAnnotations;

namespace MagicVilla_villa_API.Models
{
    public class Villa
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string detalis { get; set; }
        public double rate { get; set; }
        public double sqft { get; set; }
        public string imgurl { get; set; }
        public DateTime createdate { get; set; }
        public DateTime updateddate { get; set; }
    }
}
