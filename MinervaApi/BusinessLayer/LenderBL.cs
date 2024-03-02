using Minerva.IDataAccessLayer;
using Minerva.Models;
using Minerva.Models.Returns;
using MinervaApi.BusinessLayer.Interface;
using MinervaApi.IDataAccessLayer;
using MinervaApi.Models;
using MinervaApi.Models.Requests;
using System.Data;

namespace MinervaApi.BusinessLayer
{
    public class LenderBL : ILenderBL
    {
        ILenderRepository repository;
        IUserRepository userRepository;
        public LenderBL(ILenderRepository _repositoryIus,IUserRepository user) 
        {
            userRepository = user;
            repository = _repositoryIus;
        }
        public async Task<Apistatus> DeleteLender(int LenderId)
        {
            return await repository.DeleteLender(LenderId);
        }

        public async Task<List<Lender?>> GetALLLenders()
        {
            return await repository.GetALLLenders();
        }

        public async Task<Lender?> GetLender(int LenderId)
        {
            return await repository.GetLender(LenderId);
        }

        public async Task<int> SaveLender(LenderRequest Lender, string userid)
        {
            User ?user=await userRepository.GetuserusingUserNameAsync(userid);
            Lender req = Mapping(Lender);
            req.TenantID = user.TenantId;
            return await repository.SaveLender(req);

        }

        private Lender Mapping(LenderRequest lender)
        {
            Lender la = new Lender 
            {
                LenderID=lender.LenderID,
                TenantID=lender.TenantID,
                Name=lender.Name,
                ContactAddress=lender.ContactAddress,
                PhoneNumber=lender.PhoneNumber,
                Email=lender.Email,
                LicensingDetails=lender.LicensingDetails,
                CommercialMortgageProducts=lender.CommercialMortgageProducts,
                InterestRates=lender.InterestRates,
                Terms=lender.Terms,
                LoanToValueRatio=lender.LoanToValueRatio,
                ApplicationProcessDetails=lender.ApplicationProcessDetails,
                UnderwritingGuidelines=lender.UnderwritingGuidelines,
                ClosingCostsAndFees=lender.ClosingCostsAndFees,
                SpecializedServices=lender.SpecializedServices
            };
            return la;
        }

        public async Task<Apistatus> UpdateLender(LenderRequest Lender)
        {
            Lender lender = Mapping(Lender);
            return await repository.UpdateLendet(lender);
        }
    }
}
