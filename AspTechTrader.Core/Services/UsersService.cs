﻿using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.Domain.RepositoryContracts;
using AspTechTrader.Core.DTO;
using AspTechTrader.Core.Helpers;
using AspTechTrader.Core.ServiceContracts;

namespace AspTechTrader.Core.Services
{
    public class UsersService : IUserService
    {

        private readonly IUsersRepository _usersRepository;


        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }


        public async Task<User?> GetUserById(Guid? userId)
        {

            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            User? findedUser = await _usersRepository.GetUserById(userId.Value);

            if (findedUser == null)
            {
                return null;
            }

            return findedUser;

        }

        public async Task<User?> GetUserByEmail(string emailAddress)
        {
            if (emailAddress == null)
            {
                throw new ArgumentNullException(nameof(emailAddress));
            }

            User? matchedUser = await _usersRepository.GetUserByEmail(emailAddress);

            return matchedUser;
        }

        public async Task<User> AddUser(UserAddRequestDTO? userAddRequest)
        {
            // chech in the userAddRequest in not null
            if (userAddRequest == null)
            {
                throw new ArgumentNullException(nameof(userAddRequest));
            }

            //Model validation
            ValidationHelper.ModelValidation(userAddRequest);


            // check for the duplicate user in db
            User? matchedUser = await _usersRepository.GetUserByEmail(userAddRequest.EmailAddress);

            if (matchedUser != null)
            {
                throw new Exception("a user exists with the given gmail in db");
            }

            User user = new User()
            {
                UserId = Guid.NewGuid(),
                EmailAddress = userAddRequest.EmailAddress,
                UserName = userAddRequest.UserName,
                UserProperty = 40000,
                UserRole = userAddRequest.UserRole,
            };


            User? AddedUser = await _usersRepository.AddUser(user);

            return AddedUser;
        }

        public async Task<bool> UpdateUserProperty(UserPropertyUpdateRequestDTO userPropertyUpdateRequestDTO)
        {


            User? matchedUser = await GetUserById(userPropertyUpdateRequestDTO.UserId);

            if (matchedUser == null)
            {
                throw new ArgumentException("no user founded with the given userId");
            }

            bool isSuccess = await _usersRepository.UpdateUserProperty(userPropertyUpdateRequestDTO);

            return isSuccess;
        }

        public async Task<List<User>> GetAllUsers()
        {
            List<User> allUsers = await _usersRepository.GetAllUsers();

            return allUsers;
        }

        // these methods arent used
        public async Task<bool> DeleteUserById(Guid? userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            User? user = await _usersRepository.GetUserById(userId.Value);

            if (user == null)
            {
                return false;
            }

            await _usersRepository.DeleteUserById(userId.Value);

            return true;

        }
        public async Task<User> UpdateUser(UserUpdateRequestDTO? userUpdateRequest)
        {

            if (userUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(userUpdateRequest));
            }

            if (userUpdateRequest.UserId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userUpdateRequest.UserId));
            }

            ValidationHelper.ModelValidation(userUpdateRequest);

            User? matchingUser = await _usersRepository.GetUserById(userUpdateRequest.UserId);

            if (matchingUser == null)
            {
                throw new ArgumentException("user was not in database");
            }

            // update user details
            matchingUser.UserName = userUpdateRequest.UserName;
            matchingUser.EmailAddress = userUpdateRequest.EmailAddress;
            matchingUser.UserProperty = userUpdateRequest.UserProperty;


            User updatedUser = await _usersRepository.UpdateUser(matchingUser);

            return updatedUser;
        }


    }
}
