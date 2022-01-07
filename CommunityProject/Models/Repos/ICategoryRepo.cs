using System.Collections.Generic;

namespace CommunityProject.Models.Services
{
    public interface ICategoryRepo
    {
       Category Create(Category category);
        Category FindById(int id);
        List<Category> GetAll();
        bool Update(Category category);
        bool Delete(Category category);
    }
}