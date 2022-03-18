using System.Collections.Generic;
using University.Models;

namespace University.Repositories
{
    interface IStudentRepository
    {
        void Add(Student student);
        Student GetById(int id);
        List<Student> GetAll();
    }
}