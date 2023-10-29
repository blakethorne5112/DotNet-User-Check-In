using DotNetAssign2.Data;
using DotNetAssign2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DotNetAssign2.Controllers
{
    /// <summary>
    /// The controller for locations.
    /// </summary>
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

        // Get a list of user records for a location
        [HttpGet("locations/{id}")]
        public async Task<IActionResult> UserLocations(int id)
        {
            var location = await _context.Locations
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            if (location == null)
            {
                return NotFound("Location not found.");
            }

            var userLocations = await _context.UserLocations
                .Where(m => m.LocationsId == id)
                .ToListAsync();

            if (userLocations == null)
            {
                return NotFound("No users checked in.");
            }

            return View(userLocations);
        }

        // Get the details of a location
        [HttpGet("locations/{id}/details")]
        public async Task<IActionResult> Details(int id)
        {
            var location = await _context.Locations
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            if (location == null)
            {
                return NotFound("Location not found.");
            }

            return View(location);
        }

        [HttpGet("locations/{id}/delete")]
        [Authorize(Roles = "Administrator,Staff")]
        public async Task<IActionResult> Delete(int id)
        {
            var location = await _context.Locations.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (location == null)
            {
                return NotFound("Location not found.");
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Locations");
        }

        // Check a user into a location
        [HttpPost("locations/{id}/checkin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckIn([Bind("ID,Name,Email,Phone,CheckedIn,CheckInTime,CheckOutTime")] Users users, int id)
        {

            var location = await _context.Locations
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            if (location == null)
            {
                return NotFound("Location not found.");
            }

            // Check if the user is already checked in by getting the UsersLocations record for the user and location
            var userLocation = await _context.UserLocations
                .Where(m => m.UsersID == users.ID)
                .Where(m => m.LocationsId == id)
                .FirstOrDefaultAsync();

            if (userLocation != null)
            {
                return NotFound("User is already checked in.");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Locations", "Index");
            }

            var newUser = new Models.Users
            {
                Email = users.Email,
                Name = users.Name,
                Phone = users.Phone
            };

            _context.Add(newUser);

            await _context.SaveChangesAsync();

            // Create a new UsersLocations record for the user and location
            var newUserLocation = new UsersLocations()
            {
                UsersID = users.ID,
                LocationsId = id,
                CheckedIn = true,
                CheckInTime = DateTime.Now,
                CheckOutTime = null,
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

            return RedirectToAction("Locations", "Index");
        }

        [HttpGet("locations/{id}/checkin")]
        public async Task<IActionResult> CheckIn(int id)
        {

            var location = await _context.Locations
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            if (location == null)
            {
                return NotFound("Location not found.");
            }

            return View();
        }

        // POST: Locations/5/CheckOut
        // Check a user out of a location
        [HttpPost("locations/{eventId}/checkout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut([Bind("Phone")] Users users, int eventId)
        {
            var cookieUserPhone = Request.Cookies["phoneNumber"];

            // find user by phone number
            var user = await _context.Users
                .Where(m => m.Phone == cookieUserPhone || m.Phone == users.Phone)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Check if the user is already checked in by getting the UsersLocations record for the user and location
            var userLocation = await _context.UserLocations
                .Where(m => m.UsersID == user.ID)
                .Where(m => m.LocationsId == eventId)
                .FirstOrDefaultAsync();

            if (userLocation == null)
            {
                return NotFound("User is not checked in.");
            }

            if (userLocation.CheckOutTime != null)
            {
                return NotFound("User is already checked out.");
            }

            _context.Update(user);

            // Update the UsersLocations record for the user and location
            userLocation.CheckedIn = false;
            userLocation.CheckOutTime = DateTime.Now;
            _context.Update(userLocation);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Locations");
        }

        [HttpGet("locations/{id}/checkout")]
        public async Task<IActionResult> CheckOut(int id)
        {

            var location = await _context.Locations
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            if (location == null)
            {
                return NotFound("Location not found.");
            }

            var user = new Users(); 
            if (Request.Cookies.TryGetValue("phoneNumber", out var phoneNumber))
            {
                user.Phone = phoneNumber;
            }

            return View(user);
        }

        // GET: Locations/Create
        // Get the create location page
        [HttpGet("locations/create")]
        [Authorize(Roles = "Administrator,Staff")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // Create a new location
        [HttpPost("locations/create")]
        [Authorize(Roles = "Administrator,Staff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,MapsLink")] Locations locations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Locations");
        }

        // GET: Locations/5/Edit
        // Get the edit location page
        [HttpGet("locations/{id}/edit")]
        [Authorize(Roles = "Administrator,Staff")]
        public async Task<IActionResult> Edit(int id)
        {
            var locations = await _context.Locations.FindAsync(id);
            if (locations == null)
            {
                return NotFound();
            }
            return View(locations);
        }

        // POST: Locations/5/Edit
        // Edit a location
        [HttpPost("locations/{id}/edit")]
        [Authorize(Roles = "Administrator,Staff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,MapsLink")] Locations locations)
        {
            if (id != locations.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationsExists(locations.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Locations");
            }
            return View(locations);
        }

        private bool LocationsExists(int id)
        {
            return _context.Locations.Any(e => e.Id == id);
        }

    }
}