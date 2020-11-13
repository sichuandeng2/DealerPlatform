using System.Collections.Generic;
using System.Linq;
using DealerPlatformApiDemo.Entity;

namespace DealerPlatformApiDemo.Service.UserService
{
    public partial class UserService
    {
        public List<UserMenus> ListUserMenusAll()
        {
            return _userMenusRepository.ListAll().ToList();
        }
        public List<UserMenus> GetUserMenusByUid(int userId)
        {
            return _userMenusRepository.ListByCustom("UserId", userId.ToString()).ToList();
        }
        public void DeleteUserMenusByUid(int userId)
        {
            _userMenusRepository.DeleteByCustomer("UserId", userId + "");
        }
        private void InsertUserMenusByUid(int userId, IEnumerable<int> menuIds)
        {
            foreach (var menuId in menuIds)
            {
                UserMenus userMenus = new UserMenus()
                {
                    UserId = userId,
                    MenuId = menuId
                };
                _userMenusRepository.Insert(userMenus);

            }
        }
        public  void ResetUserMenusByUid(int userId, IEnumerable<int> menuIds){
            //加一个事务
            DeleteUserMenusByUid(userId);
            InsertUserMenusByUid(userId,menuIds);
        }
    }
}