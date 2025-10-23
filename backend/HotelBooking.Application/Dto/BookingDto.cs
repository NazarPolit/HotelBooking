using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Dto
{
    public class BookingDto
    {
        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int NumberOfDays { get; set; } 
        public decimal TotalPrice { get; set; } 

        public int RoomId { get; set; }
        public int RoomCapacity { get; set; }
        public decimal RoomPricePerNight { get; set; }

        public int HotelId { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public string HotelAddress { get; set; } = string.Empty;
 
    }
}
