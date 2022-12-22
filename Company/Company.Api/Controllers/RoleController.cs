using AutoMapper;
using Company.Common.Dtos.Role;
using Company.Data.Entities.Models;
using Entity.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        public RoleController(ICompanyService companyService, IMapper mapper)
        {
            _mapper = mapper;
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IResult> GetAllRoles()
        {
            var result = await _companyService.GetAllEntities<Role>();
            var response = _mapper.Map<List<RoleDtoResponse>>(result);
            return Results.Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetRoleById(int id)
        {
            var result = await _companyService.GetEntityById<Role>(x => x.Id == id);
            if (result == null) return Results.NotFound();

            var response = _mapper.Map<RoleDtoResponse>(result);
            return Results.Ok(response);
        }

        [HttpPost]
        public async Task<IResult> CreateRole(RoleDtoRequest request)
        {

            var entity = _mapper.Map<Role>(request);

            var roleDuplicate = await _companyService.GetEntityById<Role>(x => x.Title == request.Title);
            if (roleDuplicate != null) return Results.BadRequest();

            var result = await _companyService.CreateEntity<Role>(entity);

            var response = _mapper.Map<RoleDtoResponse>(result);
            return Results.Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<IResult> DeleteRole(int id)
        {
            var result = await _companyService.DeleteEntity<Role>(x => x.Id == id);
            if (result == false) return Results.NotFound();
            return Results.NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IResult> UpdateRole(int id, RoleDtoRequest request)
        {
            var entityToUpdate = await _companyService.GetEntityById<Role>(x => x.Id == id);
            if (entityToUpdate == null) return Results.NotFound();

            var result = _mapper.Map(request, entityToUpdate);
            await _companyService.UpdateEntity(result);

            var response = _mapper.Map<RoleDtoResponse>(result);
            return Results.Ok(response);
        }

    }
}
