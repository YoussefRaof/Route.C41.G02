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
    public class GenericRepository<T>: IGenericRepository<T> where T : ModelBase 
    {
        private protected  readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
            /*_dbContext*/ /*= new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>);*/
        }
        public void Add(T entity)
           => _dbContext.Set<T>().Add(entity);
        
           
        
        public void Update(T entity)
      

           => _dbContext.Update(entity);// EF Core 3.1 Feature
      

        public void Delete(T entity)
        
           => _dbContext.Remove(entity);
         
        

        public async Task<T> GetAsync(int id)
        {
            ///var Empolyee = _dbContext.Empolyees.Local.Where(D=> D.Id == id).FirstOrDefault();
            ///if(Empolyee is null)
            ///    Empolyee = _dbContext.Empolyees.Where(D => D.Id == id).FirstOrDefault();

            return await _dbContext.FindAsync<T>(id); ;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Empolyee))
            {
               return (IEnumerable<T>) await _dbContext.Empolyees.Include(E => E.Department).AsNoTracking().ToListAsync();
            }
            else
            {
                return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
            }
        }
            

    }
}
