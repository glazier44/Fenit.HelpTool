using System.Threading.Tasks;
using Fenit.HelpTool.Core.Service.Model.Shifter;
using Fenit.Toolbox.Core.Answers;

namespace Fenit.HelpTool.Core.Service.Abstract
{
    public interface IShifterService
    {
        Task<Response> Move(ShifterConfig shifterConfig);

        bool Cancel();
    }
}