using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsAPI.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public bool? IsDeleted { get; set; }

        [ForeignKey("EventId")]
        [InverseProperty("Order")]
        public Event Event { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Order")]
        public User User { get; set; }
    }
}
