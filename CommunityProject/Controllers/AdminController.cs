using CommunityProject.Models;
using CommunityProject.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CommunityProject.Controllers
{
    //[Authorize(Roles = "Admin, SuperAdmin")]
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

        [HttpGet("getRole/{id}")]
        public async Task<IActionResult> GetRole(string id)
        {

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }


        [HttpGet("allUsers")]
        public List<AppUser> AllUsers()
        {
            return _userManager.Users.ToList();
        }

        [HttpPost("createRole")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleViewModel createRole)
        {
            IdentityRole role = new IdentityRole(createRole.RoleName);


            var result = await _roleManager.CreateAsync(role);
            if (role != null)
            {
                Response.StatusCode = 201;
            }
            else
            {
                Response.StatusCode = 400;
            }

            return Ok(result);
        }

        


        //        // POST api/<AdminController> 
        //        [ProducesResponseType(201)]
        //        [ProducesResponseType(400)]
        //        [HttpPost("createRole")]
        //        public async Task<IActionResult> CreateRole(string roleName)
        //        {
        //            IdentityRole role = new IdentityRole(roleName);

        //            var result = await _roleManager.CreateAsync(role);
        //            return Ok(result);
        //        }
        //                if (result.Succeeded)
        //                {
        //                    return Ok(result);
        //    }

        //                return BadRequest("Error: User could not be created");

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

        [HttpDelete("deleteUser/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(await user);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
            }

            return BadRequest("Error");
        }

        [HttpGet("usersWithRole/{id}")]
        public async Task<IActionResult> ManageUserRoles(string id)
        {

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            ManageRolesViewModel rolesViewModel = new ManageRolesViewModel();

            rolesViewModel.Role = role;

            rolesViewModel.UserWithRole = await _userManager.GetUsersInRoleAsync(role.Name);

            rolesViewModel.UserNoRole = _userManager.Users.ToList();

            foreach (var item in rolesViewModel.UserWithRole)
            {
                rolesViewModel.UserNoRole.Remove(item);
            }

            return Ok(rolesViewModel);


        }



        [HttpGet("addToRole/{userId}/{roleId}")]
        public async Task<IActionResult> AddToRole(string userId, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return Ok(result);
            }


            return BadRequest("User could not be added to role");


        }

        [HttpGet("removeFromRole/{userId}/{roleId}")]
        public async Task<IActionResult> RemoveFromRole(string userId, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);


            if (role == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return Ok(result);
            }


            return BadRequest("User could not be removed from the role");

        }
    }
}
