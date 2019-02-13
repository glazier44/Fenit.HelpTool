using Fenit.HelpTool.Core.Service.Model.Shifter;

namespace Fenit.HelpTool.Core.Service.Abstract
{
    public interface IShifterService
    {
        void Move(ShifterConfig shifterConfig);
    }
}