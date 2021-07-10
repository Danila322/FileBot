using FileBot.Data;
using FileBot.Models;
using FileBot.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FileBot.Services
{
    public class UserInfoRepository : BaseRepository<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(BotDbContext context) : base(context)
        {
        }

        protected override DbSet<UserInfo> Set => Context.Users;

        public async Task<bool> Exist(long id)
        {
            var info = await Get(id);
            return info is not null;
        }
    }
}
