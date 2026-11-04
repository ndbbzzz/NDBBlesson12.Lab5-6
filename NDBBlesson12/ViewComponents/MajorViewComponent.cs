using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NDBBlesson12.Data;

namespace NDBBlesson12.ViewComponents
{
    // Tên class = "Major" + "ViewComponent" => gọi bằng Component.InvokeAsync("Major")
    public class MajorViewComponent : ViewComponent
    {
        private readonly SchoolContext db;

        public MajorViewComponent(SchoolContext context)
        {
            db = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var majors = await db.Majors.AsNoTracking().OrderBy(m => m.MajorName).ToListAsync();
            // View nằm ở Views/Shared/Components/Major/RenderMajor.cshtml
            return View("RenderMajor", majors);
        }
    }
}
