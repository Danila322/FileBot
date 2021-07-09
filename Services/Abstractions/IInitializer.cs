using System.Threading.Tasks;

namespace FileBot.Services.Abstractions
{
    public interface IInitializer
    {
        public Task Initialize();
    }
}
