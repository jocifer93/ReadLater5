using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;
using System.Linq;

namespace ReadLater5.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET: Categories
        public IActionResult Index()
        {
            List<Category> model = _categoryService.GetCategories(User.Identity.Name);
            return View(model);
        }

        // GET: Categories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) { return BadRequest(); }

            Category category = _categoryService.GetCategory((int)id, User.Identity.Name);
            if (category == null) { return NotFound(); }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                CreateCategory(category);

                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) { return BadRequest(); }

            Category category = _categoryService.GetCategory((int)id, User.Identity.Name);
            if (category == null) { return NotFound(); }

            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("ID,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                var cat = _categoryService.GetCategoryAsNoTracking(category.ID, User.Identity.Name);
                if (cat == null) { return NotFound(); }

                category.Author = User.Identity.Name;
                _categoryService.UpdateCategory(category);

                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) { return BadRequest(); }

            Category category = _categoryService.GetCategory((int)id, User.Identity.Name);
            if (category == null) { return NotFound(); }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id == 0) { return BadRequest(); }

            Category category = _categoryService.GetCategory(id, User.Identity.Name);
            if (category == null) { return NotFound(); }

            _categoryService.DeleteCategory(category);

            return RedirectToAction("Index");
        }

        //public IActionResult GetCategoriesJson()
        //{
        //    var categories = _categoryService.GetCategories(User.Identity.Name);

        //    return new JsonResult(categories);
        //}

        [HttpPost]
        public IActionResult CreateCategoryAjax([Bind("Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                var newCategory = CreateCategory(category);

                return Ok(newCategory);
            }

            return BadRequest();
        }

        #region Private methods
        private Category CreateCategory(Category category)
        {
            category.Author = User.Identity.Name;
            return _categoryService.CreateCategory(category);
        }
        #endregion
    }
}
