﻿
using Minerva.Models;
using MinervaApi.Models.Requests;

namespace Minerva.BusinessLayer.Interface
{
    public interface ICBRelation
    {
        public Task<bool> Delete(int id);
        public Task<List<CBRelation?>> GetALLAync();
        public Task<CBRelation?> GetAync(int? id);
        public Task<int?> Save(CBRelationRequest ? relation);
        public Task<bool> Update(CBRelationRequest? request);
    }
}