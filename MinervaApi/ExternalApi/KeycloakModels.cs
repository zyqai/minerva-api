using Newtonsoft.Json;

namespace MinervaApi.ExternalApi
{
    public class KeycloakModels
    {
    }
    public class tokenResult
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public int refresh_expires_in { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }

        [JsonProperty("not-before-policy")]
        public int notbeforepolicy { get; set; }
        public string session_state { get; set; }
        public string scope { get; set; }
    }

    public class KeyClient
    {
        public string? id { get; set; }
        public string? username { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? email { get; set; }
        public bool? enabled { get; set; }
        public bool? emailVerified { get; set; }
        public ClientRoles? clientRoles { get; set; }
        public List<object>? realmRoles { get; set; }
        public List<string>? requiredActions { get; set; }
        public long createdTimestamp { get; set; }
        public bool totp { get; set; }
        public List<object> disableableCredentialTypes { get; set; }
        public int notBefore { get; set; }
        public Access access { get; set; }
    }
    public class ClientRoles
    {
        public List<string> roles { get; set; }    
    }
    public class Access
    {
        public bool manageGroupMembership { get; set; }
        public bool view { get; set; }
        public bool mapRoles { get; set; }
        public bool impersonate { get; set; }
        public bool manage { get; set; }
    }

}
