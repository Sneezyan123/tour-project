using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourProject.Infrastructure.Filters;
using TourProject.Models;
using TourProject.Models.DTOs;
using TourProject.Services;

namespace TourProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly TourService _tourService;
        public TourController(TourService tourService)
        {
            _tourService = tourService;
        }
        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<Tour>> CreateTour([FromBody] TourDto tourDto)
        {
            var tour = await _tourService.CreateTour(tourDto);
            return Ok(tour);
        }
        [HttpPost("join/{id}")]
        [Authorize]
        public async Task<IActionResult> JoinTour(Guid Id)
        {
            var user = await _tourService.JoinTour(Id);
            return Ok(user);
        }
    }
}
