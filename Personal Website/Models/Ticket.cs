using System;
using System.ComponentModel.DataAnnotations;

namespace Personal_Website.Models
{
    public class Ticket
    {
        public int ID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        public TicketStatus Status { get; set; }
        [MaxLength(13)]
        public long Creation_DateTime { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        [MaxLength(4028)]
        public string Text { get; set; }

    }
    public enum TicketStatus
    {
        Open = 1,
        Close = 2
    }
}
