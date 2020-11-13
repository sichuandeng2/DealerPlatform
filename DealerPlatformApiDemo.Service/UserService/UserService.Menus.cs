using System.Collections.Generic;
using System.Linq;
using DealerPlatformApiDemo.Entity;

namespace DealerPlatformApiDemo.Service.UserService
{
    public partial class UserService
    {
        public List<Menus> ListMenusAll()
        {
            return _menusRepository.ListAll().ToList();
        }
        public List<Menus> GetMenusByUids(IEnumerable<int> menuIds)
        {
            return _menusRepository.ListByCustomWhereIn("Id", menuIds.Select(m => m.ToString()).ToArray()).ToList();
        }
    }
}