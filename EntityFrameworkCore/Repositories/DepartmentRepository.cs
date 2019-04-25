using Domain.Entities;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Repositories
{
    public class DepartmentRepository : ZYRepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ZYDBContext dbcontext) : base(dbcontext)
        {

        }
    }
}
