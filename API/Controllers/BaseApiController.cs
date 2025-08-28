/// <summary>
/// Base controller for all API endpoints.
/// Applies [ApiController] and [Route] attributes to child controllers.
/// Ensures consistent routing and API conventions.
/// </summary>
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController] // Enables API-specific features (model validation, error responses)
    [Route("api/[controller]")] // Sets base route for all child controllers
    public class BaseApiController : ControllerBase
    {
    }
}
