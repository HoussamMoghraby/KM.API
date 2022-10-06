using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyApp.Business.Exceptions;
using MyApp.Business.Services;
using MyApp.Data.DTOs;
using MyApp.Data.Models;
using MyApp.Data.Payloads;

namespace MyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();

            var response = new ResponseDTO<List<User>>(users, "All users successfully retrieved");
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);

                var response = new ResponseDTO<UserDTO>(user, $"User with Id { id } was successfully retrieved");
                return Ok(response);
            }
            catch (CustomBusinessException ex)
            {
                if (ex.Type == ErrorType.BadRequest)
                {
                    return BadRequest(ex.Message);
                }
                else if (ex.Type == ErrorType.NotFound)
                {
                    return NotFound(ex.Message);
                }
                else
                {
                    throw;
                }
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserPayload user)
        {
            try
            {
                var addedUser = await _userService.AddUser(user);

                var response = new ResponseDTO<UserDTO>(addedUser, "User was successfully created");
                return Created(nameof(AddUser), response);
            }
            catch (CustomBusinessException ex)
            {
                if (ex.Type == ErrorType.BadRequest)
                {
                    return BadRequest(ex.Message);
                }
                else
                {
                    throw;
                }
            }
             
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] JsonPatchDocument<UpdateUserPayload> userPatch)
        {
            try
            {
                //map to entity here
                var updatedUser = await _userService.UpdateUser(id, userPatch);

                var response = new ResponseDTO<UserDTO>(updatedUser, $"Successfully modified information for user with Id { id }");
                return Ok(response);
            }
            catch (CustomBusinessException ex)
            {
                if (ex.Type == ErrorType.BadRequest)
                {
                    return BadRequest(ex.Message);
                }
                else
                {
                    throw;
                }
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            try
            {
                var deletedUser = await _userService.DeleteUser(id);

                var response = new ResponseDTO<UserDTO>(deletedUser, $"User with Id { id } was successfully deleted");
                return Ok(response);
            }
            catch (CustomBusinessException ex)
            {
                if (ex.Type == ErrorType.NotFound)
                {
                    return NotFound(ex.Message);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
