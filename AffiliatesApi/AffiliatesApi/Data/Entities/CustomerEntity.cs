using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AffiliatesApi.Data.Entities
{
    public class CustomerEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AffiliateId { get; set; }
        public virtual AffiliateEntity Affiliate { get; set; }
        public string Name { get; set; }
    }
}
