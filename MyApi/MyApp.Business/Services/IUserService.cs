using Microsoft.AspNetCore.JsonPatch;
using MyApp.Data.DTOs;
using MyApp.Data.Models;
using MyApp.Data.Payloads;

namespace MyApp.Business.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetUsers();
        public Task<UserDTO> GetUserById(int userId);
        public Task<UserDTO> AddUser(AddUserPayload user);
        public Task<UserDTO> UpdateUser(int id, JsonPatchDocument<UpdateUserPayload> user);
        public Task<UserDTO> DeleteUser(int userId);
    }
}
