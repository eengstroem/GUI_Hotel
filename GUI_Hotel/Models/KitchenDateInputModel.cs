using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GUI_Hotel.Models
{
    public class KitchenDateInputModel
    {

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
