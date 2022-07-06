using System.Collections.Generic;

namespace Personal_Website.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string LocalName { get; set; }
        public string InternationalName { get; set; }
        public string ISO { get; set; }
        public virtual ICollection<IPRange> IpRanges { get; set; }
        public virtual ICollection<Visitor> Visitors { get; set; }
    }
}
