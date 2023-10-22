using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq.Expressions;

namespace houseHomework.Controllers
{
    public class SaleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SaleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sale
        public async Task<IActionResult> Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.Sale.Where(s => s.UserId == currentUserId).Include(s => s.House);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sale/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sale == null)
            {
                return NotFound();
            }

            var sale = await _context.Sale
                .Include(s => s.House)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // // GET: Sale/Create
        // [Authorize]
        // public IActionResult Create()
        // {
        //     ViewData["HouseId"] = new SelectList(_context.House, "Id", "Id");
        //     return View();
        // }

        // // POST: Sale/Create
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("Id,UserId,HouseId,SaleDate,SalePrice")] Sale sale)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         // var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //         // // Update the Sale object with the user ID, set IsSold to true, and set SaleDate to the current date and time
        //         // sale.UserId = currentUserId;
        //         // sale.SaleDate = DateTime.Now; // or DateTime.UtcNow if you prefer UTC time
        //         //  // Assuming you have a DbContext named _context
        //         // var house = _context.House.SingleOrDefault(h => h.Id == houseId);
        //         // var priceForSelectedHouse = 0;
        //         // if (house != null)
        //         // {
        //         //     house.IsSold = true;
        //         //     _context.SaveChanges();
        //         //     priceForSelectedHouse = house.Price; // Assuming the price is stored as a property in the House model
        //         // }
        //         // // Assuming you have logic to get the price for the selected house

        //         // // Set the sale price
        //         // sale.SalePrice = priceForSelectedHouse;
        //         _context.Add(sale);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     ViewData["HouseId"] = new SelectList(_context.House, "Id", "Id", sale.HouseId);
        //     return View(sale);
        // }

        // // GET: Sale/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null || _context.Sale == null)
        //     {
        //         return NotFound();
        //     }

        //     var sale = await _context.Sale.FindAsync(id);
        //     if (sale == null)
        //     {
        //         return NotFound();
        //     }
        //     ViewData["HouseId"] = new SelectList(_context.House, "Id", "Id", sale.HouseId);
        //     return View(sale);
        // }

        // // POST: Sale/Edit/5
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // [Authorize]
        // public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,HouseId,SaleDate,SalePrice")] Sale sale)
        // {
        //     if (id != sale.Id)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(sale);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!SaleExists(sale.Id))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     ViewData["HouseId"] = new SelectList(_context.House, "Id", "Id", sale.HouseId);
        //     return View(sale);
        // }

        // // GET: Sale/Delete/5
        // [Authorize]
        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null || _context.Sale == null)
        //     {
        //         return NotFound();
        //     }

        //     var sale = await _context.Sale
        //         .Include(s => s.House)
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (sale == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(sale);
        // }

        // // POST: Sale/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // [Authorize]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     if (_context.Sale == null)
        //     {
        //         return Problem("Entity set 'ApplicationDbContext.Sale'  is null.");
        //     }
        //     var sale = await _context.Sale.FindAsync(id);
        //     if (sale != null)
        //     {
        //         _context.Sale.Remove(sale);
        //     }
            
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }

        private bool SaleExists(int id)
        {
          return (_context.Sale?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CreateSale(int? id)
        {
            try{
           
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var sale = new Sale
                {
                    // Update the Sale object with the user ID, set IsSold to true, and set SaleDate to the current date and time
                    UserId = currentUserId,
                    SaleDate = DateTime.Now // or DateTime.UtcNow if you prefer UTC time
                };
                // Assuming you have a DbContext named _context
                var house = _context.House.SingleOrDefault(h => h.Id == id);
                var priceForSelectedHouse = 0.0;
                if (house != null)
                {
                    house.IsSold = true;
                    _context.SaveChanges();
                    priceForSelectedHouse = house.Price; // Assuming the price is stored as a property in the House model
                    sale.HouseId = house.Id;
                }
                // Assuming you have logic to get the price for the selected house
                
                // Set the sale price
                sale.SalePrice = priceForSelectedHouse;
                _context.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }catch (Exception ex){
                Console.WriteLine(ex);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
