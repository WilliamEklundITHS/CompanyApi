using AutoMapper;
using Company.Common.Dtos.Company;
using Entity.Data.Services;
using Microsoft.AspNetCore.Mvc;
using CompanyModel = Company.Data.Entities.Models.Company;

namespace Company.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _mapper = mapper;
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IResult> GetAllCompanies()
        {
            var result = await _companyService.GetAllEntities<CompanyModel>();
            var response = _mapper.Map<List<CompanyDtoResponse>>(result);
            return Results.Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetCompanyById(int id)
        {
            var result = await _companyService.GetEntityById<CompanyModel>(x => x.Id == id);
            if (result == null) return Results.NotFound();

            var response = _mapper.Map<CompanyDtoResponse>(result);
            return Results.Ok(response);
        }

        [HttpPost]
        public async Task<IResult> CreateCompany(CompanyDtoRequest request)
        {

            var entity = _mapper.Map<CompanyModel>(request);

            var CompanyModelDuplicate = await _companyService.GetEntityById<CompanyModel>(x => x.Name == request.Name);
            if (CompanyModelDuplicate != null) return Results.BadRequest();

            var result = await _companyService.CreateEntity<CompanyModel>(entity);

            var response = _mapper.Map<CompanyDtoResponse>(result);
            return Results.Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<IResult> DeleteCompany(int id)
        {
            var result = await _companyService.DeleteEntity<CompanyModel>(x => x.Id == id);
            if (result == false) return Results.NotFound();
            return Results.NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IResult> UpdateCompany(int id, CompanyDtoRequest request)
        {
            var entityToUpdate = await _companyService.GetEntityById<CompanyModel>(x => x.Id == id);
            if (entityToUpdate == null) return Results.NotFound();

            var result = _mapper.Map(request, entityToUpdate);
            await _companyService.UpdateEntity(result);

            var response = _mapper.Map<CompanyDtoResponse>(result);
            return Results.Ok(response);
        }

    }
}
