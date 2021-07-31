namespace AuthDemo.IdentityServer4.App.Data.Entities
{
    public class ApiResourceScopes
    {
        public int Id { get; set; }
        public string Scope { get; set; }
        public int ApiResourceId { get; set; }
    }
}
