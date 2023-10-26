using Microsoft.EntityFrameworkCore;
using DotNetAssign2.Data;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAssign2.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// For the /Events/List route
        /// </summary>
        /// <returns>List of events</returns>
        public async Task<IActionResult> Records()
        {
            return _context.Events != null ?
                        View(await _context.Events.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Users'  is null.");
        }
    }
}
