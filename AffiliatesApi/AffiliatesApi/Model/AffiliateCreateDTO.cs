using System.ComponentModel.DataAnnotations;

namespace AffiliatesApi.Model
{
    public class AffiliateCreateDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
