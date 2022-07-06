using System.ComponentModel.DataAnnotations.Schema;

namespace Personal_Website.Models
{
    public class Visitor
    {
        public int Id { get; set; }
        public string IP { get; set; }
        public int Count { get; set; }
        public int CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
        public long Minutes { get; set; }
    }
}
