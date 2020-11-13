using System;
using System.Collections.Generic;
using DealerPlatformApiDemo.Entity;
using DealerPlatformApiDemo.Service.UserService.Dto;

namespace DealerPlatformApiDemo.Service.UserService
{
    public interface IUsers
    {
        List<Users> ListUserAll();
        Users GetUserByPwd(string userName, string pwd);
        Users GetUserByGuid(Guid userGuid);
        Users GetUserById(int userId);
        Users InsertUser(Users user);
        void UpdateUser(Users user);
        List<Menus> ListMenusAll();
        List<Menus> GetMenusByUids(IEnumerable<int> userIds);
        List<UserMenus> ListUserMenusAll();
        List<UserMenus> GetUserMenusByUid(int userId);
        void DeleteUserMenusByUid(int userId);

        void ResetUserMenusByUid(int userId, IEnumerable<int> menuIds);
        List<UserDto> GetUserDto();
    }
}