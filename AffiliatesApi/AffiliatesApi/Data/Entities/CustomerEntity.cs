using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AffiliatesApi.Data.Entities
{
    public class CustomerEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int AffiliateId { get; set; }
        public AffiliateEntity Affiliate { get; set; }
        public string Name { get; set; }
    }
}
