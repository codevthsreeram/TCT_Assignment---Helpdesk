using System;

namespace Asp_Core_with_Angular.Models
{
    public class TicketDetail
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
