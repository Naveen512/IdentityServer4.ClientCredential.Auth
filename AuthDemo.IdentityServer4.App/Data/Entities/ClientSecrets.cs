namespace AuthDemo.IdentityServer4.App.Data.Entities
{
    public class ClientSecrets
    {
        public int Id { get; set; }
        public string Secrets { get; set; }
        public int ClientId { get; set; }
    }
}
