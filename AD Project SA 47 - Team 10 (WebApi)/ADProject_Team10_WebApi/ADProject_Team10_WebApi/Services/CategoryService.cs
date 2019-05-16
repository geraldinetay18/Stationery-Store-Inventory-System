/* Author: Zhao Pengkai */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADProject_Team10_WebApi.Services
{
    using Models;
    public class CategoryService : ICategoryService
    {
        public int AddCategory(Category category)
        {
            SSAEntities context = new SSAEntities();
            try
            {
                context.Categories.Add(category);
                return context.SaveChanges(); 
            }
            catch (Exception e)
            {
                return 0; 
            }
        }

        public List<Category> FindAllCategories()
        {
            SSAEntities context = new SSAEntities();
            List<Category> listCategory = context.Categories.ToList<Category>();
            return listCategory;
        }

        public Category FindCategoryById(int categoryId)
        {
            SSAEntities context = new SSAEntities();
            Category category = context.Categories.Where(x => x.CategoryId == categoryId).FirstOrDefault();
            return category;
        }

        public List<Category> FindCategoryByName(string name)
        {
            SSAEntities context = new SSAEntities();
            List<Category> categorylist = context.Categories.Where(x => x.CategoryName.Contains(name)).ToList<Category>();
            return categorylist;
        }

        public Category FindCategoryByFullName(string name)
        {
            SSAEntities context = new SSAEntities();
            Category category = context.Categories.Where(x => x.CategoryName == name).FirstOrDefault();
            return category;
        }

        public int UpdateCategory(Category category)
        {
            SSAEntities context = new SSAEntities();
            try
            {
                Category c = context.Categories.Where(x => x.CategoryId == category.CategoryId).FirstOrDefault();

                
                c.CategoryId = category.CategoryId;
                c.CategoryName = category.CategoryName;
                return context.SaveChanges(); 
            }
            catch (Exception e)
            {
                return 0; 
            }
        }

        public int DeleteCategoryById(int categoryId)
        {
            SSAEntities context = new SSAEntities();
            try
            {
                Category c = context.Categories.Where(x => x.CategoryId == categoryId).FirstOrDefault();
                context.Categories.Remove(c);
                return context.SaveChanges(); 
            }
            catch (Exception e)
            {
                return 0; 
            }
        }
    }
}