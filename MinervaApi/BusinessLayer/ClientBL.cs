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
        IUserRepository userRepository;
        public ClientBL(IClientRepository _client,IUserRepository user)
        { 
            client = _client;
            userRepository = user;
        }
        public Task<Client?> GetClient(int ClientId)
        {
            return client.GetClientAsync(ClientId);
        }
        public Task<List<Client?>> GetALLClients()
        {
            return client.GetALLClientsAsync();
        }
        public async Task<int> SaveClient(ClientRequest c)
        {
            var users = await userRepository.GetuserusingUserNameAsync(c.CreatedBy);
            c.CreatedBy = users?.UserId;
            c.UserId = users?.UserId;
            Client us = MappingClient(c);
            return await client.SaveClient(us);
        }
        public async Task<bool> UpdateClient(ClientRequest c)
        {
            var users = await userRepository.GetuserusingUserNameAsync(c.ModifiedBy);
            c.ModifiedBy = users?.UserId;
            Client us = MappingClient(c);
            return await client.UpdateClient(us);
        }
        public Task<bool> DeleteClient(int ClientId)
        {
            return client.DeleteClient(ClientId);
        }
        private Client MappingClient(ClientRequest c)
        {
            Client ct = new Client
            {
                ClientId = c.ClientId,
                UserId = c.UserId,
                TenantId = c.TenantId,
                ClientName = c.ClientName,
                ClientAddress = c.ClientAddress,
                PreferredContact = c.PreferredContact,
                Email = c.Email,
                CreditScore = c.CreditScore,
                LendabilityScore = c.LendabilityScore,
                MarriedStatus = c.MarriedStatus,
                SpouseClientId = c.SpouseClientId,
                RootFolder = c.RootFolder,
                CreatedTime = c.CreatedTime,
                ModifiedTime = c.ModifiedTime,
                ModifiedBy = c.ModifiedBy,
                CreatedBy = c.CreatedBy,
                PhoneNumber = c.PhoneNumber,
                firstName = c.firstName,
                lastName = c.lastName,
                dob = c.dob,
                socialsecuritynumber = c.socialsecuritynumber,
                postalnumber = c.postalnumber,
                stateid = c.stateid,
                City = c.City,
                ClientAddress1 = c.ClientAddress1,
                Gender = c.Gender,
            };
            return ct;
        }
    }
}
