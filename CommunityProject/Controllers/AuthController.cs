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

        public AuthController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IAuthService authService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authService = authService;
        }

        //[AllowAnonymous]
        //[HttpPost("login")]
        //public async Task<IActionResult> Login(string userName, string password)
        //{

        //    var result = await _signInManager.PasswordSignInAsync(userName, password, false, true);

        //    if (!result.Succeeded)
        //    {
        //        return Unauthorized();
        //    }
        //    AppUser user = await _userManager.FindByNameAsync(userName);
        //    IList<string> userRoles = await _userManager.GetRolesAsync(user);

        //    string jwtToken = _authService.GenerateJwtToken(user, userRoles, User.Claims);

        //    return Ok(jwtToken);

        //} 
        
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel userLogin)
        {

          
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(userLogin.UserName, userLogin.Password, false, false);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }
            AppUser user = await _userManager.FindByNameAsync(userLogin.UserName);
            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            string jwtToken = _authService.GenerateJwtToken(user, userRoles, User.Claims);

            return Ok(jwtToken);

        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult TestJwtToken()
        {
            return Ok("It's working!");
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
    }
}
