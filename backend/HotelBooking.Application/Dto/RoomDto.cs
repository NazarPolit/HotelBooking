using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Dto
{
    public class RoomDto
    {
        public int Id { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
    }
}
