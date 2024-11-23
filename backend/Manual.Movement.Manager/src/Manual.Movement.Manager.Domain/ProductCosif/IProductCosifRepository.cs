using System.Threading;
using System.Threading.Tasks;

namespace Manual.Movement.Manager.Domain.ProductCosif
{
    public interface IProductCosifRepository
    {
        Task<ProductCosif> UpsertAsync(ProductCosif productCosif, CancellationToken cancellationToken = default);
    }
}
