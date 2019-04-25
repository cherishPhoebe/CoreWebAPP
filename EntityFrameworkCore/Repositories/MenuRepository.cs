using Domain.Entities;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Repositories
{
    public class MenuRepository : ZYRepositoryBase<Menu>, IMenuRepository
    {
        public MenuRepository(ZYDBContext dbcontext) : base(dbcontext)
        {

        }
    }
}
