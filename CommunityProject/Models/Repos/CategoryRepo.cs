using CommunityProject.Models.Data;
using CommunityProject.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityProject.Models.Repos
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly CommunityDbContext _communityDbContext;

        public CategoryRepo(CommunityDbContext communityDbContext)
        {
            _communityDbContext = communityDbContext;
        }
        public Category Create(Category category)
        {
            _communityDbContext.Categories.Add(category);
            _communityDbContext.SaveChanges();
            return category;
        }
        public List<Category> GetAll()
        {
            return _communityDbContext.Categories.ToList();
        }
      
        public Category FindById(int id)
        {
            return _communityDbContext.Categories.SingleOrDefault(category => category.Id == id);
        }        

        public bool Update(Category category)
        {
            _communityDbContext.Categories.Update(category);
            int updateChanges = _communityDbContext.SaveChanges();
            if (updateChanges == 0)
            {
                return false;
            }

            return true;
        }

        public bool Delete(Category category)
        {
            _communityDbContext.Categories.Remove(category);
            int saveChanges = _communityDbContext.SaveChanges();
            if (saveChanges == 0)
            {
                return false;
            }
            return true;
        }

    }
}
