﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperChatsWebAPI.Models;

namespace SuperChatsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UserManager<User> UserManager;

        public UsersController(UserManager<User> userManager)
        {
            this.UserManager = userManager;
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if(registerDTO.Password != registerDTO.PasswordConfirm)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new { Message = "Les deux mots de passe spécifiés sont différents."});
            }

            User user = new User()
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email
            };

            IdentityResult identityResult = await UserManager.CreateAsync(user, registerDTO.Password);
            if(!identityResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = "La création de l'utilisateur a échoué." });
            }
            return Ok();
        }
    }
}
