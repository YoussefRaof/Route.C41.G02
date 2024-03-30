using Route.C4.G02.DAL.Data;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.BLL
{
    public class UnitOfWork : IUniitOfWork 
    {
        private readonly ApplicationDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository { get; set ; } = null;
        public IDepartmentRepository DepartmentRepository { get ; set ; } = null;
         
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            EmployeeRepository =  new EmployeeRepository(_dbContext);
            DepartmentRepository = new DepartmentRepository(_dbContext);
        }



        public int Complete()
        {

          return _dbContext.SaveChanges();   
           
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
