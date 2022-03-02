using Data;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private ReadLaterDataContext _ReadLaterDataContext;
        public CategoryService(ReadLaterDataContext readLaterDataContext)
        {
            _ReadLaterDataContext = readLaterDataContext;
        }

        public Category CreateCategory(Category category)
        {
            _ReadLaterDataContext.Add(category);
            _ReadLaterDataContext.SaveChanges();
            return category;
        }

        public void UpdateCategory(Category category)
        {
            _ReadLaterDataContext.Update(category);
            _ReadLaterDataContext.SaveChanges();
        }

        public List<Category> GetCategories(string username)
        {
            return _ReadLaterDataContext.Categories.Where(c => c.Author == username).ToList();
        }

        public Category GetCategory(int id, string username)
        {
            return _ReadLaterDataContext.Categories.Where(c => c.ID == id && c.Author == username).FirstOrDefault();
        }

        public Category GetCategoryAsNoTracking(int id, string username)
        {
            return _ReadLaterDataContext.Categories.Where(c => c.ID == id && c.Author == username).AsNoTracking().FirstOrDefault();
        }

        public void DeleteCategory(Category category)
        {
            _ReadLaterDataContext.Categories.Remove(category);
            _ReadLaterDataContext.SaveChanges();
        }
    }
}
