using Minerva.BusinessLayer.Interface;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using MinervaApi.Models.Requests;

namespace Minerva.BusinessLayer
{
    public class CBRelationBL : ICBRelation
    {
        ICBRelationRepository CBRelationRepository;
        public CBRelationBL(ICBRelationRepository _cB) 
        {
            CBRelationRepository = _cB;
        }

        public Task<bool> Delete(int id)
        {
            return CBRelationRepository.Delete(id);
        }

        public Task<List<CBRelation?>> GetALLAync()
        {
            return CBRelationRepository.GelAllAync();
        }

        public Task<CBRelation?> GetAync(int? id)
        {
            return CBRelationRepository.GetAync(id);
        }
        public Task<int?> Save(CBRelationRequest? relation)
        {
            CBRelation Relation = Mapping(relation);
            return CBRelationRepository.Save(Relation);
        }

        public Task<bool> Update(CBRelationRequest? request)
        {
            CBRelation Relation = Mapping(request);
            return CBRelationRepository.Update(Relation);
        }

        private CBRelation Mapping(CBRelationRequest? relation)
        {
            CBRelation cB = new CBRelation {
                businessId = relation.businessId,
                clientBusinessId=relation.clientBusinessId, 
                clientId=relation.clientId,
                personaId = relation.personaId  
            };
            return cB;
        }
    }
}
