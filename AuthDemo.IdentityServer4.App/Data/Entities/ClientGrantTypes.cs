namespace AuthDemo.IdentityServer4.App.Data.Entities
{
    public class ClientGrantTypes
    {
        public int Id { get; set; }
        public string GrantType { get; set; }
        public int ClientId { get; set; }
    }
}
