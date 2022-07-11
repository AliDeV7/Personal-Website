using System.ComponentModel.DataAnnotations;

namespace Personal_Website.ViewModel
{
    public class TicketViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string message { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
