using Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Security.Claims;

namespace ReadLater5.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BookmarksApiController : ControllerBase
    {

        private IBookmarkService _bookmarkService;

        public BookmarksApiController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [HttpGet("GetBookmarks")]
        public IActionResult GetBookmarks()
        {
            string username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }

            return new JsonResult(_bookmarkService.GetBookmarks(username));
        }

        [HttpGet("GetBookmark")]
        public IActionResult GetBookmark(int id)
        {
            string username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }

            return new JsonResult(_bookmarkService.GetBookmark(id, username));
        }

        [HttpPost("CreateBookmark")]
        public IActionResult CreateBookmark([FromBody] Bookmark bookmark)
        {
            string username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }

            //Server side validation

            bookmark.Author = username;
            bookmark.CreateDate = System.DateTime.UtcNow;
            _bookmarkService.CreateBookmark(bookmark);

            return Ok();
        }

        [HttpPut("UpdateBookmark")]
        public IActionResult UpdateBookmark([FromBody] Bookmark bookmark)
        {
            if (bookmark.ID == 0)
            {
                return BadRequest();
            }

            string username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }

            var bk = _bookmarkService.GetBookmarkAsNoTracking(bookmark.ID, username);
            if (bk == null)
            {
                return NotFound();
            }

            //Server side validation
            bookmark.Author = bk.Author;
            _bookmarkService.UpdateBookmark(bookmark);

            return Ok();
        }

        [HttpDelete("DeleteBookmark")]
        public IActionResult DeleteBookmark([FromQuery] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            string username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }

            var bookmark = _bookmarkService.GetBookmarkAsNoTracking(id, username);
            if (bookmark == null)
            {
                return NotFound();
            }

            _bookmarkService.DeleteBookmark(bookmark);

            return Ok();
        }
    }
}
