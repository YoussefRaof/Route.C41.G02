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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext) // Ask CLR For Creating Object Of "ApplicationDbContext" Impilicity
        {
            _dbContext = dbContext;
            /*_dbContext*/ /*= new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>);*/
        }
        public int Add(Empolyee entity)
        {
            _dbContext.Add(entity);
            return _dbContext.SaveChanges();
        }
        public int Update(Empolyee entity)
        {
            _dbContext.Update(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(Empolyee entity)
        {
            _dbContext.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public Empolyee Get(int id)
        {
            var Empolyee = _dbContext.Empolyees.Find(id);
            ///var Empolyee = _dbContext.Empolyees.Local.Where(D=> D.Id == id).FirstOrDefault();
            ///if(Empolyee is null)
            ///    Empolyee = _dbContext.Empolyees.Where(D => D.Id == id).FirstOrDefault();

            return Empolyee;
        }

        public IEnumerable<Empolyee> GetAll()
            => _dbContext.Empolyees.AsNoTracking().ToList();

    }
}
