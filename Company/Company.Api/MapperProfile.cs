using AutoMapper;
using Company.Common.Dtos.Company;
using Company.Common.Dtos.Department;
using Company.Common.Dtos.Employee;
using Company.Common.Dtos.EmployeeRole;
using Company.Common.Dtos.Role;
using Company.Data.Entities.Models;
using CompanyModel = Company.Data.Entities.Models.Company;

namespace Company.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {



            CreateMap<CompanyDtoRequest, CompanyModel>();
            CreateMap<CompanyDtoRequest, CompanyDtoResponse>();
            CreateMap<CompanyModel, CompanyDtoResponse>();


            CreateMap<EmployeeRoleDtoRequest, EmployeeRoleDtoResponse>();
            CreateMap<EmployeeRoleDtoRequest, Employee>();


            CreateMap<DepartmentDtoRequest, Department>();
            CreateMap<Department, DepartmentDtoResponse>();

            CreateMap<EmployeeDtoRequest, Employee>();
            CreateMap<Employee, EmployeeDtoResponse>();
            CreateMap<Employee, EmployeeDtoRequest>();
            CreateMap<Employee, EmployeeRoleDtoResponse>();


            CreateMap<RoleDtoRequest, Role>();
            CreateMap<Role, RoleDtoResponse>();
            CreateMap<RoleDtoResponse, Role>();





        }
    }
}
