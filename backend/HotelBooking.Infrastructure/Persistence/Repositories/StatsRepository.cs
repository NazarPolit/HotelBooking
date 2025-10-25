using HotelBooking.Application.Dto;
using HotelBooking.Application.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Infrastructure.Persistence.Repositories
{
	public class StatsRepository : IStatsRepository
	{
		private readonly string _connectionString;
        public StatsRepository(IConfiguration configuration)
        {
			_connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<BookingStatsDto>> GetBookingStatsAsync()
		{
			var sql = @"SELECT
							CAST(DateFrom AS DATE) AS Date,
							COUNT(*) AS BookingCount
						FROM
							Bookings
						WHERE
							DateFrom > DATEADD(month, -3, GETDATE())
						GROUP BY
							CAST(DateFrom AS DATE)
						ORDER BY
							Date DESC";

			using(var connection = new SqlConnection(_connectionString))
			{
				var stats = await connection.QueryAsync<BookingStatsDto>(sql);
				return stats;
			}
		}
	}
}
