using Minerva.BusinessLayer.Interface;
using Minerva.DataAccessLayer;
using Minerva.IDataAccessLayer;
using Minerva.Models;
using Minerva.Models.Requests;

namespace Minerva.BusinessLayer
{
    public class ClientBL :IClientBL
    {
        IClientRepository client;
        public ClientBL(IClientRepository _client)
        { 
            client = _client;
        }
        public Task<Client?> GetClient(int ClientId)
        {
            return client.GetClientAsync(ClientId);
        }
        public Task<List<Client?>> GetALLClients()
        {
            return client.GetALLClientsAsync();
        }
        public Task<int> SaveClient(ClientRequest c)
        {
            Client us = MappingClient(c);
            return client.SaveClient(us);
        }
        public Task<bool> UpdateClient(ClientRequest c)
        {
            Client us = MappingClient(c);
            return client.UpdateClient(us);
        }
        public Task<bool> DeleteClient(int ClientId)
        {
            return client.DeleteClient(ClientId);
        }
        private Client MappingClient(ClientRequest c)
        {
            Client ct = new Client();
            ct.ClientId = c.ClientId;
            ct.UserId= c.UserId;
            ct.TenantId = c.TenantId;
            ct.ClientName  = c.ClientName;
            ct.ClientAddress = c.ClientAddress;
            ct.PreferredContact=   c.PreferredContact;
            ct.Email = c.Email;
            ct.CreditScore = c.CreditScore;
            ct.LendabilityScore = c.LendabilityScore;
            ct.MarriedStatus = c.MarriedStatus;
            ct.SpouseClientId = c.SpouseClientId;
            ct.RootFolder  =    c.RootFolder;
            ct.CreatedTime= c.CreatedTime;
            ct.ModifiedTime= c.ModifiedTime;
            ct.ModifiedBy=c.ModifiedBy;
            ct.CreatedBy=c.CreatedBy;
            ct.PhoneNumber = c.PhoneNumber;
            ct.firstName = c.firstName;
            ct.lastName=c.lastName;
            ct.dob = c.dob;
            ct.socialsecuritynumber=c.socialsecuritynumber;
            ct.postalnumber = c.postalnumber;
            ct.stateid = c.stateid;
            return ct;
        }
    }
}
