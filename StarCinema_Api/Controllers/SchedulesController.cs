using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.Services;

namespace StarCinema_Api.Controllers
{
    /// <summary>
    ///     Account : AnhNT282
    ///     Description : Class controller for entity Schedule
    ///      Date created : 2023/05/18
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly ISchedulesService _schedulesService;
        
        

        // Constructor AnhNT282
        public SchedulesController(ISchedulesService SchedulesService)
        {
            _schedulesService = SchedulesService;
        }

        /// <summary>
        /// Get all schedules AnhNT282
        /// </summary>
        /// <param name="filmId"></param>
        /// <param name="roomId"></param>
        /// <param name="date"></param>
        /// <param name="sortDate"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>

        [HttpGet]
        public async Task<ActionResult> GetSchedules(int? filmId, int? roomId, DateTime? date, string? sortDate, int? page, int? limit)
        {
            var resData = await _schedulesService.GetAllSchedules(filmId, roomId, date, sortDate, page, limit);
            return StatusCode(resData.code, resData);
        }
        /// <summary>
        /// Get schedule by id AnhNT282
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        public async Task<ActionResult> GetScheduleById(int id)
        {
            var resData = await _schedulesService.GetScheduleById(id);
            return StatusCode(resData.code, resData);
        }
        /// <summary>
        /// Update schedule AnhNT282
        /// </summary>
        /// <param name="id"></param>
        /// <param name="schedules"></param>
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedules(int id, ScheduleDTO schedules)
        {
            var resData = await _schedulesService.UpdateSchedule(id, schedules);
            return StatusCode(resData.code, resData);
        }
        /// <summary>
        /// Create schedule AnhNT282
        /// </summary>
        /// <param name="schedules"></param>

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateSchedules(ScheduleDTO schedules)
        {
            var resData = await _schedulesService.CreateSchedule(schedules);
            return StatusCode(resData.code, resData);
        }

        /// <summary>
        /// Delete schedule AnhNT282
        /// </summary>
        /// <param name="id"></param>
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedules(int id)
        {
            var resData = await _schedulesService.DeleteSchedule(id);
            return StatusCode(resData.code, resData);
        }
    }
}
