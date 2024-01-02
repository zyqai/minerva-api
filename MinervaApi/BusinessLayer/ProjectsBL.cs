using Minerva.BusinessLayer.Interface;
using Minerva.DataAccessLayer;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using Minerva.Models.Requests;
using MinervaApi.DataAccessLayer;

namespace Minerva.BusinessLayer
{
    public class ProjectsBL : IProjectsBL
    {
        IProjectRepository PorjectRepository;
        public ProjectsBL(IProjectRepository _repository)
        {
            PorjectRepository = _repository;
        }
        public Task<Project?> GetProjects(int Id_Projects)
        {
            return PorjectRepository.GetProjectAsync(Id_Projects);
        }

        public Task<bool> SaveProject(ProjectRequest request)
        {
            Project project = Mapping(request);
            return PorjectRepository.SaveProject(project);
        }

        public Task<List<Project?>> GetAllProjects()
        {
            return PorjectRepository.GetAllProjectAsync();
        }
        private Project Mapping(ProjectRequest request)
        {
            Project p = new Project();
            p.Id_Projects=request.Id_Projects;
            p.Filename=request.Filename;
            p.Loanamount=request.Loanamount;
            p.Assignrdstaff=request.Assignrdstaff;
            p.Filedescription=request.Filedescription;
            p.Staffnote=request.Staffnote;
            p.Primaryborrower=request.Primaryborrower;
            p.Primarybusiness=request.Primarybusiness;  
            p.Startdate=request.Startdate;
            p.Desiredclosingdate=request.Desiredclosingdate;    
            p.Initialphase=request.Initialphase;
            return p;
        }
        public Task<bool> UpdateProject(ProjectRequest request)
        {
            Project project = Mapping(request);
            return PorjectRepository.UpdateProject(project);
        }
        public Task<bool> DeleteProject(int id)
        { 
            return PorjectRepository.DeleteProject(id);
        }
    }
}
