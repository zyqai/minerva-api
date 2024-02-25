using Minerva.Models.Returns;
using MinervaApi.Models.Requests;

namespace MinervaApi.BusinessLayer.Interface
{
    public interface IprojectPeopleRelation
    {
        public Task<int> Create(projectPeopleRelationRequest request);
        public Task<ProjectByPeople> GetProjectByPeople(int? projectId);
    }
}
