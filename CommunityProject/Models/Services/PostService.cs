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
            
            Post currentPost = _postRepo.FindById(id);

            if (currentPost != null)
            {
                currentPost.Title = editPost.Title;
                currentPost.Text = editPost.Text;
                currentPost.CreatedBy = editPost.CreatedBy;
                currentPost.Category = editPost.Category;               
            };

            return _postRepo.Update(currentPost);
        }

       
        public Post FindById(int id)
        {
            return _postRepo.FindById(id);
        }

        public List<Post> GetAll()
        {
            return _postRepo.GetAll();
        }

        public bool Remove(int id)
        {
            Post post = _postRepo.FindById(id);

            if (post != null)
            {
                return _postRepo.Delete(post);
            }
            return false;
        }
    }
}
