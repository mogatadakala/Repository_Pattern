using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RepositoryApp.Models;

namespace RepositoryApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomersRepository _customerrepo;
        private readonly IMemoryCache _memorycache;
        public CustomersController(ICustomersRepository customersRepository,IMemoryCache memoryCache) 
        {
            _customerrepo = customersRepository;
            _memorycache = memoryCache;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IEnumerable<Customers>> getdetails()
        {
            //Cache key Functionality
            var cachekey = "customerslist";
            if(!_memorycache.TryGetValue(cachekey,out IEnumerable<Customers> customersList))
            {
                customersList = await _customerrepo.GetAllCustomers();
                var cacheExpiryoptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(20)
                };
                _memorycache.Set(cachekey, customersList);
            }
            return customersList;
        }
        public async Task<Customers> GetCustomer(int id)
        {
            return await _customerrepo.GetCustomer(id);
        }
        //[HttpDelete("{id:int}")]
        public async Task<ActionResult<Customers>> deletecustomer(int id)
        {
           return await _customerrepo.DeleteCustomer(id);
        }
    }
}
