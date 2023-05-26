using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.Services;

namespace StarCinema_Api.Controllers
{
    /*
        Account : AnhNT282
        Description : Class controller for entity Schedule
        Date created : 2023/05/18
    */
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

        // Get all schedules AnhNT282
        [HttpGet]
        public async Task<ActionResult> GetSchedules(int? filmId, int? roomId, DateTime? date, string? sortDate, int? page, int? limit)
        {
            var resData = await _schedulesService.GetAllSchedules(filmId, roomId, date, sortDate, page, limit);
            return StatusCode(resData.code, resData);
        }

        // Get schedule by id AnhNT282
        [HttpGet("{id}")]
        public async Task<ActionResult> GetScheduleById(int id)
        {
            var resData = await _schedulesService.GetScheduleById(id);
            return StatusCode(resData.code, resData);
        }

        // Update schedule AnhNT282
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedules(int id, ScheduleDTO schedules)
        {
            var resData = await _schedulesService.UpdateSchedule(id, schedules);
            return StatusCode(resData.code, resData);
        }

        // Create schedule AnhNT282
        [HttpPost]
        public async Task<IActionResult> CreateSchedules(ScheduleDTO schedules)
        {
            var resData = await _schedulesService.CreateSchedule(schedules);
            return StatusCode(resData.code, resData);
        }

        // Delete schedule AnhNT282
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedules(int id)
        {
            var resData = await _schedulesService.DeleteSchedule(id);
            return StatusCode(resData.code, resData);
        }
    }
}
