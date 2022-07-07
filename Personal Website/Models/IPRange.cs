namespace Personal_Website.Models
{
    public class IPRange
    {
        public int Id { get; set; }
        public long BeginIPAddress { get; set; }
        public long EndIPAddress { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public int Count { get; set; }
    }
}
