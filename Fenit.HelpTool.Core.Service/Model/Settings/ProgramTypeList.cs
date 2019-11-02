using System.Collections.Generic;
using System.Linq;
using InstallPackageLib.ProgramsType;

namespace Fenit.HelpTool.Core.Service.Model.Settings
{
    public class ProgramTypeList
    {
        public List<ProgramType> ProgramType { get; set; } = new List<ProgramType>();
        public List<string> GetProgramTypeName => ProgramType.Select(w => w.Name).ToList();

        public (ProgramType, Program) FindProgram(string name)
        {
            foreach (var programType in ProgramType)
            {
                var program = programType.Programs.FirstOrDefault(w => name.Contains(w.Name));
                if (program != null) return (programType, program);
            }
            return (null, null);
        }
        public ProgramType FindProgramFromType(string type)
        {
            return ProgramType.FirstOrDefault(w => type.Contains(w.Name));
        }
        //private event ProgramTypeListEvent _programTypeListEvent;
        //public event ProgramTypeListEvent ProgramTypeListEvent
        //{
        //    add => _programTypeListEvent += value;
        //    remove => _programTypeListEvent -= value;
        //}
    }
}