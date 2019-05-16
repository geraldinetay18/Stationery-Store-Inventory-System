/* Author: Zhao Pengkai */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADProject_Team10_WebApi.Services
{
    using Models;
    interface ICategoryService
    {
        int AddCategory(Category category);
        List<Category> FindAllCategories();
        Category FindCategoryById(int categoryId);
        List<Category> FindCategoryByName(string name);
        Category FindCategoryByFullName(string name);
        int UpdateCategory(Category category);
        int DeleteCategoryById(int categoryId);
    }
}
