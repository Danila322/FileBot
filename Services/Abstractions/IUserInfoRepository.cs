using FileBot.Models;
using System.Threading.Tasks;

namespace FileBot.Services.Abstractions
{
    public interface IUserInfoRepository : IRepository<UserInfo>
    {
        public Task<bool> Exist(long id);
    }
}
