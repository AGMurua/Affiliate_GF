namespace AffiliatesApi.Model
{
    public class AffiliateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CustomerDTO> Customers { get; set; }

        public AffiliateDTO(string name)
        {
            Name = name;
        }
    }
}
