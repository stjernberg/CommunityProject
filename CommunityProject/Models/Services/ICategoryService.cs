using CommunityProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityProject.Models.Services
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category FindById(int id);
        Category Create(CreateCategoryViewModel createCategory);
        bool Edit(int id, CreateCategoryViewModel editCategory);
        StatusResult Remove(int id);
    }
}
