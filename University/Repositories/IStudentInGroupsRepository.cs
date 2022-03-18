using University.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Repositories
{
    interface IStudentInGroupsRepository
    {
        void Add(StudentInGroups studentInGroups);
        List<StudentInGroups> Get(int id);
    }
}