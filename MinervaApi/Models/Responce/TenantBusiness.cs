using Minerva.Models;

namespace Minerva.Models.Responce
{
    public class TenantBusiness
    {
        public Tenant ?tenant { get; set; }
        public List<Business?> ?business { get; set; }    
    }
    public class PeopleBusiness
    { 
        public Tenant ?tenant { get; set; }
        public List<Client?> ?peoples { get; set; }
    }
    public class TenantUsers
    {
        public Tenant ?tenant { get; set; }
        public List<User?> ?users { get; set; }
    }
    public class ClientPersonas : Client
    {
        public int? clientBusinessId { get; set; }
        public Personas Personas { get; set; }
    }
    public class BusinessPersonas : Business
    {
        public int? clientBusinessId { get; set; }
        public Personas Personas { get; set; }
    }
    public class BusinessRelation 
    {
        public Business Business { get; set; }
        public List<ClientPersonas> ClientPersonas { get; set; }
    }
    public class ClientRelation
    {
        public Client Client{ get; set; }
        public List<BusinessPersonas> businesses { get; set; }
    }
    public class TenantProject
    {
        public Tenant? tenant { get; set; }
        public List<Project>? Projects { get; set; }
    }
}
