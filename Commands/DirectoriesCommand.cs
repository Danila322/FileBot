using FileBot.MarkupBuilders;
using FileBot.Models;
using FileBot.Services.Abstractions;

namespace FileBot.Commands
{
    public class DirectoriesCommand : SwitchCommand
    {
        public DirectoriesCommand(IUserInfoRepository userRepository, IMarkupBuilder<Directory> markupBuilder) 
            : base(userRepository,markupBuilder)
        {
        }

        protected override string Name => CommandName.Directories;
    }
}
