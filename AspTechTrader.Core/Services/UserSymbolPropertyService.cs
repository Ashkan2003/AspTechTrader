using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.Domain.RepositoryContracts;
using AspTechTrader.Core.DTO;
using AspTechTrader.Core.ServiceContracts;

namespace AspTechTrader.Core.Services
{
    public class UserSymbolPropertyService : IUserSymbolPropertyService
    {


        private readonly IUserSymbolPropertyRepository _userSymbolPropertyRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ISymbolsRepository _symbolsRepository;

        public UserSymbolPropertyService(IUserSymbolPropertyRepository userSymbolPropertyRepository, IUsersRepository usersRepository, ISymbolsRepository symbolsRepository)
        {
            _userSymbolPropertyRepository = userSymbolPropertyRepository;
            _usersRepository = usersRepository;
            _symbolsRepository = symbolsRepository;
        }



        public async Task<User> AddNewBoughtSymbol(UserBoughtSymbolAddRequestDTO userBoughtSymbolAddRequest)
        {
            if (userBoughtSymbolAddRequest.UserId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userBoughtSymbolAddRequest.UserId));
            }

            User? matchedUser = await _usersRepository.GetUserById(userBoughtSymbolAddRequest.UserId);

            Symbol? matchedSymbol = await _symbolsRepository.GetSymbolById(userBoughtSymbolAddRequest.SymbolId);

            // check if the user exists with the given userId
            if (matchedUser == null)
            {
                throw new ArgumentException("no user founded with the given userId");
            }

            // check if symbol existes with the given symbolId
            if (matchedSymbol == null)
            {
                throw new ArgumentException("no symbol founded with the given SymbolId");
            }

            // check if the user bought this symbol previosly or not
            UserSymbolProperty? matchedUserSymbolProperty = matchedUser.UserSymbolProperties.FirstOrDefault(temp => temp.SymbolId == userBoughtSymbolAddRequest.SymbolId);

            // if the user has this symbol already in his userSymbolProperties // this symbol was bought by user previosly
            // dont create a newUserSymbolProperty, in other hand update the price and quantity fild
            if (matchedUserSymbolProperty != null)
            {

                int privioseBoughtSymbolQuantity = matchedUserSymbolProperty.SymbolQuantity;

                //sum symbol-previose-quantity with  currentBoughtQuantity
                //matchedUserSymbolProperty.SymbolQuantity = privioseBoughtSymbolQuantity + userBoughtSymbolAddRequest.SymbolQuantity;

                //matchedUserSymbolProperty.SymbolPrice = userBoughtSymbolAddRequest.SymbolPrice;

                UserSymbolPropertyUpdateRequestDTO userSymbolPropertyUpdateRequestDTO = new UserSymbolPropertyUpdateRequestDTO()
                {
                    UserSymbolPropertyId = matchedUserSymbolProperty.UserSymbolPropertyId,

                    SymbolQuantity = privioseBoughtSymbolQuantity + userBoughtSymbolAddRequest.SymbolQuantity,
                    SymbolPrice = userBoughtSymbolAddRequest.SymbolPrice,
                };

                bool isSuccess = await _userSymbolPropertyRepository.UpdateUserSymbolProperty(userSymbolPropertyUpdateRequestDTO);
                return matchedUser;
            }
            // if the user dont have this symbol already in his userSymbolProperties // user wants to buy this symbol in this time
            else
            {

                UserSymbolProperty newUserSymbolPropertyToAdd = new UserSymbolProperty()
                {
                    SymbolQuantity = userBoughtSymbolAddRequest.SymbolQuantity,
                    SymbolPrice = userBoughtSymbolAddRequest.SymbolPrice,

                    UserId = userBoughtSymbolAddRequest.UserId,
                    SymbolId = userBoughtSymbolAddRequest.SymbolId
                };

                User user = await _userSymbolPropertyRepository.AddNewBoughtSymbol(newUserSymbolPropertyToAdd);

                return user;

            }


        }

        public async Task<bool> SaleSymbol(SymbolSaleRequestDTO symbolSaleRequestDTO)
        {
            User? matchedUser = await _usersRepository.GetUserById(symbolSaleRequestDTO.UserId);

            Symbol? matchedSymbol = await _symbolsRepository.GetSymbolById(symbolSaleRequestDTO.SymbolId);

            // check if the user exists with the given userId
            if (matchedUser == null)
            {
                throw new ArgumentException("no user founded with the given userId");
            }

            // check if symbol existes with the given symbolId
            if (matchedSymbol == null)
            {
                throw new ArgumentException("no symbol founded with the given SymbolId");
            }

            // check if the user bought this symbol previosly or not
            UserSymbolProperty? matchedUserSymbolProperty = matchedUser.UserSymbolProperties.FirstOrDefault(temp => temp.SymbolId == symbolSaleRequestDTO.SymbolId);

            if (matchedUserSymbolProperty == null)
            {
                throw new Exception("user dont have this symbol,so he cant sale it");
            }

            //check if user have enoght symbolQuantity to sale it
            if (matchedUserSymbolProperty.SymbolQuantity < symbolSaleRequestDTO.SymbolSaleQuantity)
            {
                throw new Exception("user dont have enoght symbolQuantity to sale");
            }

            // if user have more symbolQuantity then saleSymbolQuantity so
            // update the userSymbolProperty
            if (matchedUserSymbolProperty.SymbolQuantity > symbolSaleRequestDTO.SymbolSaleQuantity)
            {
                int remainingSymbolQuantity = matchedUserSymbolProperty.SymbolQuantity - symbolSaleRequestDTO.SymbolSaleQuantity;

                bool isSuccess = await _userSymbolPropertyRepository.UpdateUserSymbolProperty(new UserSymbolPropertyUpdateRequestDTO()
                {
                    SymbolPrice = symbolSaleRequestDTO.SymbolSalePrice,
                    SymbolQuantity = remainingSymbolQuantity,

                    UserSymbolPropertyId = matchedUserSymbolProperty.UserSymbolPropertyId
                });

                return isSuccess;
            }

            // if user have equal quantity // symbolQuantity == saleSymbolQuantity so
            // so when we minez the user SymbolQuantity is 0  so delete userSymbolProperty
            if (matchedUserSymbolProperty.SymbolQuantity == symbolSaleRequestDTO.SymbolSaleQuantity)
            {
                bool isSuccess = await _userSymbolPropertyRepository.DeleteBoughtSymbol(matchedUserSymbolProperty.UserSymbolPropertyId);

                return isSuccess;
            }

            return false;

        }


    }
}
