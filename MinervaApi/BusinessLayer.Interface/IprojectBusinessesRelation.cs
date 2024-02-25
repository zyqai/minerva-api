using MinervaApi.Models.Requests;

namespace MinervaApi.BusinessLayer.Interface
{
    public interface IprojectBusinessesRelation
    {
        public Task<int> Create(projectBusinessesRelationRequest request);

    }
}
