using Manual.Movement.Manager.Application.Dto;
using System.Collections.Generic;

namespace Manual.Movement.Manager.Application.UseCases.GetAllManualMovement
{
    public class GetAllManualMovementOutput
    {
        public GetAllManualMovementOutput(IEnumerable<ManualHandlingDto> manualHandlings)
        {
            ManualHandlings = manualHandlings;
        }

        public IEnumerable<ManualHandlingDto> ManualHandlings { get; set; }
    }
}