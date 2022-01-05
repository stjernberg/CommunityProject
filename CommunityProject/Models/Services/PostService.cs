using CommunityProject.Models.Repos;
using CommunityProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityProject.Models.Services
{
    public class PostService : IPostService
    {

        private readonly IPostRepo _postRepo;
     
        public PostService(IPostRepo postRepo)
        {
            _postRepo = postRepo;
        }
        public Post Create(CreatePostViewModel createPost)
        {
            Post post = new Post()
            {
                Title = createPost.Title,
                Text = createPost.Text,
                CreatedBy = createPost.CreatedBy,
                Category = createPost.Category                

            };
            return _postRepo.Create(post);
        }

      
        public bool Edit(int id, CreatePostViewModel editPost)
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

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
