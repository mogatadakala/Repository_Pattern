namespace RepositoryApp.Models
{
    public interface ICustomersRepository
    {
        Task<IEnumerable<Customers>> GetAllCustomers();
        Task<Customers> GetCustomer(int CustomerId);
        Task<Customers> AddCustomer(Customers customer);
        Task<Customers> UpdateCustomer(Customers customer);
        Task<Customers> DeleteCustomer(int CustomerId);
    }
}
