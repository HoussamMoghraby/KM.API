using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using MyApp.Business.Exceptions;
using MyApp.Business.Helpers;
using MyApp.Data.DTOs;
using MyApp.Data.Models;
using MyApp.Data.Payloads;
using MyApp.Data.Repositories;

namespace MyApp.Business.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private ICountryRepository _countryRepository;
        private IRoleRepository _roleRepository;
        private IMapper _mapper;
        public UserService(IUserRepository userRepository, ICountryRepository countryRepository, IRoleRepository roleRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _countryRepository = countryRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return users;
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new CustomBusinessException(ErrorType.NotFound, $"No user with Id: {userId} was found");
            }
            
            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public async Task<UserDTO> AddUser(AddUserPayload user)
        {
            var country = await _countryRepository.GetCountryById(user.CountryId);
            var role = await _roleRepository.GetRoleById(user.RoleId);
            if (country == null)
            {
                throw new CustomBusinessException(ErrorType.BadRequest, $"The Id: { user.CountryId } is not an existing country Id");
            }
            if (role == null)
            {
                throw new CustomBusinessException(ErrorType.BadRequest, $"The Id: { user.RoleId } is not an existing role Id");
            }

            var emailAvailable = await _userRepository.IsEmailAvailable(user.Email);
            if (!emailAvailable)
            {
                throw new CustomBusinessException(ErrorType.BadRequest, $"The email: { user.Email } is already in use");
            }

            var hashedPassword = StringHasher.HashString(user.Password);
            user.Password = hashedPassword;

            var userEntity = _mapper.Map<User>(user);
            userEntity.Country = country;
            userEntity.Role = role;

            await _userRepository.AddUser(userEntity);

            var response = _mapper.Map<UserDTO>(userEntity);
            return response;
        }

        public async Task<UserDTO> UpdateUser(int id, JsonPatchDocument<UpdateUserPayload> userPayload)
        {
            var userEntity = await _userRepository.GetUserById(id);

            if (userEntity != null)
            {
                var userToPatch = _mapper.Map<UpdateUserPayload>(userEntity);
                userPayload.ApplyTo(userToPatch);
                _mapper.Map(userToPatch, userEntity);
                
                if (userEntity.Country == null)
                {
                    throw new CustomBusinessException(ErrorType.BadRequest, "Provided Country Id does not exist");
                }
                if (userEntity.Role == null)
                {
                    throw new CustomBusinessException(ErrorType.BadRequest, "Provided Role Id does not exist");
                }
                await _userRepository.UpdateUser(userEntity);
            }
            else
            {
                throw new CustomBusinessException(ErrorType.BadRequest, $"No user with Id: {id} was found");
            }

            var userDTO = _mapper.Map<UserDTO>(userEntity);
            return userDTO;
        }

        public async Task<UserDTO> DeleteUser(int userId)
        {
            var deletedUser = await _userRepository.DeleteUser(userId);

            if (deletedUser == null)
            {
                throw new CustomBusinessException(ErrorType.NotFound, $"No user with Id: {userId} was found");
            }

            var deletedUserDTO = _mapper.Map<UserDTO>(deletedUser);
            return deletedUserDTO;
        }
    }
}
