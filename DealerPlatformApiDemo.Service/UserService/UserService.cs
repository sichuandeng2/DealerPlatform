using AutoMapper;
using DealerPlatformApiDemo.Core.Data;
using DealerPlatformApiDemo.Entity;

namespace DealerPlatformApiDemo.Service.UserService
{
    public partial class UserService:IUsers
    {
        private readonly IRepository<Users> _userRepository;
        private readonly IRepository<UserMenus> _userMenusRepository;
        private readonly IRepository<Menus> _menusRepository;
        private readonly IMapper _mapper;

        public  UserService(
            IRepository<Users> userRepository, 
            IRepository<UserMenus> userMenusRepository, 
            IRepository<Menus> menusRepository, 
            IMapper mapper)
        {
            this._userRepository = userRepository;
            this._userMenusRepository = userMenusRepository;
            this._menusRepository = menusRepository;
            this._mapper = mapper;
        }
    }
}