using CommunityProject.Models.Repos;
using CommunityProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityProject.Models.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        //private IPostRepo _postRepo;
        public CategoryService(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
            //_postRepo = postRepo;
        }
        public Category Create(CreateCategoryViewModel createCategory)
        {
            if (string.IsNullOrWhiteSpace(createCategory.CategoryName))
            {
                throw new ArgumentException("Category cannot consist of backsapce and whitespace");
            }

            Category category = new Category()
            {
                CategoryName = createCategory.CategoryName              
            };
            return _categoryRepo.Create(category);
        }

    
        public List<Category> GetAll()
        {
            return _categoryRepo.GetAll();
        }

        public Category FindById(int id)
        {
            return _categoryRepo.FindById(id);
        }

        public bool Edit(int id, CreateCategoryViewModel editCategory)
        {
            Category currentCategory = FindById(id);

            if (currentCategory == null)
            {
                return false;
            }

            currentCategory.CategoryName = editCategory.CategoryName;

            return _categoryRepo.Update(currentCategory);
        }

        public bool Remove(int id)
        {
            Category category = _categoryRepo.FindById(id);

            if (category != null)
            {
                return _categoryRepo.Delete(category);
            }
            return false;
        }
    }
}
