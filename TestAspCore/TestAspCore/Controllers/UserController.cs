using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAspCore.Authentication;

namespace TestAspCore.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)

        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }


        // GET: UserController/GetList
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public IEnumerable<AppUser> GetUsers()
        {

            var users = userManager.Users.Where(u => u.Email != User.Identity.Name);

            return users;
        }

        // GET: UserController/GetUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(Guid id)
        {

            var user = await userManager.FindByIdAsync(id.ToString());
            if (user is null)
            {
                return NotFound();
            }

            return user;
        }


        // POST: UserController/Create
        [HttpPost]
        public async Task<ActionResult<AppUser>> Post([FromBody] RegisterModel model)
        {
            var userExist = await userManager.FindByNameAsync(model.UserName);
            if (userExist != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = " User Already Exist" });

            AppUser user = new AppUser
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Failed to register new user" });

            await userManager.AddToRolesAsync(user, new List<string>() { UserRoles.User });
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }


        // PUT: UserController/Update
        [HttpPut("{id}")]

        public async Task<ActionResult> Put(Guid id, [FromBody] EditUserModel model)
        {
            if (id.ToString() != model.Id)
            {
                return BadRequest();
            }

            AppUser user = await userManager.FindByIdAsync(model.Id.ToString());

            if (user is not null)
            {
                user.Email = model.Email;
                user.UserName = model.UserName;

                var PasswordHash = userManager.PasswordHasher.HashPassword(user, model.Password);
                user.PasswordHash = PasswordHash;
                var result = await userManager.UpdateAsync(user);

                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Failed to update user" });

                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User not found" });

        }


        // GET: UserController/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user is null)
            {
                return NotFound();
            }
            await userManager.DeleteAsync(user);
            return Ok(new Response { Status = "Success", Message = "User deleted Successfully" });
        }










    }

}
