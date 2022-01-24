using CommunityProject.Models;
using CommunityProject.Models.Services;
using CommunityProject.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CommunityProject.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthService _authService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,
            IAuthService authService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authService = authService;
            _roleManager = roleManager;
        }



        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel userLogin)
        {


            var result = await _signInManager.PasswordSignInAsync(userLogin.UserName, userLogin.Password, false, false);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }
            AppUser user = await _userManager.FindByNameAsync(userLogin.UserName);
            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            string jwtToken = _authService.GenerateJwtToken(user, userRoles, User.Claims);

            return Ok(jwtToken);

        }




        [HttpGet("getUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUser()
        {

            string userName = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
           
            
            if (user != null)
            {
                return Ok(user);

            }
            return BadRequest("Error");
        }


       
        [HttpGet("CheckRole")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CheckUserRole()
        {
            string userName = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return BadRequest("user not found");
               
            }

            IList<string> roles = await _userManager.GetRolesAsync(user);

            return Ok(roles);
        }

                




        [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegUserViewModel regUser)
    {
        if (ModelState.IsValid)
        {
            AppUser user = new AppUser()
            {
                FirstName = regUser.FirstName,
                LastName = regUser.LastName,
                UserName = regUser.UserName,
                Email = regUser.Email,
                PhoneNr = regUser.PhoneNr
            };
            IdentityResult result = await _userManager.CreateAsync(user, regUser.Password);

            if (result.Succeeded)
            {
                return Ok("User created");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(item.Code, item.Description);
            }
        }
        return BadRequest(ModelState);
    }

        [HttpGet("editUser/{id}")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            
            var model = new EditUserViewModel
            {
                //Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNr = user.PhoneNr
            };

            return Ok(model);
        }

        [AllowAnonymous]
        [HttpPut("editUser/{id}")]
        public async Task<IActionResult> EditUser(string id, EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNr = model.PhoneNr;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return Ok(model);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return BadRequest("User could not be edited");
            }
        }

       
        [HttpPost("logout")]
        public void Logout()
        {
              _signInManager.SignOutAsync();
            
           
         }
    }
}
