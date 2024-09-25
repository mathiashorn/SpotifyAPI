using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpotifyAPI.Models;
using SpotifyAPI.Services;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpotifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get(int id)
        {
            return Ok(await _userService.GetUsers());
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            return Ok(await _userService.GetUser(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            var message = "";
            if (!_userService.IsValidUser(user, out message))
            {
                return BadRequest(message);
            }

            await _userService.CreateUser(user);
            return Ok();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            var message = "";
            if (!_userService.IsValidUser(user, out message))
            {
                return BadRequest(message);
            }

            await _userService.EditUser(user);

            return Ok();

        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteUser(id);
            return Ok();
        }
    }
}
