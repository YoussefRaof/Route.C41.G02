using Microsoft.EntityFrameworkCore;
using Route.C4.G02.DAL.Data;
using Route.C4.G02.DAL.Models;
using Route.C41.G02.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Empolyee> , IEmployeeRepository
    {
        //private readonly ApplicationDbContext dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext):base(dbContext)   
        {
            //this.dbContext = dbContext;
        }
        public IQueryable<Empolyee> GetEmployeeByAddress(string Address)
        {
            return _dbContext.Empolyees.Where(E => E.Address.ToLower() == Address.ToLower()); 
        }
    }
}
