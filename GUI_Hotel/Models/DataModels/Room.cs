using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GUI_Hotel.Models.DataModels
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomId { get; set; }
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public Booking Booking { get; set; }
        [Required]
        public int ChildCount { get; set; }
        [Required]
        public int AdultCount { get; set; }
    }
}
