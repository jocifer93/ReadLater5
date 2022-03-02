using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICategoryService
    {
        Category CreateCategory(Category category);
        List<Category> GetCategories(string username);
        Category GetCategory(int id, string username);
        Category GetCategoryAsNoTracking(int id, string username);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
