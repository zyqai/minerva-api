using MinervaApi.BusinessLayer.Interface;
using MinervaApi.IDataAccessLayer;
using MinervaApi.Models;
using MinervaApi.Models.Requests;

namespace MinervaApi.BusinessLayer
{
    public class projectPeopleRelationBL : IprojectPeopleRelation
    {
        IprojectPeopleRelationRepository repository;
        public projectPeopleRelationBL( IprojectPeopleRelationRepository iprojectPeople)
        {
            repository = iprojectPeople;
        }
        public Task<int> Create(projectPeopleRelationRequest request)
        {
            projectPeopleRelation? relation = new projectPeopleRelation();
            relation = mapRequest(request);
            return repository.CreateprojectPeopleRelation(relation);
        }

        private projectPeopleRelation mapRequest(projectPeopleRelationRequest request)
        {
            projectPeopleRelation res = new projectPeopleRelation { 
             projectPeopleId = request.projectPeopleId,
             tenantId = request.tenantId,
             projectId = request.projectId,
             peopleId=request.peopleId,
             primaryBorrower=request.primaryBorrower,
             personaAutoId=request.personaAutoId,
            };
            return res;
        }
    }
}
