using AffiliatesApi.Model;

namespace AffiliatesApi.Business.Interfaces
{
    public interface ICustomerService
    {
        public Task<CustomerDTO> Create(CustomerCreateDTO payload);
        Task<ICollection<CustomerDTO>> GetCustomersByAffiliateId(int idAffiliated);
        Task<int?> GetCommisionReport(int idAffiliated);
    }
}
