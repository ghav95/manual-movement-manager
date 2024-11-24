using Manual.Movement.Manager.Domain.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Manual.Movement.Manager.Infrastructure.SqlServer.Repositories
{
    public class ProductRespository : IProductRepository
    {
        private readonly SqlServerDbContext _context;

        public ProductRespository(SqlServerDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Include(p => p.ProductCosifs)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}