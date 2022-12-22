using AutoMapper;
using Company.Common.Dtos.Employee;
using Company.Common.Dtos.EmployeeRole;
using Company.Data.DataAccess;
using Company.Data.Entities.Models;
using Entity.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly ICompanyService _dbService;
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public EmployeeController(ICompanyService dbService, IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dbService = dbService;
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IResult> GetAllEmployees()
        {
            var result = await _dbService.GetAllRelationalEntities<Employee, Role>(x => x.Roles);

            if (result == null) return Results.BadRequest();

            var response = _mapper.Map<List<EmployeeDtoResponse>>(result);
            return Results.Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetEmployeeById(int id)
        {
            var result = await _dbService.GetEntityById<Employee>(x => x.Id == id);
            if (result == null) return Results.NotFound();

            var response = _mapper.Map<EmployeeDtoResponse>(result);
            return Results.Ok(response);
        }

        [HttpPost]
        public async Task<IResult> CreateEmployee(EmployeeDtoRequest request)
        {
            var entity = _mapper.Map<Employee>(request);
            var department = await _dbService.GetEntityById<Employee>(x => x.DepartmentId == request.DepartmentId);
            if (department == null) return Results.BadRequest();

            var result = await _dbService.CreateEntity(entity);

            var response = _mapper.Map<EmployeeDtoResponse>(result);
            return Results.Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteEmployee(int id)
        {
            var result = await _dbService.DeleteEntity<Employee>(x => x.Id == id);
            if (result == false) return Results.NotFound();
            return Results.NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IResult> UpdateEmployee(int id, EmployeeDtoRequest request)
        {
            var entityToUpdate = await _dbService.GetEntityById<Employee>(x => x.Id == id);
            if (entityToUpdate == null) return Results.NotFound();

            var result = _mapper.Map(request, entityToUpdate);
            await _dbService.UpdateEntity(result);

            var response = _mapper.Map<EmployeeDtoResponse>(result);
            return Results.Ok(response);
        }

        [HttpPost("EmployeeRole")]
        public async Task<IResult> AddEmployeeRole(EmployeeRoleDtoRequest request)
        {

            var employee = await _dbService.GetRelationalEntityById<Employee, Role>(x => x.Id == request.EmployeeId, x => x.Roles);
            var role = await _dbService.GetEntityById<Role>(x => x.Id == request.RoleId);

            if (employee == null || role == null) return Results.NotFound();
            if (employee.Roles.Contains(role)) return Results.BadRequest();

            employee.Roles.Add(role);
            var response = _mapper.Map<EmployeeDtoResponse>(employee);

            return Results.Ok(response);
        }
    }
}
