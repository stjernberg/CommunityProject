using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommunityProject.Models.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        [StringLength(80, MinimumLength = 2)]
        [Display(Name = "Category name")]
        public string CategoryName { get; set; }
       
    }
}
