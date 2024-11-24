using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Manual.Movement.Manager.Domain.Product
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> Get(CancellationToken cancellationToken = default);
        Task<Product> GetById(string productId, CancellationToken cancellationToken = default);
    }
}