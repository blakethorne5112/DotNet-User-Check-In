using System.Data.Entity;
using DotNetAssign2.Data;
using DotNetAssign2.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAssign2.Controllers
{
    public class LocationsController : Controller
    {
        private readonly UsersContext _context;

        public LocationsController(UsersContext context)
        {
            _context = context;
        }

        // GET: Locations
        [HttpGet("locations")]
        public async Task<IActionResult> Index()
        {
            var locations = await _context.Locations.ToListAsync();
            return View(locations);
        }

        // GET: Locations/5/Records
        // Get a list of user records for a location
        [HttpGet("locations/{id}")]
        public async Task<IActionResult> Records(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .Where(m => m.Id == id)
                .Include(m => m.UsersLocations)
                .FirstOrDefaultAsync();

            if (location == null)
            {
                return NotFound();
            }

            return View(location.UsersLocations);
        }

        // GET: Locations/5/Details
        // Get the details of a location
        [HttpGet("locations/{id}/details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/5/CheckIn
        // Check a user into a location
        [HttpPost("locations/{id}/checkin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckIn([Bind("ID,Name,Email,Phone,CheckedIn,CheckInTime,CheckOutTime")] Users users, int? eventId)
        {
            if (!eventId.HasValue)
            {
                return NotFound();
            }

            // Check if the user is already checked in by getting the UsersLocations record for the user and location
            var userLocation = await _context.UserLocations
                .Where(m => m.UsersID == users.ID)
                .Where(m => m.LocationsID == eventId)
                .FirstOrDefaultAsync();

            if (userLocation != null)
            {
                return NotFound("User is already checked in.");
            }

            if (ModelState.IsValid)
            {

                users.CheckedIn = true;
                users.CheckInTime = DateTime.Now;
                users.CheckOutTime = DateTime.Now;
                _context.Add(users);

                // Create a new UsersLocations record for the user and location
                var newUserLocation = new UsersLocations()
                {
                    UsersID = users.ID,
                    LocationsID = eventId.Value,
                    CheckedIn = true,
                    CheckInTime = DateTime.Now,
                    CheckOutTime = null
                };

                _context.Add(newUserLocation);

                await _context.SaveChangesAsync();

                if (users.Phone != null)
                {
                    Response.Cookies.Append("phoneNumber", users.Phone, new CookieOptions
                    {
                        Path = "/",
                        HttpOnly = true,
                        Secure = true,
                        MaxAge = TimeSpan.FromDays(30) // Set the cookie to expire in 30 days
                    });
                }

                return RedirectToAction("Index", "Home");
            }
            return View(users);
        }

        [HttpGet("locations/{id}/checkin")]
        public async Task<IActionResult> CheckIn(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            if (location == null)
            {
                return NotFound("Location not found.");
            }

            return View(location);
        }

        // POST: Locations/5/CheckOut
        // Check a user out of a location
        [HttpPost("locations/{eventId}/checkout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut([Bind("ID,Name,Email,Phone,CheckedIn,CheckInTime,CheckOutTime")] Users users, int? eventId)
        {
            if (!eventId.HasValue)
            {
                return NotFound();
            }

            // Check if the user is already checked in by getting the UsersLocations record for the user and location
            var userLocation = await _context.UserLocations
                .Where(m => m.UsersID == users.ID)
                .Where(m => m.LocationsID == eventId)
                .FirstOrDefaultAsync();

            if (userLocation == null)
            {
                return NotFound("User is not checked in.");
            }

            if (ModelState.IsValid)
            {
                users.CheckedIn = false;
                users.CheckInTime = userLocation.CheckInTime;
                users.CheckOutTime = DateTime.Now;
                _context.Update(users);

                // Update the UsersLocations record for the user and location
                userLocation.CheckedIn = false;
                userLocation.CheckOutTime = DateTime.Now;
                _context.Update(userLocation);

                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            return View(users);
        }

        // GET: Locations/Create
        // Get the create location page
        [HttpGet("locations/create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // Create a new location
        [HttpPost("locations/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,MapsLink")] Locations locations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Locations/5/Edit
        // Get the edit location page
        [HttpGet("locations/{id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var locations = await _context.Locations.FindAsync(id);
            if (locations == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Locations/Edit/5

    }
}