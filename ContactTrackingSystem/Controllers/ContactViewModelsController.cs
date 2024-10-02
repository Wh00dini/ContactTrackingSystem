using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContactTrackingSystem.Context;
using ContactTrackingSystem.Models;

namespace ContactTrackingSystem.Controllers
{
    public class ContactViewModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactViewModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ContactViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contacts.ToListAsync());
        }

        // GET: ContactViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactViewModel = await _context.Contacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactViewModel == null)
            {
                return NotFound();
            }

            return View(contactViewModel);
        }

        // GET: ContactViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,PhoneNumber,Zip")] ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactViewModel);
        }

        // GET: ContactViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactViewModel = await _context.Contacts.FindAsync(id);
            if (contactViewModel == null)
            {
                return NotFound();
            }
            return View(contactViewModel);
        }

        // POST: ContactViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,PhoneNumber,Zip")] ContactViewModel contactViewModel)
        {
            if (id != contactViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactViewModelExists(contactViewModel.Id))
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
            return View(contactViewModel);
        }

        // GET: ContactViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactViewModel = await _context.Contacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactViewModel == null)
            {
                return NotFound();
            }

            return View(contactViewModel);
        }

        // POST: ContactViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactViewModel = await _context.Contacts.FindAsync(id);
            if (contactViewModel != null)
            {
                _context.Contacts.Remove(contactViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactViewModelExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}
