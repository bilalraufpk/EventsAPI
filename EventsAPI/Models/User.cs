using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsAPI.Models
{
    public partial class User
    {
        public User()
        {
            Event = new HashSet<Event>();
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(50)]
        public string Role { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; }

        [InverseProperty("User")]
        public ICollection<Event> Event { get; set; }
        [InverseProperty("User")]
        public ICollection<Order> Order { get; set; }
    }
}
