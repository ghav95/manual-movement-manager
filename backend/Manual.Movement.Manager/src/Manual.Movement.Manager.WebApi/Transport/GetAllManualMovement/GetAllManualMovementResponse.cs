using Manual.Movement.Manager.Application.Dto;
using Manual.Movement.Manager.Application.UseCases.GetAllManualMovement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manual.Movement.Manager.WebApi.Transport.GetAllManualMovement
{
    public class GetAllManualMovementResponse
    {
        public IEnumerable<ManualHandlingDto> ManualHandlings { get; set; }
    }

    public static partial class OutputExtensios
    {
        public static GetAllManualMovementResponse MapToResponse(this GetAllManualMovementOutput output)
        {
            return new GetAllManualMovementResponse
            {
                ManualHandlings = output.ManualHandlings
            };
        }
    }
}