using Route.C4.G02.DAL.Data;
using Route.C4.G02.DAL.Models;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.BLL
{
    public class UnitOfWork : IUniitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        //public IEmployeeRepository EmployeeRepository { get; set ; } = null;
        //public IDepartmentRepository DepartmentRepository { get ; set ; } = null;
        private Hashtable _repostories;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            //EmployeeRepository =  new EmployeeRepository(_dbContext);
            //DepartmentRepository = new DepartmentRepository(_dbContext);
            _repostories = new Hashtable();
        }



        public IGenericRepository<T> Repository<T>() where T : ModelBase
        {
            var key = typeof(T).Name;
            if (!_repostories.ContainsKey(key))
            {
              
                if (key == nameof(Empolyee))
                {

                    var repository = new EmployeeRepository(_dbContext);
                    _repostories.Add(key,repository);
                }
                else
                {
                    var repository = new GenericRepository<T>(_dbContext);
                    _repostories.Add(key,repository);


                }


            }
            return _repostories[key] as IGenericRepository<T>;
        }


        public async Task<int> Complete()
        {

            return await _dbContext.SaveChangesAsync();

        }

        public async ValueTask DisposeAsync()
        {
           await _dbContext.DisposeAsync();
        }

    }
}
