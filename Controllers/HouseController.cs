using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
// using MvcHouse.Data;
using MvcHouse.Models;
namespace houseHomework.Controllers
{
    public class HouseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: House
        public async Task<IActionResult> Index(string searchString)
        {
            
            if (_context.House == null){
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }
            // IQueryable<string> ariaQuery = from m in _context.House
            //                         orderby m.Area
            //                         select m.Area;
            var houses = from m in _context.House
                        where m.IsSold == false
                        select m ;

            if (!string.IsNullOrEmpty(searchString))
            {
                houses = houses.Where(s => s.Address!.Contains(searchString) && s.IsSold == false);
            }

            // if (!string.IsNullOrEmpty(houseAria))
            // {
            //     houses = houses.Where(x => x.Area == houseAria);
            // }

            var houseAddressVM = new HouseSearchCriteria
            {
                // Areas = new SelectList(await areaQuery.Distinct().ToListAsync()),
                Houses = await houses.ToListAsync()
            };

            return View(houseAddressVM);
        }

        // GET: House/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.House == null)
            {
                return NotFound();
            }

            var house = await _context.House
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (house == null)
            {
                
                return NotFound();
            }
            Console.WriteLine(house.Images);
            return View(house);
        }

        // GET: House/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: House/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Address,Bedrooms,Bathrooms,Price,Area,NumberOfRooms,Floor,IsSold,Images")] House house, List<IFormFile> Images)
        {
            if (ModelState.IsValid)
            {
                 if (Images != null && Images.Count > 0)
                {
                    var imageUrls = new List<Image>();

                    foreach (var image in Images)
                    {
                        if (image.Length > 0)
                        {
                            // Generate a unique file name for each image (you can use a library like Guid.NewGuid() or a custom logic)
                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                            // Define the directory where images will be stored (adjust this path as needed)
                            var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                            // Ensure the directory exists; create it if it doesn't
                            if (!Directory.Exists(uploadDirectory))
                            {
                                Directory.CreateDirectory(uploadDirectory);
                            }

                            // Combine the directory and file name to get the full path
                            var filePath = Path.Combine(uploadDirectory, fileName);

                            // Save the image to the server
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }

                            // Store the URL in the list
                            var imageUrl = "/uploads/" + fileName; // Adjust this URL as needed
                            var obj = new Image();
                            obj.ImageUrl = imageUrl;
                            imageUrls.Add(obj);
                        }
                    }

                    // Assign the list of image URLs to the House.Images property
                    house.Images = imageUrls;
                }
                _context.Add(house);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(house);
        }

        // GET: House/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.House == null)
            {
                return NotFound();
            }

            var house = await _context.House.FindAsync(id);
            if (house == null)
            {
                return NotFound();
            }
            return View(house);
        }

        // POST: House/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Address,Bedrooms,Bathrooms,Price,Area,NumberOfRooms,Floor,IsSold")] House house)
        {
            if (id != house.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(house);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseExists(house.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(house);
        }

        // GET: House/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.House == null)
            {
                return NotFound();
            }

            var house = await _context.House
                .FirstOrDefaultAsync(m => m.Id == id);
            if (house == null)
            {
                return NotFound();
            }

            return View(house);
        }

        // POST: House/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.House == null)
            {
                return Problem("Entity set 'ApplicationDbContext.House'  is null.");
            }
            var house = await _context.House.FindAsync(id);
            if (house != null)
            {
                _context.House.Remove(house);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HouseExists(int id)
        {
          return (_context.House?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> ConfirmHouse(int? id)
        {
            if (id == null || _context.House == null)
            {
                return NotFound();
            }

            var house = await _context.House.FindAsync(id);
            if (house == null)
            {
                return NotFound();
            }
            return View(house);
        }
    }
}
