using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GUI_Hotel.Models.DataModels
{
    public class Booking
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        public int RoomNumber { get; set; }


        public int AdultsBooked { get; set; }

        public int AdultsCheckedIn { get; set; }

        public int ChildrenBooked { get; set; }

        public int ChildrenCheckedIn { get; set; }




    }
}
