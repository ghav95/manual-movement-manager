using System.Threading;
using System.Threading.Tasks;

namespace Manual.Movement.Manager.Domain.Product
{
    public interface IProductRepository
    {
        Task<Product> UpserAsync(Product product, CancellationToken cancellationToken = default);
    }
}