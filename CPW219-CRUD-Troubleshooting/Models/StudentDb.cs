using CPW219_CRUD_Troubleshooting.Models;
using Microsoft.EntityFrameworkCore;

namespace CPW219_CRUD_Troubleshooting.Models
{
    public static class StudentDb
    {
        public static Student Add(Student p, SchoolContext db)
        {
            //Add student to context
            db.Students.Add(p);
            db.SaveChanges(); // Save changes after adding
            return p;
        }

        public static List<Student> GetStudents(SchoolContext context)
        {
            return context.Students.ToList(); // Directly use ToList() for simplicity
        }

        public static Student? GetStudent(SchoolContext context, int id)
        {
            return context.Students.SingleOrDefault(s => s.StudentId == id); // Use SingleOrDefault to handle non-existent IDs
        }

        public static void Delete(SchoolContext context, Student p)
        {
            context.Students.Remove(p);
            context.SaveChanges(); // Save changes after removing
        }

        public static void Update(SchoolContext context, Student p)
        {
            //Mark the object as deleted
            context.Students.Update(p);

            //Send delete query to database
            context.SaveChanges(); // Save changes after updating
        }
    }
}
