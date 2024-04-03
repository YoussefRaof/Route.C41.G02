﻿using Route.C4.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.BLL.Interfaces
{
    public interface IUniitOfWork:IAsyncDisposable
    {
        //public IEmployeeRepository EmployeeRepository { get; set; }
        //public IDepartmentRepository DepartmentRepository{ get; set; }

        IGenericRepository<T> Repository<T>() where T : ModelBase;

        Task<int> Complete();

    }
}
