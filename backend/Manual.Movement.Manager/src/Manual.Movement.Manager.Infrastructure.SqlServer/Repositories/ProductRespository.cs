using Manual.Movement.Manager.Domain.Product;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Manual.Movement.Manager.Infrastructure.SqlServer.Repositories
{
    public class ProductRespository : IProductRepository
    {
        private readonly SqlServerDbContext _context;

        public ProductRespository(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> Get(CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<Product> GetById(string productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
        }
    }
}