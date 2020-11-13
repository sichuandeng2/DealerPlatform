using System;
using System.Linq;
using System.Collections.Generic;
using DealerPlatformApiDemo.Common.RedisHelper;
using DealerPlatformApiDemo.Entity;
using Newtonsoft.Json;
using DealerPlatformApiDemo.Common;
using DealerPlatformApiDemo.Service.UserService.Dto;

namespace DealerPlatformApiDemo.Service.UserService
{
    public partial class UserService
    {
        public List<Users> ListUserAll()
        {
            List<Users> list = new List<Users>();
            bool hasKey = RedisHelper.KeyExists(AppConst.redisUserKey);
            if (!hasKey)
            {
                list = _userRepository.ListAll().ToList();
                RedisHelper.SetStringMemory(AppConst.redisUserKey, JsonConvert.SerializeObject(list), TimeSpan.FromHours(2));
            }
            else
            {
                var strJson = RedisHelper.GetStringMemory(AppConst.redisUserKey);
                list = JsonConvert.DeserializeObject<List<Users>>(strJson);
            }
            return list;
        }
        public Users GetUserByPwd(string userName, string pwd)
        {
            return _userRepository.ListByCustom("UserName", userName).FirstOrDefault(m => m.Password == pwd.ToMd5());
        }
        public Users GetUserByGuid(Guid userGuid)
        {
            return _userRepository.ListByCustom("UserGuid", userGuid.ToString()).First();
        }
        public Users GetUserById(int userId)
        {
            return _userRepository.ListById(userId).First();
        }
        public Users InsertUser(Users user)
        {
            //添加用户
            user.IsDel = false;
            user.Password = "0".ToMd5();
            user.UserGuid = Guid.NewGuid();
            user.Id = (int)_userRepository.InsertBackRecord(user);
            //更新缓存
            var users = ListUserAll();
            users.Add(user);
            RedisHelper.SetStringMemory(AppConst.redisUserKey, JsonConvert.SerializeObject(users), TimeSpan.FromHours(2));
            return user;
        }
        public void UpdateUser(Users user)
        {
            _userRepository.Update(user);
            //更新缓存
            var users = ListUserAll();
            var userIndex = users.FindIndex(m => m.UserGuid == user.UserGuid);
            users[userIndex] = user;
            RedisHelper.SetStringMemory(AppConst.redisUserKey, JsonConvert.SerializeObject(users), TimeSpan.FromHours(2));
        }

        public List<UserDto> GetUserDto()
        {
            var users = ListUserAll();
            var allMenus = ListMenusAll();
            var allUserMenus = ListUserMenusAll();
            var dtos = _mapper.Map<List<UserDto>>(users);
            foreach (var dto in dtos)
            {

                var menuIds = allUserMenus.FindAll(m => m.UserId == dto.Id).Select(m => m.MenuId);
                var menuNames = allMenus.FindAll(m => menuIds.Contains(m.Id)).Select(m => m.MenuName);
                dto.Authorize = string.Join("，", menuNames);
            }
            return dtos;
        }
    }
}