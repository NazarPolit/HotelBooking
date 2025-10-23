using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Dto
{
    public class CreateBookingDto
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public int RoomId { get; set; }
    }
}
