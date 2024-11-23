using System.Threading;
using System.Threading.Tasks;

namespace Manual.Movement.Manager.Domain.ManualHandling
{
    public interface IManualHandlingRepository
    {
        Task<ManualHandling> UpsertAsync(ManualHandling manualHandling, CancellationToken cancellationToken = default);
    }
}
