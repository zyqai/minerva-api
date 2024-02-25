using MinervaApi.BusinessLayer.Interface;
using MinervaApi.IDataAccessLayer;
using MinervaApi.Models.Requests;

namespace MinervaApi.BusinessLayer
{
    public class projectBusinessesRelation : IprojectBusinessesRelation
    {
        IprojectBusinessesRelationRepository repository;
        public projectBusinessesRelation(IprojectBusinessesRelationRepository relationRepository)
        {
            repository = relationRepository;
        }
        public Task<int> Create(projectBusinessesRelationRequest request)
        {
            return repository.CreateProjectBusinessRelation(request);
        }
    }
}
