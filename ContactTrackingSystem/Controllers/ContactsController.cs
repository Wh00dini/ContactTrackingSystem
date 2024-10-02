using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContactTrackingSystem.Context;
using ContactTrackingSystem.Entities;
using AutoMapper;
using ContactTrackingSystem.Services;
using ContactTrackingSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace ContactTrackingSystem.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactsController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IActionResult> Index(string filterData)
        {
            var contacts = await _contactRepository.GetContacts(filterData);

            return View(contacts);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]//just to be safe 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,PhoneNumber,Zip")] ContactViewModel contact)
        {
            if (ModelState.IsValid)
            {
                await _contactRepository.AddContact(contact);
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _contactRepository.GetContact(id);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }
    }
}
