using AffiliatesApi.Model;

namespace AffiliatesApi.Business.Interfaces
{
    public interface ICustomerService
    {
        public Task<int> Create(CustomerCreateDTO payload);
        Task<List<CustomerDTO>> GetCustomersByAffiliateId(int idAffiliated);
    }
}
