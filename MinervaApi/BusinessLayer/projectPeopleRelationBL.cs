using Minerva.IDataAccessLayer;
using Minerva.Models.Returns;
using MinervaApi.BusinessLayer.Interface;
using MinervaApi.IDataAccessLayer;
using MinervaApi.Models;
using MinervaApi.Models.Requests;

namespace MinervaApi.BusinessLayer
{
    public class projectPeopleRelationBL : IprojectPeopleRelation
    {
        IprojectPeopleRelationRepository repository;
        IProjectRepository projectRepository;
        IClientRepository clientRepository;
        public projectPeopleRelationBL( IprojectPeopleRelationRepository iprojectPeople,IProjectRepository _pro,IClientRepository client)
        {
            repository = iprojectPeople;
            projectRepository = _pro;
            clientRepository = client;
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
        public async Task<ProjectByPeople> GetProjectByPeople(int? projectId)
        {
            ProjectByPeople res = new ProjectByPeople();
            res.Project = await projectRepository.GetProjectAsync(projectId);
            res.People = await repository.GetPeopleByProjectId(projectId);
            if (res.People != null && res.Project != null)
            {
                res.code = "206";
                res.message = "response available ";
            }
            else
            {
                res.code = "204";
                res.message = "no content";
            }
            return res;
        }

        public Task<bool> DeleteProjectPeopleRelation(int id)
        {
            return repository.DeleteProjectPeopleRelation(id);
        }
    }
}
