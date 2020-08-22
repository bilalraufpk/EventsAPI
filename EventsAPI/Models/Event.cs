using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsAPI.Models
{
    public partial class Event
    {
        public Event()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Title { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; }
        public bool? IsDeleted { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Event")]
        public User User { get; set; }
        [InverseProperty("Event")]
        public ICollection<Order> Order { get; set; }
    }
}
