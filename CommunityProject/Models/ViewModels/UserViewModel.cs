using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityProject.Models.ViewModels
{
    public class UserViewModel
    {
       
        public AppUser user { get; set; }

        public string role { get; set; }
       
    }
}
