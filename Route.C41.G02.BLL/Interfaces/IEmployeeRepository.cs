﻿using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Route.C4.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G02.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Empolyee>
    {
        IQueryable<Empolyee> SearchByName(string name);

        IQueryable<Empolyee> GetEmployeeByAddress(string Address); 
    }
}
