using MinervaApi.Models.Requests;

namespace MinervaApi.BusinessLayer.Interface
{
    public interface IprojectPeopleRelation
    {
        public Task<int> Create(projectPeopleRelationRequest request);

    }
}
