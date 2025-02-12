using Database_Movie.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GroupController : Controller
    {
        private readonly MovieContext _context;
        public GroupController(MovieContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var group = _context.Groups.ToList();
            return View(group);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Group group)
        {
            var groupModel = new Group()
            {
                GroupId = Guid.NewGuid(),
                Name = group.Name
            };
            _context.Groups.Add(groupModel);
            await _context.SaveChangesAsync();
            TempData["success"] = "Thêm nhóm quyền thành công";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var groupInfo = _context.Groups.Find(id);
            return View(groupInfo);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Group group)
        {
            var groupExist = await _context.Groups.Where(i => i.GroupId == group.GroupId).FirstOrDefaultAsync();
            if (groupExist == null)
            {
                return NotFound();
            }
            groupExist.Name = group.Name;
            _context.Groups.Update(groupExist);
            await _context.SaveChangesAsync();
            TempData["success"] = "Cập nhật nhóm quyền thành công";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteItem = await _context.Groups.FindAsync(id);
            if (deleteItem == null)
            {
                return BadRequest(error: "Lỗi trong khi xóa nhóm quyền");
            }
            _context.Groups.Remove(deleteItem);
            await _context.SaveChangesAsync();
            TempData["success"] = "Xóa nhóm quyền thành công";
            return RedirectToAction("Index");
        }
    }
}
