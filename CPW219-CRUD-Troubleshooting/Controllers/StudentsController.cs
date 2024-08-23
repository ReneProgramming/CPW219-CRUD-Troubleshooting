using CPW219_CRUD_Troubleshooting.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_CRUD_Troubleshooting.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext context;

        public StudentsController(SchoolContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Student> students = StudentDb.GetStudents(context);
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student p)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Add(p, context);
                context.SaveChanges(); // Ensure changes are saved to the database
                ViewData["Message"] = $"{p.Name} was added!";
                return RedirectToAction("Index");
            }
            //Show web page with errors
            return View(p);
        }

        public IActionResult Edit(int id)
        {
            //get the product by id
            Student? p = StudentDb.GetStudent(context, id);
            if (p == null)
            {
                return NotFound();
            }
            //show it on web page
            return View(p);
        }

        [HttpPost]
        public IActionResult Edit(Student p)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Update(context, p);
                context.SaveChanges(); // Ensure changes are saved to the database
                ViewData["Message"] = "Student Updated!";
                return RedirectToAction("Index");
            }
            //return view with errors
            return View(p);
        }

        public IActionResult Delete(int id)
        {
            Student p = StudentDb.GetStudent(context, id);
            if (p == null)
            {
                return NotFound();
            }
            return View(p);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            Student? student = StudentDb.GetStudent(context, id);
            if (student == null)
            {
                return NotFound(); // If student not found, return 404
            }

            StudentDb.Delete(context, student);
            return RedirectToAction("Index"); // Redirect to Index after deletion
        }

    }
}
