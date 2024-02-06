using Minerva.Models;

namespace Minerva.Models.Responce
{
    public class TenantBusiness
    {
        public Tenant tenant { get; set; }
        public List<Business> business { get; set; }    
    }
    public class PeopleBusiness
    { 
        public Tenant tenant { get; set; }
        public List<Client> peoples { get; set; }
    }
}
