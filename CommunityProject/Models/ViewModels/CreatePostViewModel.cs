using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityProject.Models.ViewModels
{
    public class CreatePostViewModel
    {
        [Required]
        [StringLength(80, MinimumLength = 3)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(1500, MinimumLength = 3)]
        [Display(Name = "Text")]
        public string Text { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Name")]
        public string CreatedBy { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
