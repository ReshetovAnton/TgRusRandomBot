using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgRusRandomBot.Services;

namespace TgRusRandomBot.Cache
{
    public class RefreshService
    {
        public static void SetCountUsers()
        {
            var countUsers = DBService.GetCountUsers();
            CacheService.Set(CacheKeys.CountUsers, countUsers);
        }
    }
}
