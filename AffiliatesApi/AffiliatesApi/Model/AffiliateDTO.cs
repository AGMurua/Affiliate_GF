namespace AffiliatesApi.Model
{
    public class AffiliateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<CustomerDTO> Customers { get; set; }
    }
}
