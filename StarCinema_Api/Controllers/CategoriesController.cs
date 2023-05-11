using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.DTOs;
using StarCinema_Api.Services.CategoriesService;

namespace StarCinema_Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _CategoriesService;

        public CategoriesController(ICategoriesService CategoriesService)
        {
            _CategoriesService = CategoriesService;
        }
        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateCategories(CategoriesDTO CategoriesDTO)
        {
            var resData = await _CategoriesService.CreateCategories(CategoriesDTO);
            return StatusCode(resData.code, resData);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategories(int id, CategoriesDTO Categories)
        {
            var resData = await _CategoriesService.UpdateCategories(id, Categories);
            return StatusCode(resData.code, resData);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategories(int id)
        {
            var resData = await _CategoriesService.DeleteCategoriesById(id);
            return StatusCode(resData.code, resData);
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult> GetCategories(string? name, int page = 0, int limit = 10)
        {
            var resData = await _CategoriesService.GetAllCategories(null);
            return StatusCode(resData.code, resData);
        }

        // GET: api/Schedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoriesById(int id)
        {
            var resData = await _CategoriesService.GetCategoriesById(id);
            return StatusCode(resData.code, resData);
        }
    }

}
