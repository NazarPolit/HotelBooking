using HotelBooking.Application.Dto;
using HotelBooking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StatsController : ControllerBase
	{
		private readonly IStatsRepository _statsRepository;

		public StatsController(IStatsRepository statsRepository)
        {
			_statsRepository = statsRepository;
		}

		[HttpGet]
		[Authorize(Roles = "Administrator")]
		[ProducesResponseType(typeof(IEnumerable<BookingStatsDto>), 200)]
		public async Task<IActionResult> GetBookingStats()
		{
			var stats = await _statsRepository.GetBookingStatsAsync();
			return Ok(stats);
		}
	}
}
