using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NDBBlesson12.Data;
using NDBBlesson12.Models;

namespace NDBBlesson12.Controllers
{
    public class LearnerController : Controller
    {
        private readonly SchoolContext db;

        public LearnerController(SchoolContext context)
        {
            db = context;
        }

        // 1. Load đồng bộ: /Learner?mid=1  (không có mid => hiển thị tất cả)
        public IActionResult Index(int? mid)
        {
            var query = db.Learners.Include(l => l.Major).AsQueryable();
            if (mid != null && mid != 0)
            {
                query = query.Where(l => l.MajorID == mid);
            }
            return View(query.ToList());
        }

        // 2. Load không đồng bộ (AJAX): trả về PartialView chỉ chứa bảng
        public IActionResult LearnerByMajorID(int mid)
        {
            var query = db.Learners.Include(l => l.Major).AsQueryable();
            if (mid != 0)
            {
                query = query.Where(l => l.MajorID == mid);
            }
            return PartialView("LearnerTable", query.ToList());
        }

        // ---------- Create ----------
        public IActionResult Create()
        {
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View(new Learner());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        // ---------- Edit ----------
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var learner = db.Learners.Find(id);
            if (learner == null) return NotFound();

            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (id != learner.LearnerID) return NotFound();

            if (ModelState.IsValid)
            {
                db.Update(learner);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        // ---------- Delete ----------
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var learner = db.Learners.Include(l => l.Major).FirstOrDefault(l => l.LearnerID == id);
            if (learner == null) return NotFound();
            return View(learner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var learner = db.Learners.Find(id);
            if (learner != null)
            {
                db.Learners.Remove(learner);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
