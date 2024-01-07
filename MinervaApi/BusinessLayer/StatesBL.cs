using Minerva.BusinessLayer.Interface;
using Minerva.DataAccessLayer;
using Minerva.IDataAccessLayer;
using Minerva.Models.Requests;
using Minerva.Models;
using MinervaApi.DataAccessLayer;

namespace Minerva.BusinessLayer
{
    public class StatesBL :IStatesBL
    {
        IStatesRepository Statesrepository;
        public StatesBL(IStatesRepository _repository) 
        {
            Statesrepository = _repository;
        }

        public Task<bool> DeleteStates(int id)
        {
            return Statesrepository.DeleteState(id);
        }

        public Task<List<States?>> GetALLstates()
        {
            return Statesrepository.GetALLStatesAsync();
        }

        public Task<States?> Getstates(int Id)
        {
            return Statesrepository.GetStateAsync(Id);
        }

        public Task<bool> SaveState(StatesRequest request)
        {
            States state = Mapping(request);
            return Statesrepository.SaveState(state);
        }

        public Task<bool> UpdateStates(StatesRequest request)
        {
            States state = Mapping(request);
            return Statesrepository.UpdateState(state);
        }

        private States Mapping(StatesRequest request)
        {
            States s=new States();
            s.id = request.id;
            s.Code=request.Code; 
            s.Name=request.Name;
            return s;
        }

       
    }
}
