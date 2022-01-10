using CommunityProject.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityProject.Models.Repos
{
    public class PostRepo : IPostRepo
    {
        private readonly CommunityDbContext _communityDbContext;

        public PostRepo(CommunityDbContext communityDbContext)
        {
            _communityDbContext = communityDbContext;
        }
        public Post Create(Post post)
        {
            _communityDbContext.Posts.Add(post);
            _communityDbContext.SaveChanges();
            return post;
        }
        
        public List<Post> GetAll()
        {
            return _communityDbContext.Posts.Include(post => post.Category).ToList();
        }

      
        public Post FindById(int id)
        {
            return _communityDbContext.Posts.Include(post => post.Category).SingleOrDefault(post => post.Id == id);
        }

      
        public bool Update(Post post)
        {
            _communityDbContext.Posts.Update(post);
            int updateChanges = _communityDbContext.SaveChanges();
            if (updateChanges == 0)
            {
                return false;
            }

            return true;
        }

        public bool Delete(Post post)
        {
            _communityDbContext.Posts.Remove(post);
            int saveChanges = _communityDbContext.SaveChanges();
            if (saveChanges == 0)
            {
                return false;
            }
            return true;
            
         }

        public int CategoryPost(int categoryId)
        {
            return _communityDbContext.Posts.Count(p => p.CategoryId == categoryId);
        }
    }
}
