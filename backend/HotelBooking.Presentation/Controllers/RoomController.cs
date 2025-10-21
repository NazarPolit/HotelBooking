using HotelBooking.Application.Dto;
using HotelBooking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelBooking.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddRoom([FromBody] CreateRoomDto roomDto)
        {          
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdRoom = await _roomService.AddRoomAsync(roomDto);

            return StatusCode(201, createdRoom);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var errorMessage = await _roomService.DeleteRoomAsync(id);

            if (errorMessage != null)
                return BadRequest(new { message = errorMessage });

            return NoContent();

        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] UpdateRoomDto dto)
        {
            var errorMessage = await _roomService.UpdateRoomAsync(id, dto);

            if (errorMessage != null)
                return BadRequest(new { message = errorMessage });

            return NoContent();
        }
    }
}
