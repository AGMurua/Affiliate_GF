using System.ComponentModel.DataAnnotations;

namespace AffiliatesApi.Model
{
    public class CustomerCreateDTO
    {
        [Required]
        public int AffiliateId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
