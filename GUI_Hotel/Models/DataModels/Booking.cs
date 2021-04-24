using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GUI_Hotel.Models.DataModels
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId{get;set;}

        [Required]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        public ICollection<Room> BookedRooms { get; set; }
    }
}
