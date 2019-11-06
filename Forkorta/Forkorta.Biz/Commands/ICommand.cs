using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forkorta.Biz.Commands
{
    public interface ICommand
    {
        Task ExecuteAction();
    }

    public enum UrlAction
    {
        Short,
        Deshort
    }
}
