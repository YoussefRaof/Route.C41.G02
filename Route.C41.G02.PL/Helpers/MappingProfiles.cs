using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Route.C4.G02.DAL.Models;
using Route.C41.G02.PL.ViewModels;

namespace Route.C41.G02.PL.Helpers
{
    public class MappingProfiles :Profile
    {

        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Empolyee>().ReverseMap();
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}
