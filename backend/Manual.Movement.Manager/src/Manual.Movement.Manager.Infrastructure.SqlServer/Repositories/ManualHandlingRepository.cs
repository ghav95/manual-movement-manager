using Manual.Movement.Manager.Domain.ManualHandling;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Manual.Movement.Manager.Infrastructure.SqlServer.Repositories
{
    public class ManualHandlingRepository : IManualHandlingRepository
    {
        private readonly SqlServerDbContext _context;

        public ManualHandlingRepository(SqlServerDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));
        
        public async Task AddAsync(
            int month, 
            int year,
            string productId,
            string cosifId,
            string description,
            decimal value,
            string userId,
            CancellationToken cancellationToken = default)
        {
            var launchNumber = await GetNextLaunchNumberAsync(month, year, cancellationToken)
                .ConfigureAwait(false);

            var manualHandling = new ManualHandling(
                month, 
                year, 
                launchNumber, 
                productId, 
                cosifId, 
                description, 
                DateTime.UtcNow, 
                userId, 
                value);

            _context.ManualHandlings.Add(manualHandling);

            await _context.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<ManualHandling>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ManualHandlings
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        private async Task<long> GetNextLaunchNumberAsync(int month, int year, CancellationToken cancellationToken)
        {
            var sqlQuery = @"
                        SELECT ISNULL(
                            (SELECT MAX(NUM_LANCAMENTO) + 1
                                FROM MOVIMENTO_MANUAL
                                WHERE DAT_MES = {0}
                                AND DAT_ANO = {1}),
                            1) AS NUM_LANCAMENTO";

            var nextLaunchNumber = await _context
                .Database
                .SqlQuery<long>(sqlQuery, month, year)
                .FirstAsync(cancellationToken)
                .ConfigureAwait(false);

            return nextLaunchNumber;
        }     
    }
}