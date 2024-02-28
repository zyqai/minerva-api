using Minerva.Models;
using Minerva.Models.Returns;
using MinervaApi.Models;

namespace MinervaApi.IDataAccessLayer
{
    public interface IprojectPeopleRelationRepository
    {
        public Task<int> CreateprojectPeopleRelation(projectPeopleRelation? relation);
        public Task<List<Peoplesbyproject>> GetPeopleByProjectId(int? projectId);
        public Task<List<ResponceprojectPeopleRelation?>?> GetPeopleDetailsByProjectId(int? projectId);
    }
}
