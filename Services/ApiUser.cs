using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TourProject.Models;
using TourProject.Persistence;

namespace TourProject.Services
{
    public class ApiUser
    {
        private readonly HttpContext _HttpContext;
        private readonly ApiDbContext _context;
        private User? user = null;
        public ApiUser(IHttpContextAccessor accessor, ApiDbContext context)
        {
            var httpcontext = accessor?.HttpContext;
            if(httpcontext is null)
            {
                throw new Exception("context doesn't exist!");
            }
            _context = context;
            _HttpContext = httpcontext;
        }
        public async Task<User> Fetch(params Expression<Func<User, object>>[] includes)
        {
            if(user is null)
            {
                IQueryable<User> query = _context.Users;
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
                user = await query.FirstOrDefaultAsync();
                if(user is null)
                {
                    throw new Exception("user is not authenticated!");
                }
            }
            return user;
        }
        public Guid Id { get
            {
                if(_HttpContext?.User?.Identity?.Name is null)
                {
                    throw new Exception("user is not authenticated!");
                }
                return new Guid(_HttpContext.User.Identity.Name);
            } }
    }
}
