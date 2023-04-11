using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JsonDemo.Models;
using MDB.Models;

namespace JsonDemo.Controllers
{
    public class CoursesController : Controller
    {
        public ActionResult Index()
        {
            return View(DB.Courses.ToList().OrderBy(m => m.Code));
        }
        public ActionResult Details(int id)
        {
            return View(DB.Courses.Get(id));
        }
        public ActionResult Create()
        {
            return View(new Course());
        }
        [HttpPost]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                DB.Courses.Add(course);
                return RedirectToAction("Index");
            }
            return View(course);
        }
        public ActionResult Edit(int id)
        {
            Course course = DB.Courses.Get(id);
            if (course != null)
            {
                ViewBag.Registrations = SelectListUtilities<Student>.Convert(course.Students, "Caption");
                ViewBag.Students = SelectListUtilities<Student>.Convert(DB.Students.ToList(), "Caption");
                return View(DB.Courses.Get(id));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Course course, List<int> SelectedStudentsId)
        {
            if (ModelState.IsValid)
            {
                DB.Courses.Update(course, SelectedStudentsId);
                return RedirectToAction("Details", new { id = course.Id });
            }
            ViewBag.Registrations = SelectListUtilities<Student>.Convert(course.Students, "Caption");
            ViewBag.Students = SelectListUtilities<Student>.Convert(DB.Students.ToList(), "Caption");
            return View(course);
        }
        public ActionResult Delete(int id)
        {
            DB.Courses.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
