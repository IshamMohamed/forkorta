using System.Threading.Tasks;

namespace Forkorta.Biz.Commands
{
    /// <summary>
    /// Base interface for all commands
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Action for the command
        /// </summary>
        /// <returns></returns>
        Task<string> ExecuteAction();
    }

    /// <summary>
    /// Url shorting related actions
    /// </summary>
    public enum UrlAction
    {
        Short,
        Deshort
    }
}
