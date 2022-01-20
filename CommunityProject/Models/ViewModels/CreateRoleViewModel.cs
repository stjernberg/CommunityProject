using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityProject.Models.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [StringLength(80, MinimumLength = 2)]
        [Display(Name = "Role name")]
        public string RoleName { get; set; }
    }
}
