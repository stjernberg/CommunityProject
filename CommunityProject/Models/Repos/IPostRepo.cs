using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityProject.Models.Repos
{
    public interface IPostRepo
    {
       Post Create(Post post);
        Post FindById(int id);
        List<Post> GetAll();
        bool Update(Post post);
        bool Delete(Post post);
    }
}
