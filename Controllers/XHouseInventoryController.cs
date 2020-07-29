using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Data;
using WebApp.Models;
using System.Data;

namespace App.Controllers
{
    public class XHouseInventoryController : Controller
    {
        private readonly AppDbContext _context;

        public XHouseInventoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: XHouseInventory
        public async Task<IActionResult> Index()
        {
            return View(await _context.XHouseInventory.ToListAsync());
        }

        // GET: XHouseInventory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xHouseInventory = await _context.XHouseInventory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (xHouseInventory == null)
            {
                return NotFound();
            }

            return View(xHouseInventory);
        }

        // GET: XHouseInventory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: XHouseInventory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Sku")] XHouseInventory xHouseInventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(xHouseInventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(xHouseInventory);
        }

        [HttpGet()]  // 为了演示方便，未采用HttpPost
        public async Task<IActionResult> Purchase()
        {
            var x = _context.XHouseInventory.FirstOrDefault(x => x.Name == "XName");
            if(x.Sku > 0)
            {
                x.Sku -= 1;
                var order = new XOrder{ Description = $"订单...{DateTime.Now}", XHouseInventory = x, };
                _context.XOrders.Add(order);
                await _context.SaveChangesAsync();
            }
            return Json(x);
        }

        // GET: XHouseInventory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xHouseInventory = await _context.XHouseInventory.FindAsync(id);
            if (xHouseInventory == null)
            {
                return NotFound();
            }
            return View(xHouseInventory);
        }

        // POST: XHouseInventory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Sku")] XHouseInventory xHouseInventory)
        {
            if (id != xHouseInventory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(xHouseInventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!XHouseInventoryExists(xHouseInventory.Id))
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
            return View(xHouseInventory);
        }

        // GET: XHouseInventory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xHouseInventory = await _context.XHouseInventory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (xHouseInventory == null)
            {
                return NotFound();
            }

            return View(xHouseInventory);
        }

        // POST: XHouseInventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var xHouseInventory = await _context.XHouseInventory.FindAsync(id);
            _context.XHouseInventory.Remove(xHouseInventory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool XHouseInventoryExists(int id)
        {
            return _context.XHouseInventory.Any(e => e.Id == id);
        }
    }
}
