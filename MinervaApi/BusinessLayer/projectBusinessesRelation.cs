using Minerva.IDataAccessLayer;
using Minerva.Models.Returns;
using MinervaApi.BusinessLayer.Interface;
using MinervaApi.DataAccessLayer;
using MinervaApi.IDataAccessLayer;
using MinervaApi.Models.Requests;

namespace MinervaApi.BusinessLayer
{
    public class projectBusinessesRelation : IprojectBusinessesRelation
    {
        IprojectBusinessesRelationRepository repository;
        IProjectRepository projectRepository;
        public projectBusinessesRelation(IprojectBusinessesRelationRepository relationRepository, IProjectRepository prorepository)
        {
            repository = relationRepository;
            projectRepository = prorepository;
        }
        public Task<int> Create(projectBusinessesRelationRequest request)
        {
            return repository.CreateProjectBusinessRelation(request);
        }

        public async Task<ProjectByBusiness> GetProjectByBusiness(int? projectId)
        {
            ProjectByBusiness res = new ProjectByBusiness();
            res.Project = await projectRepository.GetProjectAsync(projectId);
            res.BusinessRelation = new List<BusinessesByProject>();
            res.BusinessRelation = await repository.GetProjectByBusinessRelation(projectId);
            if (res.BusinessRelation != null && res.Project != null)
            {
                res.code = "206";
                res.message = "responce avilabule";
            }
            else
            {
                res.code = "204";
                res.message = "no content";
            }
                return res;
        }
    }
}
