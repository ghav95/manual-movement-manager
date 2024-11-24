using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Manual.Movement.Manager.Domain.ManualHandling
{
    public interface IManualHandlingRepository
    {
        Task AddAsync(
            int month,
            int year,
            string productId,
            string cosifId,
            string description,
            decimal value, 
            string userId,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<ManualHandling>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}