using HotelBooking.Application.Dto;
using HotelBooking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace HotelBooking.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookigService _bookingService;

        public BookingController(IBookigService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateBookingDto bookingDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdBooking = await _bookingService.CreateBookingAsync(bookingDto, userId);
                return CreatedAtAction(nameof(GetBookingById), new { id = createdBooking.Id }, createdBooking);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpGet("my")]
        [Authorize(Roles = "Client")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookingDto>))]
        public async Task<IActionResult> GetMyBookings()
		{
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                    return Unauthorized(new { message = "User ID not found in token." });

                var bookings = await _bookingService.GetMyBookingsAsync(userId);

                return Ok(bookings);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

		[HttpGet("all")]
		[Authorize(Roles = "Administrator")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<BookingDto>))]
		public async Task<IActionResult> GetAllBookings()
		{
			try
			{	
				var bookings = await _bookingService.GetAllBookingsAsync();

				return Ok(bookings);
			}
			catch (Exception)
			{
				return StatusCode(500, new { message = "An unexpected error occurred." });
			}
		}

		[HttpGet("{id}")]
        [Authorize(Roles = "Client,Administrator")]
        [ProducesResponseType(typeof(BookingDto), 200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isCurrentUserAdmin = User.IsInRole("Administrator");

            try
            {
                var booking = await _bookingService.GetBookingByIdAsync(id, currentUserId, isCurrentUserAdmin);

                if (booking == null)
                {
                    return NotFound(new { message = "Booking not found" });
                }

                return Ok(booking);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }
    } 
}
