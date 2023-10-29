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

        // Get the details of a location
        [HttpGet("locations/{id}")]
        public async Task<IActionResult> Index(int id)
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