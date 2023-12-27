﻿using Minerva.BusinessLayer.Interface;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using Minerva.Models.Requests;

namespace Minerva.BusinessLayer
{
    public class BusinessBL : IBusinessBL
    {
        IBusinessRepository BusinessRepository;
        public BusinessBL(IBusinessRepository _repository)
        {
            BusinessRepository = _repository;
        }

        public bool SaveBusines(BusinessRequest request)
        {
            Business business = Mapping(request);
            return BusinessRepository.SaveBusiness(business);
        }
        private Business Mapping(BusinessRequest br)
        {
            Business business = new Business();
            business.BusinessId = br.BusinessId;    
            business.BusinessName = br.BusinessName;    
            business.TenantId = br.TenantId;
            business.AnnualRevenue  = br.AnnualRevenue;
            business.BusinessAddress= br.BusinessAddress;
            business.BusinessType  = br.BusinessType;
            business.Industry = br.Industry;
            business.AnnualRevenue =br.AnnualRevenue;
            business.IncorporationDate=br.IncorporationDate;
            business.BusinessRegistrationNumber= br.BusinessRegistrationNumber;
            business.RootDocumentFolder=br.RootDocumentFolder;
            return business;
        }
    }
}
