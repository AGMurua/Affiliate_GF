namespace AffiliatesApi.Model
{
    public class AffiliateWithRelationsDTO : AffiliateDTO
    {
        public ICollection<CustomerDTO> Customers { get; set; }

    }
}
