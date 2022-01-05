using CommunityProject.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityProject.Models.Repos
{
    public class DatabasePostRepo : IPostRepo
    {
        private CommunityDbContext _communityDbContext;

        public DatabasePostRepo(CommunityDbContext communityDbContext)
        {
            _communityDbContext = communityDbContext;
        }
        public Post Create(Post post)
        {
            _communityDbContext.Posts.Add(post);
            _communityDbContext.SaveChanges();
            return post;
        }

        public bool Delete(Post post)
        {
            throw new NotImplementedException();
        }

        public Post FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
