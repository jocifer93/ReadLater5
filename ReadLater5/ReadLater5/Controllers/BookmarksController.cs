using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Services;
using System.Linq;

namespace ReadLater5.Controllers
{
    [Authorize]
    public class BookmarksController : Controller
    {
        private IBookmarkService _bookmarkService;
        private ICategoryService _categoryService;

        public BookmarksController(IBookmarkService bookmarkService, ICategoryService categoryService)
        {
            _bookmarkService = bookmarkService;
            _categoryService = categoryService;
        }
        // GET: Bookmarks
        public IActionResult Index()
        {
            List<Bookmark> model = _bookmarkService.GetBookmarks(User.Identity.Name);
            return View(model);
        }

        // GET: Bookmarks/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) { return BadRequest(); }

            Bookmark bookmark = _bookmarkService.GetBookmark((int)id, User.Identity.Name);
            if (bookmark == null) { return NotFound(); }

            return View(bookmark);
        }

        // GET: Bookmarks/Create
        public IActionResult Create()
        {
            var availableCategories = _categoryService.GetCategories(User.Identity.Name);
            ViewData["Categories"] = new SelectList(availableCategories, "ID", "Name");

            return View();
        }

        // POST: Bookmarks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("URL,ShortDescription,CategoryId")] Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                bookmark.Author = User.Identity.Name;
                bookmark.CreateDate = DateTime.UtcNow;
                _bookmarkService.CreateBookmark(bookmark);

                return RedirectToAction(nameof(Index));
            }

            var availableCategories = _categoryService.GetCategories(User.Identity.Name);
            ViewData["Categories"] = new SelectList(availableCategories, "ID", "Name", bookmark.CategoryId);

            return View(bookmark);
        }

        // GET: Bookmarks/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) { return BadRequest(); }

            var bookmark = _bookmarkService.GetBookmark((int)id, User.Identity.Name);
            if (bookmark == null) { return NotFound(); }


            var availableCategories = _categoryService.GetCategories(User.Identity.Name);
            ViewData["Categories"] = new SelectList(availableCategories, "ID", "Name", bookmark.CategoryId);

            return View(bookmark);
        }

        // POST: Bookmarks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("ID,URL,ShortDescription,CategoryId,CreateDate")] Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                var bm = _bookmarkService.GetBookmarkAsNoTracking(bookmark.ID, User.Identity.Name);
                if (bm == null) { return NotFound(); }

                bookmark.Author = User.Identity.Name;
                _bookmarkService.UpdateBookmark(bookmark);

                return RedirectToAction(nameof(Index));
            }

            var availableCategories = _categoryService.GetCategories(User.Identity.Name);
            ViewData["Categories"] = new SelectList(availableCategories, "ID", "Name", bookmark.CategoryId);

            return View(bookmark);
        }

        // GET: Bookmarks/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) { return BadRequest(); }

            var bookmark = _bookmarkService.GetBookmark((int)id, User.Identity.Name);
            if (bookmark == null) { return NotFound(); }

            return View(bookmark);
        }

        // POST: Bookmarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id == 0) { return BadRequest(); }

            var bookmark = _bookmarkService.GetBookmark(id, User.Identity.Name);
            if (bookmark == null) { return NotFound(); }

            _bookmarkService.DeleteBookmark(bookmark);

            return RedirectToAction(nameof(Index));
        }

    }
}
