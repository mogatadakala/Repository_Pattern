using Microsoft.EntityFrameworkCore;
using RepositoryApp.Models;
using System;

namespace RepositoryApp.Models
{
    public class CustomersRepository:ICustomersRepository
    {
        private readonly DataContext _context;
        public CustomersRepository(DataContext dataContext) 
        {
            _context = dataContext;
        }
        
        public async Task<IEnumerable<Customers>> GetAllCustomers()
        {
          return   await _context.customers.ToListAsync();
        }
        public async Task<Customers> GetCustomer(int CustomerId)
        {
            return await _context.customers.SingleOrDefaultAsync(x => x.CustomerId == CustomerId);
           
        }
        public async Task<Customers> AddCustomer(Customers customer)
        {
             await _context.customers.AddAsync(customer);
            _context.SaveChanges();
            return customer;

        }
        public async Task<Customers> UpdateCustomer(Customers customers)
        {
          var result=  await _context.customers.FirstOrDefaultAsync(e=> e.CustomerId == customers.CustomerId);
            if (result != null)
            {
                result.Name = customers.Name;
                result.Email = customers.Email;
                result.Location = customers.Location;
                await _context.SaveChangesAsync();
                return result;
            }
            return customers;
        }
        public async Task<Customers> DeleteCustomer(int CustomerId)
        {
            var result = await _context.customers
                .FirstOrDefaultAsync(e => e.CustomerId == CustomerId);
            if (result != null)
            {
                _context.customers.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
       
    }
}
