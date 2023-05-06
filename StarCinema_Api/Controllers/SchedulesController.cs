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
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly ISchedulesService _schedulesService;

        public SchedulesController(ISchedulesService SchedulesService)
        {
            _schedulesService = SchedulesService;
        }

        // GET: api/Schedules
        [HttpGet]
        public async Task<ActionResult> GetSchedules()
        {
            var resData = await _schedulesService.GetAllSchedules();
            return StatusCode(resData.code, resData);
        }

        // GET: api/Schedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetScheduleById(int id)
        {
            var resData = await _schedulesService.GetScheduleById(id);
            return StatusCode(resData.code, resData);
        }

        // PUT: api/Schedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedules(int id, ScheduleDTO schedules)
        {
            var resData = await _schedulesService.UpdateSchedule(id, schedules);
            return StatusCode(resData.code, resData);
        }

        // POST: api/Schedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateSchedules(ScheduleDTO schedules)
        {
            var resData = await _schedulesService.CreateSchedule(schedules);
            return StatusCode(resData.code, resData);
        }

        // DELETE: api/Schedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedules(int id)
        {
            var resData = await _schedulesService.DeleteSchedule(id);
            return StatusCode(resData.code, resData);
        }
    }
}
