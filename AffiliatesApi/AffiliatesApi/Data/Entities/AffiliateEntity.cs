namespace AffiliatesApi.Data.Entities
{
    public class AffiliateEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<CustomerEntity> Customers { get; set; }
    }
}
