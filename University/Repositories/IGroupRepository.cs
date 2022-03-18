using System.Collections.Generic;
using University.Models;

namespace University.Repositories
{
    interface IGroupRepository
    {
        void Add(Group group);
        Group GetById(int id);
        List<Group> GetAll();
    }
}