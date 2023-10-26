using Microsoft.EntityFrameworkCore;
using DotNetAssign2.Data;
using Microsoft.AspNetCore.Mvc;
using DotNetAssign2.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace DotNetAssign2.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private UserManager<User> _userManager;

        public EventsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// For the /Events/List route
        /// </summary>
        /// <returns>List of events</returns>
        public async Task<IActionResult> Records()
        {
            return _context.Locations != null ?
                        View(await _context.Locations.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Users'  is null.");
        }

        /// <summary>
        /// Checkin to an event by using the passed in event id and the currently logged in userId
        /// </summary>

        /// <param name="id">The id of the event to checkin to</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckIn(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // get authentication state
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            // get the event
            var currentEvent = await _context.Locations.FindAsync(id);

            if (currentEvent == null)
            {
                return NotFound();
            }

            // create a new UserEvent
            var userEvent = new UserLocation
            {
                User = user,
                Location = currentEvent,
                CheckinTime = DateTime.Now
            };

            // add the UserEvent to the database and onConflict do nothing
            _context.UserLocation.Add(userEvent);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(currentEvent.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception)
            {
                return Problem("Unable to checkin to event.");
            }

            return RedirectToAction(nameof(Records));


        }

        /// <summary>
        /// Checkout of an event by using the passed in event id and the currently logged in userId
        /// </summary>
        /// <param name="id">Event Id</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // get authentication state
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            // get the event
            var currentEvent = await _context.Locations.FindAsync(id);

            if (currentEvent == null)
            {
                return NotFound();
            }

            // get the UserEvent
            var userEvent = await _context.UserLocation
                .Where(ue => ue.User == user && ue.Location == currentEvent)
                .FirstOrDefaultAsync();

            if (userEvent == null)
            {
                return NotFound();
            }

            // update the UserEvent
            userEvent.CheckoutTime = DateTime.Now;

            // update the UserEvent in the database
            _context.UserLocation.Update(userEvent);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(currentEvent.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception)
            {
                return Problem("Unable to checkout of event.");
            }

            return RedirectToAction(nameof(Records));
        }

        private bool EventExists(int id)
        {
            return _context.Locations.Any(e => e.Id == id);
        }


        public async Task<IActionResult> Create([Bind("Name,Description")] Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
