using Minerva.BusinessLayer.Interface;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using Minerva.Models.Responce;
using MinervaApi.Models.Requests;

namespace Minerva.BusinessLayer
{
    public class CBRelationBL : ICBRelation
    {
        ICBRelationRepository CBRelationRepository;
        IClientRepository ClientRepository;
        IBusinessRepository businessRepository;
        public CBRelationBL(ICBRelationRepository _cB, IClientRepository _clientRepository, IBusinessRepository _businessRepository) 
        {
            CBRelationRepository = _cB;
            ClientRepository = _clientRepository;
            businessRepository = _businessRepository;
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
        public async Task<BusinessRelation> GetBusinessRelationList(int ?businessId)
        {
            BusinessRelation br = new BusinessRelation();
            br.ClientPersonas = new List<ClientPersonas>();
            br.ClientPersonas = await ClientRepository.GetClientPersonasAsync(businessId);
            br.Business = await businessRepository.GetBussinessAsync(businessId);
            return br;
        }

        public async Task<ClientRelation> GetClientRelationList(int? clientId)
        {
            ClientRelation clientRelation = new ClientRelation();
            clientRelation.Client = await ClientRepository.GetClientAsync(clientId);
            clientRelation.businesses = new List<BusinessPersonas>();
            clientRelation.businesses = await businessRepository.GetBussinessPersonasAsync(clientId);
            return clientRelation;
        }
    }
}
