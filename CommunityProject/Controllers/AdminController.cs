using CommunityProject.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CommunityProject.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/<AdminController>
        [HttpGet("allRoles")]
        public List<IdentityRole> AllUserRoles()
        {
            return _roleManager.Roles.ToList();
        }

        //// GET api/<AdminController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{ 
        //    return "value";
        //}

        // POST api/<AdminController> 
        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            IdentityRole role = new IdentityRole(roleName);

            var result = await _roleManager.CreateAsync(role);
            return Ok(result);

            //if (result.Succeeded)
            //{
            //    return Ok(result);
            //}
            //return BadRequest("Error");
        }

        //// PUT api/<AdminController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //DELETE api/<AdminController>/5
        [HttpDelete("deleteRole/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(string id)
        {
            var role = _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(await role);
                if (result.Succeeded)
                {
                    return Ok(result);

                }
            }
            
            return BadRequest("Error");
        }
            //if (role != null)
            //{
            //    var result = await _roleManager.DeleteAsync(role);

            //    if (result.Succeeded)
            //    {
            //        return Ok(result);
            //    }
            //}


            //return BadRequest("Error");
            // }  
        }
}
