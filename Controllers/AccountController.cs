using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using students_backend_asp_sqlite.Dtos.Account;
using students_backend_asp_sqlite.Interfaces;
using students_backend_asp_sqlite.Models;

namespace students_backend_asp_sqlite.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try {
                if(!ModelState.IsValid)
                return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email
                };

               var creatdeUser = await _userManager.CreateAsync(appUser , registerDto.Password);
                if(creatdeUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser , "User");
                    if(roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto 
                        {
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                            Token = _tokenService.CreateToken(appUser)
                        }
                        );
                    }
                    else 
                    {
                        return StatusCode(500, roleResult.Errors);
                    }

                }
                else 
                {
                        return StatusCode(500, creatdeUser.Errors);
                    
                }
            } catch (Exception e)
            {
                        return StatusCode(500, e);

            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if(!ModelState.IsValid)
            return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());

            if(user == null) return Unauthorized("Invalid Username");

            // var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded) return Unauthorized("Invalid Username or password");

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token  = _tokenService.CreateToken(user)
                }
            );
        }


    }
}