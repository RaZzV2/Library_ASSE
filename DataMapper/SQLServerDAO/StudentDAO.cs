using Library.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SQLServerDAO
{
    internal class StudentDAO : IDAO<Student>
    {
        public void Add(Student t)
        {
            using (var context = new LibraryContext())
            {
                context.Students.Add(t);
                context.SaveChanges();
            }
        }

        public void Delete(Student t)
        {
            using (var context = new LibraryContext())
            {
                var newStudent = new Student { Id = t.Id };
                context.Students.Attach(newStudent);
                context.Students.Remove(newStudent);
                context.SaveChanges();
            }
        }

        public Student GetById(int id)
        {
            using (var context = new LibraryContext())
            {
                return context.Students.Where(student => student.Id == id).SingleOrDefault();
            }
        }

        public void Update(Student t)
        {
            using (var context = new LibraryContext())
            {
                var existingStudent = context.Students.Find(t.Id);

                if (existingStudent != null)
                {
                    context.Entry(existingStudent).CurrentValues.SetValues(t);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Student with ID {t.Id} not found");
                }
            }
        }
    }
}