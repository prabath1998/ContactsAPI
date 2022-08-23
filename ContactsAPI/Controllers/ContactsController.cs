using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsApiDbContext dbContext;

        public ContactsController(ContactsApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        //get all contacts
        [HttpGet]
        public async Task<IActionResult> GetContacts() {
            return Ok(await dbContext.Contacts.ToListAsync());
        }

        //add contact
        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest) {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone
            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }
    }
}
