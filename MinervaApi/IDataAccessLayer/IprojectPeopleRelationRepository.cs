using MinervaApi.Models;

namespace MinervaApi.IDataAccessLayer
{
    public interface IprojectPeopleRelationRepository
    {
        public Task<int> CreateprojectPeopleRelation(projectPeopleRelation? relation);
    }
}
