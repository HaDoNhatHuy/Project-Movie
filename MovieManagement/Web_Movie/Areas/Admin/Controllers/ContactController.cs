using Database_Movie.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly MovieContext _context;
        public ContactController(MovieContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var contact = _context.Contacts.ToList();
            return View(contact);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            var contactModel = new Contact();
            {
                contactModel.ContactId = Guid.NewGuid();
                contactModel.Content = contact.Content;
                contactModel.Status = contact.Status;
                
                _context.Contacts.Add(contactModel);
                await _context.SaveChangesAsync();
                TempData["success"] = "Thêm thành công";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var contactInfo = _context.Contacts.Find(id);
            return View(contactInfo);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Contact contact, Guid id)
        {
            var contactExist = await _context.Contacts.Where(i => i.ContactId == contact.ContactId).FirstOrDefaultAsync();
            if (contactExist == null)
            {
                return NotFound();
            }
            contactExist.Content = contact.Content;
            contactExist.Status = contact.Status;
            
            _context.Contacts.Update(contactExist);
            await _context.SaveChangesAsync();
            TempData["success"] = "Cập nhật thành công";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteContact = _context.Contacts.Find(id);
            if (deleteContact == null)
            {
                return NotFound();
            }
            
            _context.Contacts.Remove(deleteContact);
            await _context.SaveChangesAsync();
            
            TempData["success"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
    }
}
