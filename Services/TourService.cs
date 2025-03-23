using TourProject.Models;
using TourProject.Models.DTOs;
using TourProject.Persistence;

namespace TourProject.Services
{
    public class TourService
    {
        private readonly ApiDbContext _context;
        private readonly ApiUser _apiUser;
        public TourService(ApiDbContext context, ApiUser apiUser)
        {
            _context = context;
            _apiUser = apiUser;

        }
        public async Task<Tour> CreateTour(TourDto tourDto)
        {
            var tour = new Tour()
            {
                Id = Guid.NewGuid(),
                Name = tourDto.Name,
                Description = tourDto.Description,
                CreatorId = _apiUser.Id,
            };
            await _context.Tours.AddAsync(tour);
            await _context.SaveChangesAsync();
            return tour;
        }
        public async Task<User> JoinTour(Guid TourId)
        {
            var user = await _apiUser.Fetch(u => u.CreatedTours);
            return user;

        }
    }
}
