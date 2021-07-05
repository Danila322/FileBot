using FileBot.Data;
using FileBot.Models;
using FileBot.Services.Abstractions;
using System.Threading.Tasks;

namespace FileBot.Services
{
    public class UserInfoRepository : IRepository<UserInfo>
    {
        private readonly BotDbContext context;

        public UserInfoRepository(BotDbContext context)
        {
            this.context = context;
        }

        public async Task Add(UserInfo item)
        {
            await context.Users.AddAsync(item);
        }

        public async Task<bool> Exist(long id)
        {
            UserInfo userInfo = await context.Users.FindAsync(id);
            return userInfo is not null;
        }

        public async Task<UserInfo> Get(long id)
        {
            return await context.Users.FindAsync(id);
        }

        public void Remove(UserInfo item)
        {
            context.Users.Remove(item);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
