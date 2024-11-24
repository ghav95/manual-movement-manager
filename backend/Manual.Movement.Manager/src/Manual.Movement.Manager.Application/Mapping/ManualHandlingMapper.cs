using Manual.Movement.Manager.Application.Dto;
using Manual.Movement.Manager.Domain.ManualHandling;
using System.Collections.Generic;
using System.Linq;

namespace Manual.Movement.Manager.Application.Mapping
{
    public static class ManualHandlingMapper
    {
        public static IEnumerable<ManualHandlingDto> ToDto(this IEnumerable<ManualHandling> manualHandlingDtos)
        {
            return manualHandlingDtos.Select(ToDto).ToList();
        }

        public static ManualHandlingDto ToDto(this ManualHandling manualHandling)
        {
            return new ManualHandlingDto(
                manualHandling.Month.ToString(),
                manualHandling.Year.ToString(),
                manualHandling.LaunchNumber.ToString(),
                manualHandling.ProductId,
                manualHandling.CosifId,
                manualHandling.Description,
                manualHandling.Date.ToString(),
                manualHandling.UserId,
                manualHandling.Value.ToString());
        }
    }
}