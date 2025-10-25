using HotelBooking.Application.Dto;
using HotelBooking.Application.Interfaces;
using HotelBooking.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelBooking.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateHotel([FromBody] CreateAndUpdateHotelDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdHotel = await _hotelService.CreateHotelAsync(dto);

            return Ok(createdHotel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelByIdWithRooms(int id)
        {
            var hotelWithRooms = await _hotelService.GetHotelWithRooms(id);

            if (hotelWithRooms == null)
            {
                return NotFound(new { message = "Not Found" });
            }

            return Ok(hotelWithRooms);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Hotel>))]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _hotelService.GetAllHotelsAsync();
            return Ok(hotels);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var errorMessage = await _hotelService.DeleteHotelAsync(id);

            if (errorMessage != null)
                return BadRequest(new { message = errorMessage });

            return NoContent();

        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] CreateAndUpdateHotelDto dto)
        {
            var errorMessage = await _hotelService.UpdateHotelAsync(id, dto);

            if (errorMessage != null)
                return BadRequest(new { message = errorMessage });

            return NoContent();
        }

        [HttpGet("search")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Hotel>))]
		public async Task<IActionResult> GetAllAvaiableHotelsWithRooms(
            [FromQuery] string city,
            [FromQuery] DateTime dateFrom,
            [FromQuery] DateTime dateTo
        )
		{
			var hotels = await _hotelService.SearchAvailableRoomsAsync
            (
                city, 
                dateFrom, 
                dateTo
            );

			return Ok(hotels);
		}

	}
}
