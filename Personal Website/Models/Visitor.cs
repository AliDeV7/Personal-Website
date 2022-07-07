using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Personal_Website.Models
{
    public class Visitor
    {
        public int Id { get; set; }
        public string IP { get; set; }
        public int? CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
        public ICollection<VisitDetail> VisitDetails { get; set; }
    }

    public class VisitDetail
    {
        public int Id { get; set; }
        public int VisitorId { get; set; }
        [ForeignKey(nameof(VisitorId))]
        public Visitor Visitor { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public long Period { get; set; } // in a spcific date
        public ICollection<VisitDateDetail> VisitDateDetails { get; set; }

    }

    public class VisitDateDetail
    {
        public int Id { get; set; }
        public int VisitDetailId { get; set; }
        [ForeignKey(nameof(VisitDetailId))]
        public VisitDetail VisitDetail { get; set; }
        public long FullDate { get; set; }

    }
}
