using GUI_Hotel.Models;
using GUI_Hotel.Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GUI_Hotel.ViewModel
{
    public class KitchenViewModel
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public IEnumerable<Booking> Bookings { get; set; }
        public int CheckedInChildren { get => Bookings.Sum(b => b.ChildrenCheckedIn); }
        public int CheckedInAdults { get => Bookings.Sum(b => b.AdultsCheckedIn); }
        public int BookedChildren { get => Bookings.Sum(b => b.ChildrenBooked); }
        public int BookedAdults { get => Bookings.Sum(b => b.AdultsBooked); }

        public int ExpectedGuests { get => BookedAdults + BookedChildren; }
        public int GuestsNotCheckedIn { get => BookedAdults + BookedChildren - CheckedInChildren - CheckedInAdults; }

        public int ChildrenNotCheckedIn { get => BookedChildren - CheckedInChildren; }
        public int AdultsNotCheckedIn { get => BookedAdults - CheckedInAdults; }



    }
}
