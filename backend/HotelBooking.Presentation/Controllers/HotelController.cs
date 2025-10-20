using HotelBooking.Application.Dto;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> CreateHotel([FromBody] CreateAndUpdateHotelDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdHotel = await _hotelService.CreateHotelAsync(dto);

            return CreatedAtAction(nameof(GetHotelById), new {id = createdHotel.Id}, createdHotel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            // Заглушка, щоб CreatedAtAction працював
            // Тут буде виклик _hotelService.GetHotelByIdAsync(id)
            return Ok(new { Message = $"Hotel with id {id} will be here." });
        }
    }
}
