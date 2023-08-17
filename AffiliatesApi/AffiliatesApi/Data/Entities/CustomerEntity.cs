namespace AffiliatesApi.Data.Entities
{
    public class CustomerEntity
    {
        public Guid Id { get; set; }
        public Guid AffiliateId { get; set; }
        public string Name { get; set; }
    }
}
