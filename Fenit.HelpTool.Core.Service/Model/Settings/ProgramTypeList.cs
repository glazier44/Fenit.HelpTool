using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstallPackageLib.ProgramsType;

namespace Fenit.HelpTool.Core.Service.Model.Settings
{
    public class ProgramTypeList
    {
        public List<ProgramType> ProgramType { get; set; } = new List<ProgramType>();

        public Program FindProgram(string name)
        {
            foreach (var programType in ProgramType)
            {
                var program = programType.Programs.FirstOrDefault(w => name.Contains(w.Name));
                if (program != null)
                {
                    return program;
                }
            }
           // _programTypeListEvent?.Invoke(new ProgramType());
           return null;

        }

        private event ProgramTypeListEvent _programTypeListEvent;

        public event ProgramTypeListEvent ProgramTypeListEvent
        {
            add => _programTypeListEvent += value;
            remove => _programTypeListEvent -= value;
        }
    }
}
