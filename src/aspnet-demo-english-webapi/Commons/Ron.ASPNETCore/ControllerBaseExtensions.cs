﻿using Microsoft.AspNetCore.Mvc;

namespace Ron.ASPNETCore
{
    public static class ControllerBaseExtensions
    {
        public static BadRequestObjectResult APIError(this ControllerBase controller, int code, string message)
        {
            return controller.BadRequest(new APIError(code, message));
        }
    }
}
