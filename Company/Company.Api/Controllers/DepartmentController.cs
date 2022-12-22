using AutoMapper;
using Company.Common.Dtos.Department;
using Company.Data.Entities.Models;
using Entity.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        public DepartmentController(ICompanyService companyService, IMapper mapper)
        {
            _mapper = mapper;
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IResult> GetAllDepartments()
        {
            var result = await _companyService.GetAllEntities<Department>();
            var response = _mapper.Map<List<DepartmentDtoResponse>>(result);
            return Results.Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetDepartmentById(int id)
        {
            var result = await _companyService.GetEntityById<Department>(x => x.Id == id);
            if (result == null) return Results.NotFound();

            var response = _mapper.Map<DepartmentDtoResponse>(result);
            return Results.Ok(response);
        }

        [HttpPost]
        public async Task<IResult> CreateDepartment(DepartmentDtoRequest request)
        {

            var entity = _mapper.Map<Department>(request);

            var DepartmentDuplicate = await _companyService.GetEntityById<Department>(x => x.Name == request.Name);
            if (DepartmentDuplicate != null) return Results.BadRequest();

            var result = await _companyService.CreateEntity<Department>(entity);

            var response = _mapper.Map<DepartmentDtoResponse>(result);
            return Results.Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<IResult> DeleteDepartment(int id)
        {
            var result = await _companyService.DeleteEntity<Department>(x => x.Id == id);
            if (result == false) return Results.NotFound();
            return Results.NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IResult> UpdateDepartment(int id, DepartmentDtoRequest request)
        {
            var entityToUpdate = await _companyService.GetEntityById<Department>(x => x.Id == id);
            if (entityToUpdate == null) return Results.NotFound();

            var result = _mapper.Map(request, entityToUpdate);
            await _companyService.UpdateEntity(result);

            var response = _mapper.Map<DepartmentDtoResponse>(result);
            return Results.Ok(response);
        }

    }
}
