using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ReadLater5.ApiControllers
{
    /// <summary>
    /// Access to this widget should be authorized (provide token to external users). This is for demonstration purposes only.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WidgetApiController : ControllerBase
    {
        private IWidgetService _widgetService;

        public WidgetApiController(IWidgetService widgetService)
        {
            _widgetService = widgetService;
        }

        [HttpGet("GetMostRecentBookmarks")]
        public IActionResult GetMostRecentBookmarks([FromQuery]int number, [FromQuery]string username)
        {
            return new JsonResult(_widgetService.GetMostRecentBookmarks(number, username));
        }
    }
}
