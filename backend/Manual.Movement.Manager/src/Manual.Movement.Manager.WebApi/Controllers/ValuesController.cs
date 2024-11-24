using Manual.Movement.Manager.Domain.Product;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Manual.Movement.Manager.WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IProductRepository _productRepository;

        public ValuesController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET api/values
        public async Task<IEnumerable<Product>> Get()
        {
            return await _productRepository.GetAllAsync().ConfigureAwait(false);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
