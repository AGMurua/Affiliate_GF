using AffiliatesApi.Model;

namespace AffiliatesApi.Business.Interfaces
{
    public interface ICustomerService
    {
        public Task<int> Create(CustomerCreateDTO payload);
        Task<ICollection<CustomerDTO>> GetCustomersByAffiliateId(int idAffiliated);
        Task<int> GetCommisionReport(int idAffiliated);
    }
}
