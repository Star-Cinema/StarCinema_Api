using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Services;
using StarCinema_Api.Services.FilmsService;

namespace StarCinema_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IFilmsService _filmsService;

        public FilmsController(IFilmsService filmsService)
        {
            _filmsService = filmsService;
        }
        // POST: api/Films
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateFilms(FilmDTO filmDTO)
        {
            var resData = await _filmsService.CreateFilm(filmDTO);
            return StatusCode(resData.code, resData);
        }

        // PUT: api/Films/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFilms(int id, FilmDTO film)
        {
            var resData = await _filmsService.UpdateFilm(id, film);
            return StatusCode(resData.code, resData);
        }

        // DELETE: api/Films/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilms(int id)
        {
            var resData = await _filmsService.DeleteFilmById(id);
            return StatusCode(resData.code, resData);
        }

        // GET: api/Schedules
        [HttpGet]
        public async Task<ActionResult> GetFilms(string? name, string? director, string? country, string? category, int page = 0, int limit = 10)
        {
            var resData = await _filmsService.GetAllFilms(null, null, null, null);
            return StatusCode(resData.code, resData);
        }

        // GET: api/Schedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetFilmById(int id)
        {
            var resData = await _filmsService.GetFilmById(id);
            return StatusCode(resData.code, resData);
        }
    }
}
