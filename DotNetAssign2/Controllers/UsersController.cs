using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DotNetAssign2.Data;
using DotNetAssign2.Models;
using Microsoft.AspNetCore.Authorization;

namespace DotNetAssign2.Controllers
{
    public class UsersController : Controller
    {
        private readonly UsersContext _context;

        public UsersController(UsersContext context)
        {
            _context = context;
        }

        // GET: Users
        [Authorize(Roles = "Administrator,Staff")]
        public async Task<IActionResult> Records()
        {
            return _context.Users != null ?
                        View(await _context.Users.ToListAsync()) :
                        Problem("Entity set 'UsersContext.Users'  is null.");
        }

        // GET: Users/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public async Task<IActionResult> CheckIn()
        {
            var locations = await _context.Locations.ToListAsync();

            // map over locations and covert to MVC SelectListItems
            var locationSelectListItems = locations.Select(location => new SelectListItem
            {
                Text = location.Name,
                Value = location.Id.ToString()
            });

            var registerModel = new CheckinScreenModel
            {
                Locations = locationSelectListItems
            };

            return View(registerModel);
        }

        // GET: Users/CheckOut
        public IActionResult CheckOut()
        {
            var user = new Users();
            if (Request.Cookies.TryGetValue("phoneNumber", out var phoneNumber))
            {
                user.Phone = phoneNumber;
            }

            return View(user); // pass the user model to the view
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckIn([Bind("ID,Name,Email,Phone,CheckedIn,CheckInTime,CheckOutTime,LastCheckedInLocationId")] Users users)
        {
            var locations = await _context.Locations.ToListAsync();


            if (ModelState.IsValid)
            {
                // users.CheckedIn = true;
                // users.CheckInTime = DateTime.Now;
                // users.CheckOutTime = DateTime.Now;
                _context.Add(users);
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

            new CheckinScreenModel
            {
                Locations = locations.Select(location => new SelectListItem
                {
                    Text = location.Name,
                    Value = location.Id.ToString()
                })
            };
            return View(users);
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Email,Phone,CheckedIn,CheckInTime,CheckOutTime")] Users users)
        {
            if (id != users.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Records));
            }
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut([Bind("ID,Name,Email,Phone,CheckedIn,CheckInTime,CheckOutTime")] Users users)
        {
            // bool found = false;
            // foreach (Users user in _context.Users)
            // {
            //     if (users.Phone == user.Phone && user.CheckedIn == true)
            //     {
            //         users = user;
            //         found = true;
            //     }
            // }
            // if (!found)
            // {
            //     // No matching user found, display an error message to the user
            //     TempData["ErrorMessage"] = "There is no one matching that phone number.";
            //     return View("CheckOut", users);
            // }

            // // Updating user information
            // users.CheckedIn = false;
            // users.CheckOutTime = DateTime.Now;
            _context.Update(users);
            await _context.SaveChangesAsync();

            // Redirect to home. 
            return RedirectToAction("Index", "Home");
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'UsersContext.Users'  is null.");
            }
            var users = await _context.Users.FindAsync(id);
            if (users != null)
            {
                _context.Users.Remove(users);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Records));
        }

        private bool UsersExists(int id)
        {
            return (_context.Users?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
