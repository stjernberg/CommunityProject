using CommunityProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityProject.Models.Services
{
    public interface IPostService
    {
        List<Post> GetAll();
        Post FindById(int id);
        Post Create(CreatePostViewModel createPost);
        bool Edit(int id, CreatePostViewModel editPost);
        bool Remove(int id);
    }
}

