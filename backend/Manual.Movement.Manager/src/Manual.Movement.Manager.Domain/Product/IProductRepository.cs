using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Manual.Movement.Manager.Domain.Product
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}