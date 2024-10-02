using AutoMapper;
using ContactTrackingSystem.Context;
using ContactTrackingSystem.Entities;
using ContactTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactTrackingSystem.Services
{
    public class ContactRepository: IContactRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ContactRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<ContactViewModel> GetContact(int? id)
        {
            ContactViewModel contactModel = null;// new ContactViewModel();
            

            var contact = await _context.Contacts.FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
                return contactModel;

            contactModel = _mapper.Map<ContactViewModel>(contact);
            return contactModel;
        }

        public async Task<List<ContactViewModel>> GetContacts(string filterData)
        {
            var listContacts = new List<ContactViewModel>();

            var contacts = await _context.Contacts.ToListAsync();

            if (contacts == null)
                return listContacts;

            if (filterData != null)
                contacts = contacts.Where(x => x.FirstName.Contains(filterData) 
                || x.LastName.Contains(filterData) || x.Email.Contains(filterData)).ToList();
            listContacts = _mapper.Map<List<ContactViewModel>>(contacts);
            return listContacts;
        }

        public async Task AddContact(ContactViewModel contactModel)
        {
            var contact = _mapper.Map<Contact>(contactModel);

            _context.Add(contact);
            await _context.SaveChangesAsync();
        }
    }
}
