using ContactTrackingSystem.Models;

namespace ContactTrackingSystem.Services
{
    public interface IContactRepository
    {
        Task<ContactViewModel> GetContact(int? id);
        Task<List<ContactViewModel>> GetContacts(string filter);
        Task AddContact(ContactViewModel contactModel);
    }
}
