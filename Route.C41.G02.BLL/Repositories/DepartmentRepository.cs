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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext) // Ask CLR For Creating Object Of "ApplicationDbContext" Impilicity
        {
            _dbContext = dbContext;
            /*_dbContext*/ /*= new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>);*/
        }
        public int Add(Department entity)
        {
            _dbContext.Add(entity);
            return _dbContext.SaveChanges();
        }
        public int Update(Department entity)
        {
            _dbContext.Update(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(Department entity)
        {
            _dbContext.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public Department Get(int id)
        {
            var department = _dbContext.Departments.Find(id);
            ///var department = _dbContext.Departments.Local.Where(D=> D.Id == id).FirstOrDefault();
            ///if(department is null)
            ///    department = _dbContext.Departments.Where(D => D.Id == id).FirstOrDefault();

            return department;
        }

        public IEnumerable<Department> GetAll()
            =>_dbContext.Departments.AsNoTracking().ToList();
        
        

    }
}
