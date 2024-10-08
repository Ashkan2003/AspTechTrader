﻿using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.Domain.RepositoryContracts;
using AspTechTrader.Core.DTO;
using AspTechTrader.Core.Helpers;
using AspTechTrader.Core.ServiceContracts;

namespace AspTechTrader.Core.Services
{
    public class UserWatchListsService : IUserWatchListsService
    {
        private readonly IUserWatchListsRepository _userWatchListsRepository;
        public UserWatchListsService(IUserWatchListsRepository userWatchListsRepository)
        {
            _userWatchListsRepository = userWatchListsRepository;
        }



        public async Task<User> GetUserWithRelatedUserWatchListById(Guid userId)
        {

            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            User? UserWithRelatedUserWatchList = await _userWatchListsRepository.GetUserWithRelatedUserWatchListById(userId);

            if (UserWithRelatedUserWatchList == null)
            {
                throw new Exception("no user founded with the given id");

            }

            return UserWithRelatedUserWatchList;
        }


        public async Task<User> AddNewUserWatchList(UserWatchListAddRequestDTO userWatchListAddRequest)
        {
            if (userWatchListAddRequest == null)
            {
                throw new ArgumentNullException(nameof(userWatchListAddRequest));
            }

            if (userWatchListAddRequest.UserId == Guid.Empty)
            {
                throw new ArithmeticException(nameof(userWatchListAddRequest.UserId));
            }

            User? matchedUser = await _userWatchListsRepository.GetUserWithRelatedUserWatchListById(userWatchListAddRequest.UserId);

            return await _userWatchListsRepository.AddNewUserWatchList(userWatchListAddRequest);
        }

        public async Task<bool> DeleteUserWatchList(UserWatchListDeleteRequestDTO userWatchListDeleteRequestDTO)
        {
            if (userWatchListDeleteRequestDTO == null)
            {
                throw new ArgumentNullException(nameof(userWatchListDeleteRequestDTO));
            }

            ValidationHelper.ModelValidation(userWatchListDeleteRequestDTO);

            bool isDeleted = await _userWatchListsRepository.DeleteUserWatchList(userWatchListDeleteRequestDTO);

            return isDeleted;

        }


        public async Task<UserWatchList?> AddNewSymbolToUserWatchList(AddSymbolToUserWatchListRequestDTO addSymbolToUserWatchListRequestDTO)
        {
            if (addSymbolToUserWatchListRequestDTO == null)
            {
                throw new ArgumentNullException(nameof(addSymbolToUserWatchListRequestDTO));
            }

            //Model validation
            ValidationHelper.ModelValidation(addSymbolToUserWatchListRequestDTO);

            UserWatchList? matchedUserWatchList = await _userWatchListsRepository.AddNewSymbolToUserWatchList(addSymbolToUserWatchListRequestDTO);

            return matchedUserWatchList;

        }

        public async Task<bool> RemoveSymbolFromUserWatchList(RemoveSymbolFromUserWatchListDTO removeSymbolFromUserWatchListDTO)
        {
            if (removeSymbolFromUserWatchListDTO == null)
            {
                throw new ArgumentNullException(nameof(removeSymbolFromUserWatchListDTO));
            }

            //Model validation
            ValidationHelper.ModelValidation(removeSymbolFromUserWatchListDTO);

            bool isSuccessfullyRemoved = await _userWatchListsRepository.RemoveSymbolFromUserWatchList(removeSymbolFromUserWatchListDTO);

            return isSuccessfullyRemoved;
        }
    }
}
